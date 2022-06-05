using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MartinDrozdik.Data.DbContexts.Migrations
{
    public partial class RenameArticleToContent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_ProjectMarkdownArticles_ArticleId",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "ArticleId",
                table: "Projects",
                newName: "ContentId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_ArticleId",
                table: "Projects",
                newName: "IX_Projects_ContentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_ProjectMarkdownArticles_ContentId",
                table: "Projects",
                column: "ContentId",
                principalTable: "ProjectMarkdownArticles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_ProjectMarkdownArticles_ContentId",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "ContentId",
                table: "Projects",
                newName: "ArticleId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_ContentId",
                table: "Projects",
                newName: "IX_Projects_ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_ProjectMarkdownArticles_ArticleId",
                table: "Projects",
                column: "ArticleId",
                principalTable: "ProjectMarkdownArticles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
