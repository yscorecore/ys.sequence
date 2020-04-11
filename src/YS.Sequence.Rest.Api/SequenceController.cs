using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using YS.Knife.Api;

namespace YS.Sequence.Rest.Api
{
    public class SequenceController : ApiBase<ISequenceService>, ISequenceService
    {
        [Route("{key}")]
        [HttpPost]
        public Task CreateSequence([FromRoute] string key, [FromBody]SequenceInfo sequenceInfo)
        {
            return this.Delegater.CreateSequence(key, sequenceInfo);
        }

        [Route("{key}/info")]
        [HttpGet]
        public Task<SequenceInfo> GetSequence([FromRoute(Name = "key")] string name)
        {
            return this.Delegater.GetSequence(name);
        }
        [Route("{key}")]
        [HttpGet]
        public Task<long> GetValue([FromRoute]string key)
        {
            return this.Delegater.GetValue(key);
        }
        [Route("{key}/assert")]
        [HttpGet]
        public Task<long> GetOrCreateValue([FromRoute]string key, [FromQuery]SequenceInfo sequenceInfo)
        {
            return this.Delegater.GetOrCreateValue(key, sequenceInfo);
        }

        [Route("{key}")]
        [HttpDelete]
        public Task<bool> Remove([FromRoute]string key)
        {
            return this.Delegater.Remove(key);
        }

        [Route("{key}/reset")]
        [HttpPut]
        public Task<bool> Reset([FromRoute]string key)
        {
            return this.Delegater.Reset(key);
        }
    }
}
