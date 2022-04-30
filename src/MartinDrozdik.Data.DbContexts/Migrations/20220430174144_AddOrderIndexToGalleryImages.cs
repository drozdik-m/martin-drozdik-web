using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MartinDrozdik.Data.DbContexts.Migrations
{
    public partial class AddOrderIndexToGalleryImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectGalleryImages_Projects_ProjectId",
                table: "ProjectGalleryImages");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "ProjectGalleryImages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderIndex",
                table: "ProjectGalleryImages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectGalleryImages_Projects_ProjectId",
                table: "ProjectGalleryImages",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectGalleryImages_Projects_ProjectId",
                table: "ProjectGalleryImages");

            migrationBuilder.DropColumn(
                name: "OrderIndex",
                table: "ProjectGalleryImages");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "ProjectGalleryImages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectGalleryImages_Projects_ProjectId",
                table: "ProjectGalleryImages",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }
    }
}
