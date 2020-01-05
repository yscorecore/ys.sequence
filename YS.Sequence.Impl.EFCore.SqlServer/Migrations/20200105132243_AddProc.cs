using Microsoft.EntityFrameworkCore.Migrations;
using System.Linq;
using YS.Sequence.Impl.EFCore.SqlServer.Migrations._20200105132243_AddProc;

namespace YS.Sequence.Impl.EFCore.SqlServer.Migrations
{
    public partial class AddProc : Migration
    {
        static string[] StoredProcs = new string[] {
            nameof(Procs.GetOrCreateSequenceValue),
            nameof(Procs.GetSequenceValue)

        };
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            foreach (var procName in StoredProcs)
            {
                migrationBuilder.Sql(Procs.ResourceManager.GetString(procName));
            }
        }
       
      
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            foreach (var procName in StoredProcs.Reverse())
            {
                migrationBuilder.Sql($"drop procedure {procName};");
            }

        }
      
    }
}
