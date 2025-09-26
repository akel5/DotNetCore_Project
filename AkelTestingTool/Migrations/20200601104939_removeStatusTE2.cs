using Microsoft.EntityFrameworkCore.Migrations;

namespace AkelTestingTool.Migrations
{
    public partial class removeStatusTE2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusTESTTEID",
                table: "TestExcutions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StatusSTTEID",
                table: "TestExcutions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestExcutions_StatusSTTEID",
                table: "TestExcutions",
                column: "StatusTESTTEID");

            migrationBuilder.AddForeignKey(
                name: "FK_TestExcutions_StatusTE_StatusSTTEID",
                table: "TestExcutions",
                column: "StatusSTTEID",
                principalTable: "StatusTE",
                principalColumn: "STTEID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestExcutions_StatusTE_StatusSTTEID",
                table: "TestExcutions");

            migrationBuilder.DropIndex(
                name: "IX_TestExcutions_StatusSTTEID",
                table: "TestExcutions");

            

            migrationBuilder.DropColumn(
                name: "StatusTESTTEID",
                table: "TestExcutions");
        }
    }
}
