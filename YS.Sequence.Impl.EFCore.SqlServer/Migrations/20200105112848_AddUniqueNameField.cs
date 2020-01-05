using Microsoft.EntityFrameworkCore.Migrations;

namespace YS.Sequence.Impl.EFCore.SqlServer.Migrations
{
    public partial class AddUniqueNameField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Sequences",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Sequences_Name",
                table: "Sequences",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Sequences_Name",
                table: "Sequences");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Sequences");
        }
    }
}
