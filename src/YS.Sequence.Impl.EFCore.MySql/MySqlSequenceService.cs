using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Threading.Tasks;

namespace YS.Sequence.Impl.EFCore.MySql
{
    [ServiceClass()]
    public class MySqlSequenceService : BaseSequenceService
    {
        public MySqlSequenceService(SequenceContext sequenceContext) : base(sequenceContext)
        {
        }
        public override Task<long> GetValue(string name)
        {
            var nameParam = new MySqlParameter("seqence_name", name);
            var valueParam = new MySqlParameter("current_value", System.Data.SqlDbType.BigInt)
            {
                Direction = System.Data.ParameterDirection.Output
            };
            this.sequenceContext.Database.ExecuteStoredProcedureAsNonQuery("GetSequenceValue", nameParam, valueParam);
            return Task.FromResult(Convert.ToInt64(valueParam.Value));
        }

        public override Task<long> GetOrCreateValue(string name, Sequence.SequenceInfo sequenceInfo)
        {
            sequenceInfo = sequenceInfo ?? YS.Sequence.SequenceInfo.Default;
            var nameParam = new MySqlParameter("seqence_name", name);
            var valueParam = new MySqlParameter("current_value", System.Data.SqlDbType.BigInt)
            {
                Direction = System.Data.ParameterDirection.Output
            };
            var startParam = new MySqlParameter("start_value", sequenceInfo.StartValue);
            var endParam = new MySqlParameter("end_value", sequenceInfo.EndValue);
            var stepParam = new MySqlParameter("step_value", sequenceInfo.Step);
            this.sequenceContext.Database.ExecuteStoredProcedureAsNonQuery(
                "GetOrCreateSequenceValue",
                nameParam,
                valueParam,
                startParam,
                endParam,
                stepParam);
            return Task.FromResult(Convert.ToInt64(valueParam.Value));
        }


    }


}
