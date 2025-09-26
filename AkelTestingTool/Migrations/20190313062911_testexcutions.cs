using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AkelTestingTool.Migrations
{
    public partial class testexcutions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestExcutions",
                columns: table => new
                {
                    TEId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PPublicationDate = table.Column<DateTime>(nullable: false),
                    ProjectTestsTId = table.Column<int>(nullable: false),
                    TStatus = table.Column<string>(maxLength: 5000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestExcutions", x => x.TEId);
                    table.ForeignKey(
                        name: "FK_TestExcutions_ProjectTests_ProjectTestsTId",
                        column: x => x.ProjectTestsTId,
                        principalTable: "ProjectTests",
                        principalColumn: "TId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestExcutions_ProjectTestsTId",
                table: "TestExcutions",
                column: "ProjectTestsTId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestExcutions");
        }
    }
}
