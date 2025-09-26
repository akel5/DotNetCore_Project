using Microsoft.EntityFrameworkCore.Migrations;

namespace AkelTestingTool.Migrations
{
    public partial class bug23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Readers",
                table: "BugsSummary2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Readers",
                table: "BugsSummary2",
                nullable: false,
                defaultValue: 0);
        }
    }
}
