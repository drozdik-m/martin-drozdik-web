using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MartinDrozdik.Data.DbContexts.Migrations
{
    public partial class ArticleRemoveOrderAndFixTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectHasTags_Articles_ArticleId",
                table: "ProjectHasTags");

            migrationBuilder.DropIndex(
                name: "IX_ProjectHasTags_ArticleId",
                table: "ProjectHasTags");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "ProjectHasTags");

            migrationBuilder.DropColumn(
                name: "OrderIndex",
                table: "Articles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArticleId",
                table: "ProjectHasTags",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderIndex",
                table: "Articles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHasTags_ArticleId",
                table: "ProjectHasTags",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectHasTags_Articles_ArticleId",
                table: "ProjectHasTags",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id");
        }
    }
}
