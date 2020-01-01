using System.Threading.Tasks;

namespace YS.Sequence
{
    public static class SequenceExtentions
    {
        public static Task<long> GetValueOrCreateAsync(this ISequenceService sequenceService, string key, long startValue = 1L, long step = 1, long? endValue = null)
        {
            return sequenceService.GetValueOrCreateAsync(key, new SequenceInfo
            {
                StartValue = startValue,
                Step = 1,
                EndValue = endValue
            });
        }
    }
}
