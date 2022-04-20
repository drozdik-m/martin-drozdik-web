using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MartinDrozdik.Data.DbContexts.Migrations.AppDbMigrations
{
    public partial class AddOgProjectImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LogoId",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OgImageId",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProjectLogos",
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
                    table.PrimaryKey("PK_ProjectLogos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectOgImages",
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
                    table.PrimaryKey("PK_ProjectOgImages", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_LogoId",
                table: "Projects",
                column: "LogoId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_OgImageId",
                table: "Projects",
                column: "OgImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_ProjectLogos_LogoId",
                table: "Projects",
                column: "LogoId",
                principalTable: "ProjectLogos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_ProjectOgImages_OgImageId",
                table: "Projects",
                column: "OgImageId",
                principalTable: "ProjectOgImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_ProjectLogos_LogoId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_ProjectOgImages_OgImageId",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "ProjectLogos");

            migrationBuilder.DropTable(
                name: "ProjectOgImages");

            migrationBuilder.DropIndex(
                name: "IX_Projects_LogoId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_OgImageId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "LogoId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "OgImageId",
                table: "Projects");
        }
    }
}
