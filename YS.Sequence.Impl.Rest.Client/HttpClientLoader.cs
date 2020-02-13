﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace YS.Sequence.Impl.Rest.Client
{
    public class HttpClientLoader : IServiceLoader
    {
        public void LoadServices(IServiceCollection services, IConfiguration configuration)
        {
            var apiServiceOptions = configuration.GetConfigOrNew<ApiServicesOptions>();
            // add default
            services.AddHttpClient();
            foreach (var kv in apiServiceOptions.Services)
            {
                var baseAddress = kv.Value.BaseAddress;
                services.AddHttpClient(kv.Key, (client) =>
                {
                    if (apiServiceOptions.Timeout > 0) 
                    {
                        client.Timeout = TimeSpan.FromSeconds(apiServiceOptions.Timeout);
                    }
                    client.BaseAddress = new Uri(baseAddress);
                });
            }
        }
    }
}
