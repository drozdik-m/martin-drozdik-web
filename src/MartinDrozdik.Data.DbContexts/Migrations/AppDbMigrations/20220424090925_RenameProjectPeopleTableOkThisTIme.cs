using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MartinDrozdik.Data.DbContexts.Migrations.AppDbMigrations
{
    public partial class RenameProjectPeopleTableOkThisTIme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectPeople_People_PersonsId",
                table: "ProjectPeople");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectPeople_Projects_ProjectsId",
                table: "ProjectPeople");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectPeople",
                table: "ProjectPeople");

            migrationBuilder.RenameTable(
                name: "ProjectPeople",
                newName: "ProjectDevelopers");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectPeople_ProjectsId",
                table: "ProjectDevelopers",
                newName: "IX_ProjectDevelopers_ProjectsId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectPeople_PersonsId",
                table: "ProjectDevelopers",
                newName: "IX_ProjectDevelopers_PersonsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectDevelopers",
                table: "ProjectDevelopers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectDevelopers_People_PersonsId",
                table: "ProjectDevelopers",
                column: "PersonsId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectDevelopers_Projects_ProjectsId",
                table: "ProjectDevelopers",
                column: "ProjectsId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectDevelopers_People_PersonsId",
                table: "ProjectDevelopers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectDevelopers_Projects_ProjectsId",
                table: "ProjectDevelopers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectDevelopers",
                table: "ProjectDevelopers");

            migrationBuilder.RenameTable(
                name: "ProjectDevelopers",
                newName: "ProjectPeople");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectDevelopers_ProjectsId",
                table: "ProjectPeople",
                newName: "IX_ProjectPeople_ProjectsId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectDevelopers_PersonsId",
                table: "ProjectPeople",
                newName: "IX_ProjectPeople_PersonsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectPeople",
                table: "ProjectPeople",
                column: "Id");

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
    }
}
