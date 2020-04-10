using System.Threading.Tasks;

namespace YS.Sequence
{
    public interface ISequenceService
    {
        Task<long> GetValue(string name);
        Task CreateSequence(string name, SequenceInfo sequenceInfo);
        Task<long> GetOrCreateValue(string name, SequenceInfo sequenceInfo);
        Task<bool> Reset(string name);
        Task<bool> Remove(string name);
        Task<SequenceInfo> GetSequence(string name);
    }
}
