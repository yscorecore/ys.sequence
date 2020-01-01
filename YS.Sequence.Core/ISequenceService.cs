using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace YS.Sequence
{
    public interface ISequenceService
    {
        Task<long> GetValueAsync(string key);
        Task CreateSequence(string key, SequenceInfo sequenceInfo);
        Task<long> GetValueOrCreateAsync(string key, SequenceInfo sequenceInfo);
        Task ResetAsync(string key);
        Task<bool> RemoveAsync(string key);
        Task<bool> ExistsAsync(string key);
    }
}
