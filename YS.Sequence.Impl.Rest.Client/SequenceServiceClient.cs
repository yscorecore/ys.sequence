using Knife.Rest.Client;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace YS.Sequence.Impl.Rest.Client
{
    [ServiceClass()]
    public class SequenceServiceClient : ClientBase, ISequenceService
    {
        public SequenceServiceClient(IHttpClientFactory httpClientFactory, IOptions<ApiServicesOptions> apiServicesOptions)
            : base(httpClientFactory, apiServicesOptions, "SequenceService")
        {
        }

        public Task CreateSequence(string name, SequenceInfo sequenceInfo)
        {
            return this.SendHttp(
                 new RestApiInfo
                 {
                     Method = HttpMethod.Post,
                     Path = "Sequence/{key}",
                     Arguments = new List<RestArgument>
                     {
                        new RestArgument("key", ArgumentSource.FromRouter, name),
                        new RestArgument("sequenceInfo", ArgumentSource.FromBody, sequenceInfo),
                     }
                 });
        }

        public Task<SequenceInfo> GetSequence(string name)
        {
            return this.SendHttp<SequenceInfo>(
                  new RestApiInfo
                  {
                      Method = HttpMethod.Get,
                      Path = "sequence/{key}/info",
                      Arguments = new List<RestArgument>
                      {
                          new RestArgument("key", ArgumentSource.FromRouter, name),
                      }
                  });
        }

        public Task<long> GetValue(string name)
        {
            return this.SendHttp<long>(
                new RestApiInfo
                {
                    Method = HttpMethod.Get,
                    Path = "sequence/{key}",
                    Arguments = new List<RestArgument>()
                    {
                        new RestArgument("key", ArgumentSource.FromRouter, name),
                    }
                });
        }

        public Task<long> GetOrCreateValue(string name, SequenceInfo sequenceInfo)
        {
            return this.SendHttp<long>(
                new RestApiInfo
                {
                    Method = HttpMethod.Get,
                    Path = "sequence/{key}/assert",
                    Arguments = new List<RestArgument>()
                    {
                        new RestArgument("key", ArgumentSource.FromRouter, name),
                        new RestArgument("sequenceInfo", ArgumentSource.FromQuery, sequenceInfo),
                    }
                });
        }

        public Task<bool> Remove(string name)
        {
            return this.SendHttp<bool>(
                new RestApiInfo
                {
                    Method = HttpMethod.Delete,
                    Path = "sequence/{key}",
                    Arguments = new List<RestArgument>()
                    {
                        new RestArgument("key", ArgumentSource.FromRouter, name),
                    }
                });
        }

        public Task<bool> Reset(string name)
        {
            return this.SendHttp<bool>(
                new RestApiInfo
                {
                    Method = HttpMethod.Put,
                    Path = "sequence/{key}/reset",
                    Arguments = new List<RestArgument>()
                    {
                        new RestArgument("key", ArgumentSource.FromRouter, name),
                    }
                });
        }
    }
}
