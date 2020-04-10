using System.Threading.Tasks;
using Knife.Api;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace YS.Sequence.Api
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
        public Task<SequenceInfo> GetSequence([FromRoute(Name ="key")] string name)
        {
            return this.Delegater.GetSequence(name);
        }
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Route("{key}")]
        [HttpGet]
        public Task<long> GetValueAsync([FromRoute]string key)
        {
            return this.Delegater.GetValueAsync(key);
        }
        [Route("{key}/assert")]
        [HttpGet]
        public Task<long> GetValueOrCreateAsync([FromRoute]string key, [FromQuery]SequenceInfo sequenceInfo)
        {
            return this.Delegater.GetValueOrCreateAsync(key, sequenceInfo);
        }

        [Route("{key}")]
        [HttpDelete]
        public Task<bool> RemoveAsync([FromRoute]string key)
        {
            return this.Delegater.RemoveAsync(key);
        }

        [Route("{key}/reset")]
        [HttpPut]
        public Task<bool> ResetAsync([FromRoute]string key)
        {
            return this.Delegater.ResetAsync(key);
        }
    }
}
