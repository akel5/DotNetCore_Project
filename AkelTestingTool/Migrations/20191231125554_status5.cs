using Microsoft.EntityFrameworkCore.Migrations;

namespace AkelTestingTool.Migrations
{
    public partial class status5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusSTID", //111
                table: "BugsSummary",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BugsSummary_StatusSTID", //111
                table: "BugsSummary",
                column: "StatusSTID"); //111

            migrationBuilder.AddForeignKey(
                name: "FK_BugsSummary_Status_StatusSTID", //111
                table: "BugsSummary",
                column: "StatusSTID", //111
                principalTable: "Status",
                principalColumn: "STID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BugsSummary_Status_StatusSTID", //111
                table: "BugsSummary");

            migrationBuilder.DropIndex(
                name: "IX_BugsSummary_StatusSTID", //111
                table: "BugsSummary");

            migrationBuilder.DropColumn(
                name: "StatusSTID", //111
                table: "BugsSummary");
        }
    }
}
