using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MartinDrozdik.Data.DbContexts.Migrations.AppDbMigrations
{
    public partial class Addpeoplesimages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProfilePictureId",
                table: "People",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PeopleImages",
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
                    table.PrimaryKey("PK_PeopleImages", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_People_ProfilePictureId",
                table: "People",
                column: "ProfilePictureId");

            migrationBuilder.AddForeignKey(
                name: "FK_People_PeopleImages_ProfilePictureId",
                table: "People",
                column: "ProfilePictureId",
                principalTable: "PeopleImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_PeopleImages_ProfilePictureId",
                table: "People");

            migrationBuilder.DropTable(
                name: "PeopleImages");

            migrationBuilder.DropIndex(
                name: "IX_People_ProfilePictureId",
                table: "People");

            migrationBuilder.DropColumn(
                name: "ProfilePictureId",
                table: "People");
        }
    }
}
