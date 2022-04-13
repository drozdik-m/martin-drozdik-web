using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MartinDrozdik.Data.DbContexts.Migrations.AppDbMigrations
{
    public partial class Morespecifyuserprofilepic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_PeopleImages_ProfilePictureId",
                table: "People");

            migrationBuilder.RenameColumn(
                name: "ProfilePictureId",
                table: "People",
                newName: "ProfileImageId");

            migrationBuilder.RenameIndex(
                name: "IX_People_ProfilePictureId",
                table: "People",
                newName: "IX_People_ProfileImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_People_PeopleImages_ProfileImageId",
                table: "People",
                column: "ProfileImageId",
                principalTable: "PeopleImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_PeopleImages_ProfileImageId",
                table: "People");

            migrationBuilder.RenameColumn(
                name: "ProfileImageId",
                table: "People",
                newName: "ProfilePictureId");

            migrationBuilder.RenameIndex(
                name: "IX_People_ProfileImageId",
                table: "People",
                newName: "IX_People_ProfilePictureId");

            migrationBuilder.AddForeignKey(
                name: "FK_People_PeopleImages_ProfilePictureId",
                table: "People",
                column: "ProfilePictureId",
                principalTable: "PeopleImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
