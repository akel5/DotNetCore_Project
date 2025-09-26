using Microsoft.EntityFrameworkCore.Migrations;

namespace AkelTestingTool.Migrations
{
    public partial class statuster2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusTERSTTERID",                               
                table: "TestsExeResults",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StatusSTTERID",
                table: "TestsExeResults",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestsExeResults_StatusSTTERID",
                table: "TestsExeResults",
                column: "StatusSTTERID");

            migrationBuilder.AddForeignKey(
                name: "FK_TestsExeResults_StatusTER_StatusSTTERID",
                table: "TestsExeResults",
                column: "StatusSTTERID",
                principalTable: "StatusTER",
                principalColumn: "STTERID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestsExeResults_StatusTER_StatusSTTERID",
                table: "TestsExeResults");

            migrationBuilder.DropIndex(
                name: "IX_TestsExeResults_StatusSTTERID",
                table: "TestsExeResults");

            

            migrationBuilder.DropColumn(
                name: "StatusSTTERID",
                table: "TestsExeResults");
        }
    }
}
