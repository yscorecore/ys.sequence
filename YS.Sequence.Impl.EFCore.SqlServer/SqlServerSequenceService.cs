using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YS.Sequence.Impl.EFCore;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;

namespace YS.Sequence.Impl.SqlServer
{
    [ServiceClass(Lifetime = ServiceLifetime.Scoped)]
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
        public async Task<long> GetValueAsync(string name)
        {
            var nameParam = new SqlParameter("@seqenceName", name);
            var valueParam = new SqlParameter("@currentValue", System.Data.SqlDbType.BigInt)
            {
                Direction = System.Data.ParameterDirection.Output
            };
            await sequenceContext.Database.ExecuteSqlRawAsync("Exec GetSequenceValue @seqenceName, @currentValue OUTPUT",
                new SqlParameter[] { nameParam, valueParam });
            if (Convert.IsDBNull(valueParam.Value))
            {
                throw new InvalidOperationException($"Can not find the sequence named {name}.");
            }
            return Convert.ToInt64(valueParam.Value);
        }

        public async Task<long> GetValueOrCreateAsync(string name, SequenceInfo sequenceInfo)
        {
            var nameParam = new SqlParameter("@seqenceName", name);
            var startParam = new SqlParameter("@startValue", sequenceInfo.StartValue);
            var endParam = new SqlParameter("@endValue", sequenceInfo.EndValue);
            var stepParam = new SqlParameter("@step", sequenceInfo.Step == 0 ? 1 : sequenceInfo.Step);
            var valueParam = new SqlParameter("@currentValue", System.Data.SqlDbType.BigInt)
            {
                Direction = System.Data.ParameterDirection.Output
            };
            if (sequenceInfo.EndValue.HasValue)
            {
                await sequenceContext.Database.ExecuteSqlRawAsync(
                    "Exec GetOrCreateSequenceValue @seqenceName, @currentValue OUTPUT, @startValue, @endValue, @step",
                    new SqlParameter[] { nameParam, valueParam, startParam, endParam, stepParam });
            }
            else
            {
                await sequenceContext.Database.ExecuteSqlRawAsync(
                    "Exec GetOrCreateSequenceValue @seqenceName, @currentValue OUTPUT, @startValue, @step",
                    new SqlParameter[] { nameParam, valueParam, startParam, stepParam });
            }

            return Convert.ToInt64(valueParam.Value);
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
