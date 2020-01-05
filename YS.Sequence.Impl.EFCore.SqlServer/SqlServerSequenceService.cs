using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YS.Sequence.Impl.EFCore;

namespace YS.Sequence.Impl.SqlServer
{
    [ServiceClass]
    public class SqlServerSequenceService : ISequenceService
    {
        public SqlServerSequenceService(SequenceContext sequenceContext)
        {
            this.sequenceContext = sequenceContext;
        }
        private SequenceContext sequenceContext;
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
