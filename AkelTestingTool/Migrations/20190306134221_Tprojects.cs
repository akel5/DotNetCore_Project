using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AkelTestingTool.Migrations
{
    public partial class Tprojects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectTests",
                columns: table => new
                {
                    TId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PPublicationDate = table.Column<DateTime>(nullable: false),
                    ProjectsPId = table.Column<int>(nullable: false),
                    TStatus = table.Column<string>(maxLength: 5000, nullable: false),
                    Test = table.Column<string>(maxLength: 5000, nullable: false),
                    TestedBy = table.Column<string>(maxLength: 5000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTests", x => x.TId);
                    table.ForeignKey(
                        name: "FK_ProjectTests_Projects_ProjectsPId",
                        column: x => x.ProjectsPId,
                        principalTable: "Projects",
                        principalColumn: "PId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTests_ProjectsPId",
                table: "ProjectTests",
                column: "ProjectsPId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectTests");
        }
    }
}
