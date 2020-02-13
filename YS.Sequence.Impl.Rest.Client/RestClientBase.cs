using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace YS.Sequence.Impl.Rest.Client
{
    public abstract class RestClientBase
    {
        static JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        public RestClientBase(IHttpClientFactory httpClientFactory, IOptions<ApiServicesOptions> apiServicesOptions, string serviceName)
        {
            this.ClientFactory = httpClientFactory;
            this.ApiOptions = apiServicesOptions;
            this.ServiceName = serviceName;
        }
        protected IOptions<ApiServicesOptions> ApiOptions { get; private set; }
        protected IHttpClientFactory ClientFactory { get; private set; }
        protected string ServiceName { get; private set; }


        public virtual async Task Invoke(RestApiInfo apiInfo)
        {
            var client = this.ClientFactory.CreateClient(ServiceName);

            var controllerPath = TranslatePath(apiInfo, apiInfo.ControllerRoute);
            var actionPath = TranslatePath(apiInfo, apiInfo.ActionRoute);
            var queryString = BuildQueryString(apiInfo);
            var requestUri = CombinUri(controllerPath, actionPath, queryString);

            var request = new HttpRequestMessage(apiInfo.Method, requestUri);
            this.AppendRequestHeader(apiInfo, request);
            this.AppendRequestBody(apiInfo, request);
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public virtual async Task<T> Invoke<T>(RestApiInfo apiInfo)
        {
            var client = this.ClientFactory.CreateClient(ServiceName);

            var controllerPath = TranslatePath(apiInfo, apiInfo.ControllerRoute);
            var actionPath = TranslatePath(apiInfo, apiInfo.ActionRoute);
            var queryString = BuildQueryString(apiInfo);
            var requestUri = CombinUri(controllerPath, actionPath, queryString);

            var request = new HttpRequestMessage(apiInfo.Method, requestUri);
            this.AppendRequestHeader(apiInfo, request);
            var response = await client.SendAsync(request);


            return FromResponse<T>(response);
        }
        private T FromResponse<T>(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
            var content = response.Content;
            var responseData = content.ReadAsStringAsync().Result;
            if (string.IsNullOrEmpty(responseData))
            {
                return default(T);
            }
            return JsonSerializer.Deserialize<T>(responseData, JsonOptions);
        }

        private string TranslatePath(RestApiInfo apiInfo, string path)
        {
            // replace [controller] ,[action]
            var path1 = Regex.Replace(path, $"\\[(?<nm>\\w+)\\]", (m) =>
             {
                 var nm = m.Groups["nm"].Value;
                 if (string.Equals(nm, "controller", StringComparison.InvariantCultureIgnoreCase))
                 {
                     return apiInfo.Controller;
                 }
                 else if (string.Equals(nm, "action", StringComparison.InvariantCultureIgnoreCase))
                 {
                     return apiInfo.Action;
                 }
                 return m.Value;
             });
            // replace {key},{key:int},{age:range(18,120)},{ssn:regex(^\d{{3}}-\d{{2}}-\d{{4}}$)}
            var path2 = Regex.Replace(path1, "\\{(?<nm>\\w+)(\\?)?(:.+)?\\}", (m) =>
            {
                var nm = m.Groups["nm"].Value;
                var argument = apiInfo.Arguments.Single(p => p.Source == ArgumentSource.FromRouter && nm.Equals(p.Name, StringComparison.InvariantCultureIgnoreCase));
                return ValueToString(argument.Value);
            });
            return path2;
        }
        private string ValueToString(object value)
        {
            if (value == null) return string.Empty;
            if (value is string) return value as string;
            var converter = System.ComponentModel.TypeDescriptor.GetConverter(value);
            return converter.ConvertToInvariantString(value);
        }
        private Uri CombinUri(string controllerPath, string actionPath, string queryString)
        {
            if (string.IsNullOrEmpty(controllerPath))
            {
                return new Uri(actionPath + queryString, UriKind.Relative);
            }
            if (string.IsNullOrEmpty(actionPath))
            {
                return new Uri(controllerPath + queryString, UriKind.Relative);
            }
            return new Uri($"{controllerPath}/{actionPath}" + queryString, UriKind.Relative);
        }

        private string BuildQueryString(RestApiInfo apiInfo)
        {
            var queryItems = apiInfo.Arguments.Where(p => p.Source == ArgumentSource.FromQuery).ToList();
            if (queryItems.Count == 0) return string.Empty;
            var queryString = QueryString.Empty;
            foreach (var queryItem in queryItems)
            {
                // TODO array ,list 
                queryString = queryString.Add(queryItem.Name, ValueToString(queryItem.Value));
            }
            return queryString.ToUriComponent();
        }

        private void AppendRequestHeader(RestApiInfo apiInfo, HttpRequestMessage httpRequestMessage)
        {
            var headerItems = apiInfo.Arguments.Where(p => p.Source == ArgumentSource.FromHeader);
            foreach (var headerItem in headerItems)
            {
                httpRequestMessage.Headers.Add(headerItem.Name, ValueToString(headerItem.Value));
            }
        }
        private void AppendRequestBody(RestApiInfo apiInfo, HttpRequestMessage httpRequestMessage)
        {
            var bodyItem = apiInfo.Arguments.SingleOrDefault(p => p.Source == ArgumentSource.FromBody);
            if (bodyItem != null)
            {
                var text = JsonSerializer.Serialize(bodyItem.Value);
                httpRequestMessage.Content = new StringContent(text, Encoding.UTF8, "application/json");
            }
        }
    }
    public class RestApiInfo
    {
        public HttpMethod Method { get; set; }

        public string ActionRoute { get; set; }

        public string ControllerRoute { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public List<RestArgument> Arguments { get; set; }

    }

    public class RestArgument
    {
        public RestArgument(string name, ArgumentSource source, object value)
        {
            this.Name = name;
            this.Source = source;
            this.Value = value;
        }
        public string Name { get; private set; }

        public ArgumentSource Source { get; private set; }

        public object Value { get; private set; }

    }

    public enum ArgumentSource
    {
        FromQuery,
        FromBody,
        FromHeader,
        FromForm,
        FromRouter
    }
}
