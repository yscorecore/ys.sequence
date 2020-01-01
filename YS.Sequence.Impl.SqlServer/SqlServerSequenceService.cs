using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace YS.Sequence.Impl.SqlServer
{
    [ServiceClass]
    public class SqlServerSequenceService : ISequenceService
    {
        public Task CreateSequence(string key, SequenceInfo sequenceInfo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<long> GetValueAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<long> GetValueOrCreateAsync(string key, SequenceInfo sequenceInfo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task ResetAsync(string key)
        {
            throw new NotImplementedException();
        }
    }
}
