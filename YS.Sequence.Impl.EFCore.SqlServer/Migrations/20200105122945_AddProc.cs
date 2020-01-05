using Microsoft.EntityFrameworkCore.Migrations;
using System.Linq;
using YS.Sequence.Impl.EFCore.SqlServer.Migrations._20200105122945.Functions;
using YS.Sequence.Impl.EFCore.SqlServer.Migrations._20200105122945.StoredProcedures;

namespace YS.Sequence.Impl.EFCore.SqlServer.Migrations
{
    public partial class AddProc : Migration
    {
        static string[] Functions = new string[] {
           nameof(Funcs.ExistsSequence)
        };
        static string[] StoredProcs = new string[] {
            nameof(Procs.CreateSequence),
            nameof(Procs.DeleteSequence),
            nameof(Procs.GetOrCreateSequenceValue),
            nameof(Procs.GetSequenceInfo),
            nameof(Procs.ResetSequence),
            nameof(Procs.GetSequenceValue)

        };
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            this.UpFunctions(migrationBuilder);
            this.UpStoredProcs(migrationBuilder);
        }
        private void UpFunctions(MigrationBuilder migrationBuilder)
        {
            foreach (var funName in Functions)
            {
                migrationBuilder.Sql(Funcs.ResourceManager.GetString(funName));
            }
        }
        private void UpStoredProcs(MigrationBuilder migrationBuilder)
        {
            foreach (var procName in StoredProcs)
            {
                migrationBuilder.Sql(Procs.ResourceManager.GetString(procName));
            }
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            this.DownStoredProcs(migrationBuilder);
            this.DownFunctions(migrationBuilder);

        }
        private void DownFunctions(MigrationBuilder migrationBuilder)
        {
            foreach (var funName in Functions.Reverse())
            {
                migrationBuilder.Sql($"drop function {funName};");
            }
        }
        private void DownStoredProcs(MigrationBuilder migrationBuilder)
        {
            foreach (var procName in StoredProcs.Reverse())
            {
                migrationBuilder.Sql($"drop procedure {procName};");
            }
        }
    }
}
