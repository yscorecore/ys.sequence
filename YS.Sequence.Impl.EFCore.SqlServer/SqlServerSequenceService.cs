using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YS.Sequence.Impl.EFCore;
using System.Linq;

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

       
        public async Task CreateSequence(string name, SequenceInfo sequenceInfo)
        {
            sequenceContext.Sequences.Add(new EFCore.SequenceInfo
            {
                Id = Guid.NewGuid(),
                StartValue = sequenceInfo.StartValue,
                Step = sequenceInfo.Step,
                EndValue = sequenceInfo.EndValue,
                Name = name
            });
            await sequenceContext.SaveChangesAsync();
        }
        public Task<long> GetValueAsync(string name)
        {
            return null;
        }

        public Task<long> GetValueOrCreateAsync(string name, SequenceInfo sequenceInfo)
        {
            throw new NotImplementedException();
        }

        public async Task ResetAsync(string name)
        {
            var entity = await sequenceContext.Sequences.FirstAsync(p => p.Name == name);
            if (entity != null)
            {
                entity.CurrentValue = null;
                await sequenceContext.SaveChangesAsync();
            }
        }

        public async Task<bool> RemoveAsync(string name)
        {
            var entity = await sequenceContext.Sequences.FirstAsync(p => p.Name == name);
            if (entity != null)
            {
                sequenceContext.Sequences.Remove(entity);
                return await sequenceContext.SaveChangesAsync().ContinueWith(Convert.ToBoolean);
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
    }
}
