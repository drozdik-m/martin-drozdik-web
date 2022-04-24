using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MartinDrozdik.Data.DbContexts.Migrations.AppDbMigrations
{
    public partial class FixProjectPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectPeople_Projects_ProjectId",
                table: "ProjectPeople");

            migrationBuilder.DropIndex(
                name: "IX_ProjectPeople_ProjectId",
                table: "ProjectPeople");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "ProjectPeople");

            migrationBuilder.AddColumn<int>(
                name: "PersonsId",
                table: "ProjectPeople",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProjectsId",
                table: "ProjectPeople",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPeople_PersonsId",
                table: "ProjectPeople",
                column: "PersonsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPeople_ProjectsId",
                table: "ProjectPeople",
                column: "ProjectsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectPeople_People_PersonsId",
                table: "ProjectPeople",
                column: "PersonsId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectPeople_Projects_ProjectsId",
                table: "ProjectPeople",
                column: "ProjectsId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectPeople_People_PersonsId",
                table: "ProjectPeople");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectPeople_Projects_ProjectsId",
                table: "ProjectPeople");

            migrationBuilder.DropIndex(
                name: "IX_ProjectPeople_PersonsId",
                table: "ProjectPeople");

            migrationBuilder.DropIndex(
                name: "IX_ProjectPeople_ProjectsId",
                table: "ProjectPeople");

            migrationBuilder.DropColumn(
                name: "PersonsId",
                table: "ProjectPeople");

            migrationBuilder.DropColumn(
                name: "ProjectsId",
                table: "ProjectPeople");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "ProjectPeople",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPeople_ProjectId",
                table: "ProjectPeople",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectPeople_Projects_ProjectId",
                table: "ProjectPeople",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }
    }
}
