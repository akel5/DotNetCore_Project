using Microsoft.EntityFrameworkCore.Migrations;

namespace AkelTestingTool.Migrations
{
    public partial class addTestExecResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TestsExeResultsTERID",
                table: "BugsSummary",
                maxLength: 150,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BugsSummary_TestsExeResultsTERID", //111
                table: "BugsSummary",
                column: "TestsExeResultsTERID");


           /* migrationBuilder.AddForeignKey(
                name: "FK_BugsSummary_TestsExeResults_TestsExeResultsTERID",
                table: "BugsSummary",
                column: "TestsExeResultsTERID",
                principalTable: "TestsExeResults",
                principalColumn: "TERId2",
                onDelete: ReferentialAction.Cascade);*/
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BugsSummary_TestsExeResults_TestsExeResultsTERID",
                table: "BugsSummary");
        }
    }
}
