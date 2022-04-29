using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MartinDrozdik.Data.DbContexts.Migrations.AppDbMigrations
{
    public partial class AddProjectPreviewImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PreviewImageId",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProjectPreviewImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastEditAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Uploaded = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlternativeText = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectPreviewImages", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_PreviewImageId",
                table: "Projects",
                column: "PreviewImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_ProjectPreviewImages_PreviewImageId",
                table: "Projects",
                column: "PreviewImageId",
                principalTable: "ProjectPreviewImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_ProjectPreviewImages_PreviewImageId",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "ProjectPreviewImages");

            migrationBuilder.DropIndex(
                name: "IX_Projects_PreviewImageId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "PreviewImageId",
                table: "Projects");
        }
    }
}
