using Microsoft.EntityFrameworkCore.Migrations;
using System.Collections.Generic;
using System.Linq;

namespace YS.Sequence.Impl.EFCore.SqlServer.Migrations
{
    public partial class AddProc : Migration
    {
        static IDictionary<string, string> Procs = new Dictionary<string, string>
        {
            ["GetOrCreateSequenceValue"] = "20200105132243_GetOrCreateSequenceValue.sql",
            ["GetSequenceValue"]= "20200105132243_GetSequenceValue.sql"

        };
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            foreach (var procResourceName in Procs.Values)
            {
                migrationBuilder.Sql(GetSqlStringFromResource(procResourceName));
            }
        }
       
      
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            foreach (var procName in Procs.Keys.Reverse())
            {
                migrationBuilder.Sql($"drop procedure {procName};");
            }

        }
        private string GetSqlStringFromResource(string resourceName)
        {
            using (var stream = this.GetType().Assembly.GetManifestResourceStream(this.GetType(), resourceName))
            {
                using (var reader = new System.IO.StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

      
    }
}
