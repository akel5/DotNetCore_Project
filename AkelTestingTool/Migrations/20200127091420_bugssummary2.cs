using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AkelTestingTool.Migrations
{
    public partial class bugssummary2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BugsSummary2",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProjectsPId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Bug = table.Column<string>(maxLength: 100, nullable: false),
                    BugSummary = table.Column<string>(maxLength: 5000, nullable: false),
                    PublicationDate = table.Column<DateTime>(nullable: false),
                    TesterName = table.Column<string>(maxLength: 150, nullable: true),
                    Readers = table.Column<int>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    StatusSTID = table.Column<int>(maxLength: 150, nullable: false),
                    TestsExeResultsTERID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BugsSummary2", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BugsSummary2_Projects_ProjectsPId",
                        column: x => x.ProjectsPId,
                        principalTable: "Projects",
                        principalColumn: "PId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BugsSummary2_Status_StatusSTID",
                        column: x => x.StatusSTID,
                        principalTable: "Status",
                        principalColumn: "STID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BugsSummary2_TestsExeResults_TestsExeResultsTERID",
                        column: x => x.TestsExeResultsTERID,
                        principalTable: "TestsExeResults",
                        principalColumn: "TERId2",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BugsSummary2_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BugsSummary2_ProjectsPId",
                table: "BugsSummary2",
                column: "ProjectsPId");

            migrationBuilder.CreateIndex(
                name: "IX_BugsSummary2_StatusSTID",
                table: "BugsSummary2",
                column: "StatusSTID");

            migrationBuilder.CreateIndex(
                name: "IX_BugsSummary2_TestsExeResultsTERID",
                table: "BugsSummary2",
                column: "TestsExeResultsTERID");

            migrationBuilder.CreateIndex(
                name: "IX_BugsSummary2_UserId",
                table: "BugsSummary2",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BugsSummary2");
        }
    }
}
