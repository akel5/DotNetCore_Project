using Microsoft.EntityFrameworkCore.Migrations;

namespace AkelTestingTool.Migrations
{
    public partial class removeStatusTE4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TStatus",
                table: "TestExcutions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TStatus",
                table: "TestExcutions",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");
        }
    }
}
