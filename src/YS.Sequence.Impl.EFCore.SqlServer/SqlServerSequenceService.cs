﻿using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using YS.Knife;
using YS.Sequence.Impl.EFCore;

namespace YS.Sequence.Impl.SqlServer
{
    [ServiceClass()]
    public class SqlServerSequenceService : BaseSequenceService
    {
        public SqlServerSequenceService(SequenceContext sequenceContext) : base(sequenceContext)
        {
        }

        public override async Task<long> GetValue(string name)
        {
            var nameParam = new SqlParameter("@seqenceName", name);
            var valueParam = new SqlParameter("@currentValue", System.Data.SqlDbType.BigInt)
            {
                Direction = System.Data.ParameterDirection.Output
            };
            await SequenceContext.Database.ExecuteSqlRawAsync("Exec GetSequenceValue @seqenceName, @currentValue OUTPUT",
                new SqlParameter[] { nameParam, valueParam });
            if (Convert.IsDBNull(valueParam.Value))
            {
                throw new InvalidOperationException($"Can not find the sequence named {name}.");
            }
            return (long)valueParam.Value;
        }

        public override async Task<long> GetOrCreateValue(string name, SequenceInfo sequenceInfo)
        {
            sequenceInfo = sequenceInfo ?? YS.Sequence.SequenceInfo.Default;
            var nameParam = new SqlParameter("@seqenceName", name);
            var startParam = new SqlParameter("@startValue", sequenceInfo.StartValue);
            var endParam = new SqlParameter("@endValue", sequenceInfo.EndValue);
            var stepParam = new SqlParameter("@step", sequenceInfo.Step);
            var valueParam = new SqlParameter("@currentValue", System.Data.SqlDbType.BigInt)
            {
                Direction = System.Data.ParameterDirection.Output
            };
            if (sequenceInfo.EndValue.HasValue)
            {
                await SequenceContext.Database.ExecuteSqlRawAsync(
                    "Exec GetOrCreateSequenceValue @seqenceName, @currentValue OUTPUT, @startValue, @endValue, @step",
                    new SqlParameter[] { nameParam, valueParam, startParam, endParam, stepParam });
            }
            else
            {
                await SequenceContext.Database.ExecuteSqlRawAsync(
                    "Exec GetOrCreateSequenceValue @seqenceName, @currentValue OUTPUT, @startValue, @step",
                    new SqlParameter[] { nameParam, valueParam, startParam, stepParam });
            }

            return (long)valueParam.Value;
        }
    }
}
