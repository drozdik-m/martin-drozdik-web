using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MartinDrozdik.Data.DbContexts.Migrations.AppDbMigrations
{
    public partial class MakeCustomTagConnector : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectProjectTag");

            migrationBuilder.AddColumn<int>(
                name: "ProjectTagId",
                table: "Projects",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProjectHasTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectsId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastEditAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectHasTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectHasTags_Projects_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectHasTags_ProjectTags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "ProjectTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectTagId",
                table: "Projects",
                column: "ProjectTagId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHasTags_ProjectsId",
                table: "ProjectHasTags",
                column: "ProjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHasTags_TagsId",
                table: "ProjectHasTags",
                column: "TagsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_ProjectTags_ProjectTagId",
                table: "Projects",
                column: "ProjectTagId",
                principalTable: "ProjectTags",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_ProjectTags_ProjectTagId",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "ProjectHasTags");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ProjectTagId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectTagId",
                table: "Projects");

            migrationBuilder.CreateTable(
                name: "ProjectProjectTag",
                columns: table => new
                {
                    ProjectsId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectProjectTag", x => new { x.ProjectsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_ProjectProjectTag_Projects_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectProjectTag_ProjectTags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "ProjectTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProjectTag_TagsId",
                table: "ProjectProjectTag",
                column: "TagsId");
        }
    }
}
