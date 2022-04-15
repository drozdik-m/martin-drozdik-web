using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MartinDrozdik.Data.DbContexts.Migrations.AppDbMigrations
{
    public partial class RenameProfileImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_PeopleImages_ProfileImageId",
                table: "People");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PeopleImages",
                table: "PeopleImages");

            migrationBuilder.RenameTable(
                name: "PeopleImages",
                newName: "PeopleProfileImages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PeopleProfileImages",
                table: "PeopleProfileImages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_People_PeopleProfileImages_ProfileImageId",
                table: "People",
                column: "ProfileImageId",
                principalTable: "PeopleProfileImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_PeopleProfileImages_ProfileImageId",
                table: "People");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PeopleProfileImages",
                table: "PeopleProfileImages");

            migrationBuilder.RenameTable(
                name: "PeopleProfileImages",
                newName: "PeopleImages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PeopleImages",
                table: "PeopleImages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_People_PeopleImages_ProfileImageId",
                table: "People",
                column: "ProfileImageId",
                principalTable: "PeopleImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
