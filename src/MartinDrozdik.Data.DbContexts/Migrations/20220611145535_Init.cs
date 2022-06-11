using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MartinDrozdik.Data.DbContexts.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArticleMainImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderIndex = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastEditAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Uploaded = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlternativeText = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleMainImages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArticleTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastEditAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderIndex = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogMarkdownArticles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastEditAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Markdown = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HTML = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogMarkdownArticles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Educations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderIndex = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ended = table.Column<bool>(type: "bit", nullable: false),
                    EndedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastEditAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LanguageSkills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderIndex = table.Column<int>(type: "int", nullable: false),
                    LanguageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SkillLevel = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastEditAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageSkills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PeopleProfileImages",
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
                    table.PrimaryKey("PK_PeopleProfileImages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectLogos",
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
                    table.PrimaryKey("PK_ProjectLogos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectMarkdownArticles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastEditAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Markdown = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HTML = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectMarkdownArticles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectOgImages",
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
                    table.PrimaryKey("PK_ProjectOgImages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectPreviewImages",
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
                    table.PrimaryKey("PK_ProjectPreviewImages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastEditAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderIndex = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TechnologyLogos",
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
                    table.PrimaryKey("PK_TechnologyLogos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkExperiences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderIndex = table.Column<int>(type: "int", nullable: false),
                    WorkPosition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlaceOfWork = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlaceOfWorkHasWebsite = table.Column<bool>(type: "bit", nullable: false),
                    PlaceOfWorkUri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ended = table.Column<bool>(type: "bit", nullable: false),
                    EndedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastEditAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkExperiences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMe = table.Column<bool>(type: "bit", nullable: false),
                    OrderIndex = table.Column<int>(type: "int", nullable: false),
                    ProfileImageId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastEditAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                    table.ForeignKey(
                        name: "FK_People_PeopleProfileImages_ProfileImageId",
                        column: x => x.ProfileImageId,
                        principalTable: "PeopleProfileImages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderIndex = table.Column<int>(type: "int", nullable: false),
                    IsHidden = table.Column<bool>(type: "bit", nullable: false),
                    Abstract = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Keywords = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsFinished = table.Column<bool>(type: "bit", nullable: false),
                    FinishedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HasLiveLink = table.Column<bool>(type: "bit", nullable: false),
                    LiveLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasGitHubLink = table.Column<bool>(type: "bit", nullable: false),
                    GitHubLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogoId = table.Column<int>(type: "int", nullable: false),
                    OgImageId = table.Column<int>(type: "int", nullable: false),
                    PreviewImageId = table.Column<int>(type: "int", nullable: false),
                    ContentId = table.Column<int>(type: "int", nullable: false),
                    ProjectTagId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastEditAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_ProjectLogos_LogoId",
                        column: x => x.LogoId,
                        principalTable: "ProjectLogos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Projects_ProjectMarkdownArticles_ContentId",
                        column: x => x.ContentId,
                        principalTable: "ProjectMarkdownArticles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Projects_ProjectOgImages_OgImageId",
                        column: x => x.OgImageId,
                        principalTable: "ProjectOgImages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Projects_ProjectPreviewImages_PreviewImageId",
                        column: x => x.PreviewImageId,
                        principalTable: "ProjectPreviewImages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Projects_ProjectTags_ProjectTagId",
                        column: x => x.ProjectTagId,
                        principalTable: "ProjectTags",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Technologies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderIndex = table.Column<int>(type: "int", nullable: false),
                    LogoId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastEditAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technologies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Technologies_TechnologyLogos_LogoId",
                        column: x => x.LogoId,
                        principalTable: "TechnologyLogos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsHidden = table.Column<bool>(type: "bit", nullable: false),
                    Abstract = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Keywords = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsArticleReference = table.Column<bool>(type: "bit", nullable: false),
                    ReferenceLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MainImageId = table.Column<int>(type: "int", nullable: false),
                    ContentId = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: true),
                    ArticleTagId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastEditAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_ArticleMainImages_MainImageId",
                        column: x => x.MainImageId,
                        principalTable: "ArticleMainImages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Articles_ArticleTags_ArticleTagId",
                        column: x => x.ArticleTagId,
                        principalTable: "ArticleTags",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Articles_BlogMarkdownArticles_ContentId",
                        column: x => x.ContentId,
                        principalTable: "BlogMarkdownArticles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Articles_People_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "People",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectDevelopers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderIndex = table.Column<int>(type: "int", nullable: false),
                    ProjectsId = table.Column<int>(type: "int", nullable: false),
                    PersonsId = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastEditAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectDevelopers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectDevelopers_People_PersonsId",
                        column: x => x.PersonsId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectDevelopers_Projects_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectGalleryImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderIndex = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastEditAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Uploaded = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlternativeText = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectGalleryImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectGalleryImages_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectHasTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectsId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastEditAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectHasTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectHasTags_Projects_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectHasTags_ProjectTags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "ProjectTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTechnology",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderIndex = table.Column<int>(type: "int", nullable: false),
                    ProjectsId = table.Column<int>(type: "int", nullable: false),
                    TechnologiesId = table.Column<int>(type: "int", nullable: false),
                    Usage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastEditAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTechnology", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectTechnology_Projects_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectTechnology_Technologies_TechnologiesId",
                        column: x => x.TechnologiesId,
                        principalTable: "Technologies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArticleHasTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticlesId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastEditAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleHasTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleHasTags_Articles_ArticlesId",
                        column: x => x.ArticlesId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleHasTags_ArticleTags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "ArticleTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleHasTags_ArticlesId",
                table: "ArticleHasTags",
                column: "ArticlesId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleHasTags_TagsId",
                table: "ArticleHasTags",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ArticleTagId",
                table: "Articles",
                column: "ArticleTagId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_AuthorId",
                table: "Articles",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ContentId",
                table: "Articles",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_MainImageId",
                table: "Articles",
                column: "MainImageId");

            migrationBuilder.CreateIndex(
                name: "IX_People_ProfileImageId",
                table: "People",
                column: "ProfileImageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDevelopers_PersonsId",
                table: "ProjectDevelopers",
                column: "PersonsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDevelopers_ProjectsId",
                table: "ProjectDevelopers",
                column: "ProjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectGalleryImages_ProjectId",
                table: "ProjectGalleryImages",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHasTags_ProjectsId",
                table: "ProjectHasTags",
                column: "ProjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHasTags_TagsId",
                table: "ProjectHasTags",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ContentId",
                table: "Projects",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_LogoId",
                table: "Projects",
                column: "LogoId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_OgImageId",
                table: "Projects",
                column: "OgImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_PreviewImageId",
                table: "Projects",
                column: "PreviewImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectTagId",
                table: "Projects",
                column: "ProjectTagId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTechnology_ProjectsId",
                table: "ProjectTechnology",
                column: "ProjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTechnology_TechnologiesId",
                table: "ProjectTechnology",
                column: "TechnologiesId");

            migrationBuilder.CreateIndex(
                name: "IX_Technologies_LogoId",
                table: "Technologies",
                column: "LogoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleHasTags");

            migrationBuilder.DropTable(
                name: "Educations");

            migrationBuilder.DropTable(
                name: "LanguageSkills");

            migrationBuilder.DropTable(
                name: "ProjectDevelopers");

            migrationBuilder.DropTable(
                name: "ProjectGalleryImages");

            migrationBuilder.DropTable(
                name: "ProjectHasTags");

            migrationBuilder.DropTable(
                name: "ProjectTechnology");

            migrationBuilder.DropTable(
                name: "WorkExperiences");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Technologies");

            migrationBuilder.DropTable(
                name: "ArticleMainImages");

            migrationBuilder.DropTable(
                name: "ArticleTags");

            migrationBuilder.DropTable(
                name: "BlogMarkdownArticles");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "ProjectLogos");

            migrationBuilder.DropTable(
                name: "ProjectMarkdownArticles");

            migrationBuilder.DropTable(
                name: "ProjectOgImages");

            migrationBuilder.DropTable(
                name: "ProjectPreviewImages");

            migrationBuilder.DropTable(
                name: "ProjectTags");

            migrationBuilder.DropTable(
                name: "TechnologyLogos");

            migrationBuilder.DropTable(
                name: "PeopleProfileImages");
        }
    }
}
