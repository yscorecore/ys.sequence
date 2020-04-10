using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YS.Sequence.Impl.EFCore.SqlServer.Migrations
{
    public partial class CreateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sequences",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 128, nullable: false),
                    StartValue = table.Column<long>(nullable: false),
                    Step = table.Column<int>(nullable: false),
                    EndValue = table.Column<long>(nullable: true),
                    CurrentValue = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sequences", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sequences_Name",
                table: "Sequences",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sequences");
        }
    }
}
