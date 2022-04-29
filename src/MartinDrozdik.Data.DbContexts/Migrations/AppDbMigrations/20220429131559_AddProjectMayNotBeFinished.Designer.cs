﻿// <auto-generated />
using System;
using Bonsai.DataPersistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MartinDrozdik.Data.DbContexts.Migrations.AppDbMigrations
{
    [DbContext(typeof(AppDb))]
    [Migration("20220429131559_AddProjectMayNotBeFinished")]
    partial class AddProjectMayNotBeFinished
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MartinDrozdik.Data.Models.People.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsMe")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastEditAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderIndex")
                        .HasColumnType("int");

                    b.Property<int>("ProfileImageId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProfileImageId");

                    b.ToTable("People");
                });

            modelBuilder.Entity("MartinDrozdik.Data.Models.People.PersonProfileImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AlternativeText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTime>("LastEditAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Uploaded")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("PeopleProfileImages");
                });

            modelBuilder.Entity("MartinDrozdik.Data.Models.Projects.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Abstract")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FinishedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("GitHubLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HasGitHubLink")
                        .HasColumnType("bit");

                    b.Property<bool>("HasLiveLink")
                        .HasColumnType("bit");

                    b.Property<bool>("IsFinished")
                        .HasColumnType("bit");

                    b.Property<bool>("IsHidden")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastEditAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("LiveLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LogoId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OgImageId")
                        .HasColumnType("int");

                    b.Property<int>("OrderIndex")
                        .HasColumnType("int");

                    b.Property<int>("PreviewImageId")
                        .HasColumnType("int");

                    b.Property<int?>("ProjectTagId")
                        .HasColumnType("int");

                    b.Property<string>("UrlName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LogoId");

                    b.HasIndex("OgImageId");

                    b.HasIndex("PreviewImageId");

                    b.HasIndex("ProjectTagId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("MartinDrozdik.Data.Models.Projects.ProjectDeveloper", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastEditAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("OrderIndex")
                        .HasColumnType("int");

                    b.Property<int>("PersonsId")
                        .HasColumnType("int");

                    b.Property<int>("ProjectsId")
                        .HasColumnType("int");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PersonsId");

                    b.HasIndex("ProjectsId");

                    b.ToTable("ProjectDevelopers");
                });

            modelBuilder.Entity("MartinDrozdik.Data.Models.Projects.ProjectHasTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastEditAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProjectsId")
                        .HasColumnType("int");

                    b.Property<int>("TagsId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProjectsId");

                    b.HasIndex("TagsId");

                    b.ToTable("ProjectHasTags");
                });

            modelBuilder.Entity("MartinDrozdik.Data.Models.Projects.ProjectLogo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AlternativeText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTime>("LastEditAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Uploaded")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("ProjectLogos");
                });

            modelBuilder.Entity("MartinDrozdik.Data.Models.Projects.ProjectOgImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AlternativeText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTime>("LastEditAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Uploaded")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("ProjectOgImages");
                });

            modelBuilder.Entity("MartinDrozdik.Data.Models.Projects.ProjectPreviewImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AlternativeText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTime>("LastEditAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Uploaded")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("ProjectPreviewImages");
                });

            modelBuilder.Entity("MartinDrozdik.Data.Models.Projects.ProjectTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastEditAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderIndex")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ProjectTags");
                });

            modelBuilder.Entity("MartinDrozdik.Data.Models.Projects.ProjectTechnology", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastEditAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("OrderIndex")
                        .HasColumnType("int");

                    b.Property<int>("ProjectsId")
                        .HasColumnType("int");

                    b.Property<int>("TechnologiesId")
                        .HasColumnType("int");

                    b.Property<string>("Usage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProjectsId");

                    b.HasIndex("TechnologiesId");

                    b.ToTable("ProjectTechnology");
                });

            modelBuilder.Entity("MartinDrozdik.Data.Models.Technologies.Technology", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastEditAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("LogoId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderIndex")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LogoId");

                    b.ToTable("Technologies");
                });

            modelBuilder.Entity("MartinDrozdik.Data.Models.Technologies.TechnologyLogo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AlternativeText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTime>("LastEditAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Uploaded")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("TechnologyLogos");
                });

            modelBuilder.Entity("MartinDrozdik.Data.Models.People.Person", b =>
                {
                    b.HasOne("MartinDrozdik.Data.Models.People.PersonProfileImage", "ProfileImage")
                        .WithMany()
                        .HasForeignKey("ProfileImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProfileImage");
                });

            modelBuilder.Entity("MartinDrozdik.Data.Models.Projects.Project", b =>
                {
                    b.HasOne("MartinDrozdik.Data.Models.Projects.ProjectLogo", "Logo")
                        .WithMany()
                        .HasForeignKey("LogoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MartinDrozdik.Data.Models.Projects.ProjectOgImage", "OgImage")
                        .WithMany()
                        .HasForeignKey("OgImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MartinDrozdik.Data.Models.Projects.ProjectPreviewImage", "PreviewImage")
                        .WithMany()
                        .HasForeignKey("PreviewImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MartinDrozdik.Data.Models.Projects.ProjectTag", null)
                        .WithMany("Projects")
                        .HasForeignKey("ProjectTagId");

                    b.Navigation("Logo");

                    b.Navigation("OgImage");

                    b.Navigation("PreviewImage");
                });

            modelBuilder.Entity("MartinDrozdik.Data.Models.Projects.ProjectDeveloper", b =>
                {
                    b.HasOne("MartinDrozdik.Data.Models.People.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MartinDrozdik.Data.Models.Projects.Project", "Project")
                        .WithMany("Developers")
                        .HasForeignKey("ProjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("MartinDrozdik.Data.Models.Projects.ProjectHasTag", b =>
                {
                    b.HasOne("MartinDrozdik.Data.Models.Projects.Project", "Project")
                        .WithMany("Tags")
                        .HasForeignKey("ProjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MartinDrozdik.Data.Models.Projects.ProjectTag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("MartinDrozdik.Data.Models.Projects.ProjectTechnology", b =>
                {
                    b.HasOne("MartinDrozdik.Data.Models.Projects.Project", "Project")
                        .WithMany("Technologies")
                        .HasForeignKey("ProjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MartinDrozdik.Data.Models.Technologies.Technology", "Technology")
                        .WithMany()
                        .HasForeignKey("TechnologiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("Technology");
                });

            modelBuilder.Entity("MartinDrozdik.Data.Models.Technologies.Technology", b =>
                {
                    b.HasOne("MartinDrozdik.Data.Models.Technologies.TechnologyLogo", "Logo")
                        .WithMany()
                        .HasForeignKey("LogoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Logo");
                });

            modelBuilder.Entity("MartinDrozdik.Data.Models.Projects.Project", b =>
                {
                    b.Navigation("Developers");

                    b.Navigation("Tags");

                    b.Navigation("Technologies");
                });

            modelBuilder.Entity("MartinDrozdik.Data.Models.Projects.ProjectTag", b =>
                {
                    b.Navigation("Projects");
                });
#pragma warning restore 612, 618
        }
    }
}
