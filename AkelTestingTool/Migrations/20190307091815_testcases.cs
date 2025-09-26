using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AkelTestingTool.Migrations
{
    public partial class Testcases : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestCases",
                columns: table => new
                {
                    TCId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExpectedResult = table.Column<string>(maxLength: 5000, nullable: false),
                    PPublicationDate = table.Column<DateTime>(nullable: false),
                    ProjectTestsTId = table.Column<int>(nullable: false),
                    Result = table.Column<string>(maxLength: 5000, nullable: false),
                    TestCase = table.Column<string>(maxLength: 5000, nullable: false),
                    TestCaseNum = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestCases", x => x.TCId);
                    table.ForeignKey(
                        name: "FK_TestCases_ProjectTests_ProjectTestsTId",
                        column: x => x.ProjectTestsTId,
                        principalTable: "ProjectTests",
                        principalColumn: "TId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestCases_ProjectTestsTId",
                table: "TestCases",
                column: "ProjectTestsTId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestCases");
        }
    }
}
