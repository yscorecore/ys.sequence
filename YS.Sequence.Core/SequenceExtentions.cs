using System.Threading.Tasks;

namespace YS.Sequence
{
    public static class SequenceExtentions
    {
        public static Task<long> GetValueOrCreateAsync(this ISequenceService sequenceService, string name, long startValue = 1L, int step = 1, long? endValue = null)
        {
            return sequenceService.GetValueOrCreateAsync(name, new SequenceInfo
            {
                StartValue = startValue,
                Step = step,
                EndValue = endValue
            });
        }

        public static async Task<bool> ExistsAsync(this ISequenceService sequenceService, string name)
        {
            var sequence = await sequenceService.GetSequence(name);
            return sequence != null;
        }
    }
}
