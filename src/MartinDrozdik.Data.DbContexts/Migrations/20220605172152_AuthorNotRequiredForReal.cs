using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MartinDrozdik.Data.DbContexts.Migrations
{
    public partial class AuthorNotRequiredForReal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_People_AuthorId",
                table: "Articles");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Articles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_People_AuthorId",
                table: "Articles",
                column: "AuthorId",
                principalTable: "People",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_People_AuthorId",
                table: "Articles");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Articles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_People_AuthorId",
                table: "Articles",
                column: "AuthorId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
