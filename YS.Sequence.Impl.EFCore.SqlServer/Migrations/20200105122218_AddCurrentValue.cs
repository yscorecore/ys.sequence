using Microsoft.EntityFrameworkCore.Migrations;

namespace YS.Sequence.Impl.EFCore.SqlServer.Migrations
{
    public partial class AddCurrentValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CurrentValue",
                table: "Sequences",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentValue",
                table: "Sequences");
        }
    }
}
