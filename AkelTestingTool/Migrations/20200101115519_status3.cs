using Microsoft.EntityFrameworkCore.Migrations;

namespace AkelTestingTool.Migrations
{
    public partial class status3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BugsSummary_Status_StatusSTID",
                table: "BugsSummary");

          //  migrationBuilder.DropColumn(
          //      name: "StatusSID",
          //      table: "BugsSummary");

         //   migrationBuilder.AlterColumn<int>(
        //        name: "StatusSTID",
        //        table: "BugsSummary",
          //      maxLength: 150,
           //     nullable: false,
           //     oldClrType: typeof(int),
           //     oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BugsSummary_Status_StatusSTID",
                table: "BugsSummary",
                column: "StatusSTID",
                principalTable: "Status",
                principalColumn: "STID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BugsSummary_Status_StatusSTID",
                table: "BugsSummary");

            migrationBuilder.AlterColumn<int>(
                name: "StatusSTID",
                table: "BugsSummary",
                nullable: true,
                oldClrType: typeof(int),
                oldMaxLength: 150);

            migrationBuilder.AddColumn<int>(
                name: "StatusSID",
                table: "BugsSummary",
                maxLength: 150,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_BugsSummary_Status_StatusSTID",
                table: "BugsSummary",
                column: "StatusSTID",
                principalTable: "Status",
                principalColumn: "STID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
