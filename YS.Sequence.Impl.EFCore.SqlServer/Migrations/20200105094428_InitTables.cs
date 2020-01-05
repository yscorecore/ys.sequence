using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YS.Sequence.Impl.EFCore.SqlServer.Migrations
{
    public partial class InitTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sequences",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    StartValue = table.Column<long>(nullable: false),
                    Step = table.Column<int>(nullable: false),
                    EndValue = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sequences", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sequences");
        }
    }
}
