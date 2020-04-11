using System;
using System.Threading.Tasks;

namespace YS.Sequence
{
    public static class SequenceExtentions
    {
        public static Task<long> GetValueOrCreateAsync(this ISequenceService sequenceService, string name, long startValue = 1L, int step = 1, long? endValue = null)
        {
            _ = sequenceService ?? throw new ArgumentNullException(nameof(sequenceService));
            return sequenceService.GetOrCreateValue(name,
                new SequenceInfo
                {
                    StartValue = startValue,
                    Step = step,
                    EndValue = endValue
                });
        }

        public static async Task<bool> ExistsAsync(this ISequenceService sequenceService, string name)
        {
            _ = sequenceService ?? throw new ArgumentNullException(nameof(sequenceService));
            var sequence = await sequenceService.GetSequence(name);
            return sequence != null;
        }
    }
}
