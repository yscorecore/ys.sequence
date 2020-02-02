using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace YS.Sequence.Impl.EFCore
{
    public abstract class BaseSequenceService : ISequenceService
    {
        public BaseSequenceService(SequenceContext sequenceContext)
        {
            this.sequenceContext = sequenceContext;
        }
        protected SequenceContext sequenceContext;


        public async Task CreateSequence(string name, YS.Sequence.SequenceInfo sequenceInfo)
        {
            if (sequenceInfo == null)
            {
                throw new ArgumentNullException(nameof(sequenceInfo));
            }
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            var row = new EFCore.SequenceInfo
            {
                Id = Guid.NewGuid(),
                StartValue = sequenceInfo == null ? 1 : sequenceInfo.StartValue,
                Step = sequenceInfo == null ? 1 : sequenceInfo.Step,
                EndValue = sequenceInfo?.EndValue,
                Name = name
            };
            sequenceContext.Sequences.Add(row);
            await sequenceContext.SaveChangesAsync();

            sequenceContext.Entry(row).DetectChanges();
        }

        public async Task<bool> ResetAsync(string name)
        {
            var entity = await sequenceContext.Sequences.SingleOrDefaultAsync(p => p.Name == name);
            if (entity != null)
            {
                entity.CurrentValue = null;
                this.sequenceContext.Entry(entity).Property(p => p.CurrentValue).IsModified = true;
                await sequenceContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> RemoveAsync(string name)
        {
            var entity = await sequenceContext.Sequences.SingleOrDefaultAsync(p => p.Name == name);
            if (entity != null)
            {
                sequenceContext.Sequences.Remove(entity);
                var changeRows = await sequenceContext.SaveChangesAsync();
                return Convert.ToBoolean(changeRows);
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> ExistsAsync(string name)
        {
            var totalCount = await this.sequenceContext.Sequences.CountAsync(p => p.Name == name);
            return totalCount > 0;
        }
        public abstract Task<long> GetValueAsync(string name);
        public abstract Task<long> GetValueOrCreateAsync(string name, Sequence.SequenceInfo sequenceInfo);

        public async Task<Sequence.SequenceInfo> GetSequence(string name)
        {
            var entity = await sequenceContext.Sequences.SingleOrDefaultAsync(p => p.Name == name);
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
