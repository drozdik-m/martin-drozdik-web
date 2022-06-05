using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MartinDrozdik.Data.DbContexts.Migrations
{
    public partial class ArticlesCanBeLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsArticleReference",
                table: "Articles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ReferenceLink",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsArticleReference",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "ReferenceLink",
                table: "Articles");
        }
    }
}
