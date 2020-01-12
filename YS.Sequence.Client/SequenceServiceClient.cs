using System;
using System.Threading.Tasks;

namespace YS.Sequence.Client
{

    public class SequenceServiceClient : ISequenceService
    {
        public Task CreateSequence(string name, SequenceInfo sequenceInfo)
        {
            
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<long> GetValueAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<long> GetValueOrCreateAsync(string name, SequenceInfo sequenceInfo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task ResetAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}
