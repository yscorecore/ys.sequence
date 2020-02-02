using System.Threading.Tasks;
using Knife.Api;
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

        [Route("{key}/assert")]
        [HttpGet]
        public Task<bool> ExistsAsync([FromRoute]string key)
        {
            return this.Delegater.ExistsAsync(key);
        }

        public Task<SequenceInfo> GetSequence(string name)
        {
            throw new System.NotImplementedException();
        }

        [Route("{key}")]
        [HttpGet]
        public Task<long> GetValueAsync([FromRoute]string key)
        {
            return this.Delegater.GetValueAsync(key);
        }
        [Route("{key}/assert")]
        [HttpPost]
        public Task<long> GetValueOrCreateAsync([FromRoute]string key, [FromBody]SequenceInfo sequenceInfo)
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
