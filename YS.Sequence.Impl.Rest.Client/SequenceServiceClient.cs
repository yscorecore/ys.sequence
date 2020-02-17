using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace YS.Sequence.Impl.Rest.Client
{
    [ServiceClass(Lifetime = Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped)]
    public class SequenceServiceClient : RestClientBase, ISequenceService
    {
        public SequenceServiceClient(IHttpClientFactory httpClientFactory, IOptions<ApiServicesOptions> apiServicesOptions)
            : base(httpClientFactory, apiServicesOptions, "SequenceService")
        {
        }

        public Task CreateSequence(string name, SequenceInfo sequenceInfo)
        {
            return this.Invoke(
                 new RestApiInfo
                 {
                     Method = HttpMethod.Post,
                     ControllerRoute = "[controller]",
                     Controller = "Sequence",
                     ActionRoute = "{key}",
                     Action = "CreateSequence",
                     Arguments = new List<RestArgument>
                     {
                        new RestArgument("key", ArgumentSource.FromRouter, name),
                        new RestArgument("sequenceInfo", ArgumentSource.FromBody, sequenceInfo),
                     }
                 });
        }

        public Task<SequenceInfo> GetSequence(string name)
        {
            return this.Invoke<SequenceInfo>(
                  new RestApiInfo
                  {
                      Method = HttpMethod.Get,
                      ControllerRoute = "[controller]",
                      Controller = "Sequence",
                      ActionRoute = "{key}/info",
                      Action = "GetSequence",
                      Arguments = new List<RestArgument>
                      {
                          new RestArgument("key", ArgumentSource.FromRouter, name),
                      }
                  });
        }

        public Task<long> GetValue(string name)
        {
            return this.Invoke<long>(
                new RestApiInfo
                {
                    Method = HttpMethod.Get,
                    ControllerRoute = "[controller]",
                    Controller = "Sequence",
                    ActionRoute = "{key}",
                    Action = "GetValue",
                    Arguments = new List<RestArgument>()
                    {
                        new RestArgument("key", ArgumentSource.FromRouter, name),
                    }
                });
        }

        public Task<long> GetOrCreateValue(string name, SequenceInfo sequenceInfo)
        {
            return this.Invoke<long>(
                new RestApiInfo
                {
                    Method = HttpMethod.Get,
                    ControllerRoute = "[controller]",
                    Controller = "Sequence",
                    ActionRoute = "{key}/assert",
                    Action = "GetValueOrCreateAsync",
                    Arguments = new List<RestArgument>()
                    {
                        new RestArgument("key", ArgumentSource.FromRouter, name),
                        new RestArgument("sequenceInfo", ArgumentSource.FromQuery, sequenceInfo),
                    }
                });
        }

        public Task<bool> Remove(string name)
        {
            return this.Invoke<bool>(
                new RestApiInfo
                {
                    Method = HttpMethod.Delete,
                    ControllerRoute = "[controller]",
                    Controller = "Sequence",
                    ActionRoute = "{key}",
                    Action = "RemoveAsync",
                    Arguments = new List<RestArgument>()
                    {
                        new RestArgument("key", ArgumentSource.FromRouter, name),
                    }
                });
        }

        public Task<bool> Reset(string name)
        {
            return this.Invoke<bool>(
                new RestApiInfo
                {
                    Method = HttpMethod.Put,
                    ControllerRoute = "[controller]",
                    Controller = "Sequence",
                    ActionRoute = "{key}/reset",
                    Action = "RemoveAsync",
                    Arguments = new List<RestArgument>()
                    {
                        new RestArgument("key", ArgumentSource.FromRouter, name),
                    }
                });
        }
    }
}
