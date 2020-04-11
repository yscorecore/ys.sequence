using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace YS.Sequence.Impl.EFCore
{
    public abstract class BaseSequenceService : ISequenceService
    {
        public BaseSequenceService(SequenceContext sequenceContext)
        {
            this.SequenceContext = sequenceContext;
        }
        protected SequenceContext SequenceContext { get; }


        public async Task CreateSequence(string name, YS.Sequence.SequenceInfo sequenceInfo)
        {
            sequenceInfo = sequenceInfo ?? YS.Sequence.SequenceInfo.Default;
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            var row = new EFCore.SequenceInfo
            {
                Id = Guid.NewGuid(),
                StartValue = sequenceInfo.StartValue,
                Step = sequenceInfo.Step,
                EndValue = sequenceInfo.EndValue,
                Name = name
            };
            SequenceContext.Sequences.Add(row);
            await SequenceContext.SaveChangesAsync();

            SequenceContext.Entry(row).DetectChanges();
        }

        public async Task<bool> Reset(string name)
        {
            var entity = await SequenceContext.Sequences.SingleOrDefaultAsync(p => p.Name == name);
            if (entity != null)
            {
                entity.CurrentValue = null;
                this.SequenceContext.Entry(entity).Property(p => p.CurrentValue).IsModified = true;
                await SequenceContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Remove(string name)
        {
            var entity = await SequenceContext.Sequences.SingleOrDefaultAsync(p => p.Name == name);
            if (entity != null)
            {
                SequenceContext.Sequences.Remove(entity);
                var changeRows = await SequenceContext.SaveChangesAsync();
                return Convert.ToBoolean(changeRows);
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> ExistsAsync(string name)
        {
            var totalCount = await this.SequenceContext.Sequences.CountAsync(p => p.Name == name);
            return totalCount > 0;
        }
        public abstract Task<long> GetValue(string name);
        public abstract Task<long> GetOrCreateValue(string name, Sequence.SequenceInfo sequenceInfo);

        public async Task<Sequence.SequenceInfo> GetSequence(string name)
        {
            var entity = await SequenceContext.Sequences.SingleOrDefaultAsync(p => p.Name == name);
            if (entity != null)
            {
                return new Sequence.SequenceInfo()
                {
                    StartValue = entity.StartValue,
                    EndValue = entity.EndValue,
                    Step = entity.Step,
                };
            }
            else
            {
                return null;
            }
        }
    }
}
