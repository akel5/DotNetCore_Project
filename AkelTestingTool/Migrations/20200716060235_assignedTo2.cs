using Microsoft.EntityFrameworkCore.Migrations;

namespace AkelTestingTool.Migrations
{
    public partial class assignedTo2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

           migrationBuilder.CreateIndex(
                name: "IX_TestExcutions_AssignedToAssignedToID",
                table: "TestExcutions",
                column: "AssignedToAssignedToID");

            migrationBuilder.AddForeignKey(
                name: "FK_TestExcutions_AssignedTo_AssignedToAssignedToID",
                table: "TestExcutions",
                column: "AssignedToAssignedToID",
                principalTable: "AssignedTo",
                principalColumn: "AssignedToID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestExcutions_AssignedTo_AssignedToAssignedToID",
                table: "TestExcutions");

            migrationBuilder.DropIndex(
                name: "IX_TestExcutions_AssignedToAssignedToID",
                table: "TestExcutions");

            migrationBuilder.DropColumn(
                name: "AssignedToAssignedToID",
                table: "TestExcutions");
        }
    }
}
