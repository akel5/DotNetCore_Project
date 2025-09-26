using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AkelTestingTool.Migrations
{
    public partial class _3three : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestsExeResults",
                columns: table => new
                {
                    TERId2 = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PPublicationDate = table.Column<DateTime>(nullable: false),
                    ProjectTestsTId = table.Column<int>(nullable: true),
                    Result = table.Column<string>(maxLength: 5000, nullable: false),
                    TestCasesTCId = table.Column<int>(nullable: true),
                    TestExcutionsTEId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestsExeResults", x => x.TERId2);
                    table.ForeignKey(
                        name: "FK_TestsExeResults_ProjectTests_ProjectTestsTId",
                        column: x => x.ProjectTestsTId,
                        principalTable: "ProjectTests",
                        principalColumn: "TId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestsExeResults_TestCases_TestCasesTCId",
                        column: x => x.TestCasesTCId,
                        principalTable: "TestCases",
                        principalColumn: "TCId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestsExeResults_TestExcutions_TestExcutionsTEId",
                        column: x => x.TestExcutionsTEId,
                        principalTable: "TestExcutions",
                        principalColumn: "TEId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestsExeResults_ProjectTestsTId",
                table: "TestsExeResults",
                column: "ProjectTestsTId");

            migrationBuilder.CreateIndex(
                name: "IX_TestsExeResults_TestCasesTCId",
                table: "TestsExeResults",
                column: "TestCasesTCId");

            migrationBuilder.CreateIndex(
                name: "IX_TestsExeResults_TestExcutionsTEId",
                table: "TestsExeResults",
                column: "TestExcutionsTEId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestsExeResults");
        }
    }
}
