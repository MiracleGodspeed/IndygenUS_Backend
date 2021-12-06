﻿// <auto-generated />
using System;
using DataLayer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace APIs.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20211128034921_UserUpdate")]
    partial class UserUpdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataLayer.Model.AncestryIdentity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Ancestry")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("ANCESTRY_IDENTITY");
                });

            modelBuilder.Entity("DataLayer.Model.Gender", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("GENDER");
                });

            modelBuilder.Entity("DataLayer.Model.Nationality", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("NATIONALITY");
                });

            modelBuilder.Entity("DataLayer.Model.Person", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<byte[]>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("varbinary(50)");

                    b.Property<string>("Firstname")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Othername")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<byte[]>("PhoneNo")
                        .HasMaxLength(50)
                        .HasColumnType("varbinary(50)");

                    b.Property<string>("Surname")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("PERSON");
                });

            modelBuilder.Entity("DataLayer.Model.PlatformDiscoveryType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("PLATFORM_DISCOVERY_TYPE");
                });

            modelBuilder.Entity("DataLayer.Model.ResponseType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Response")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("RESPONSE_TYPE");
                });

            modelBuilder.Entity("DataLayer.Model.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("ROLE");
                });

            modelBuilder.Entity("DataLayer.Model.SecurityQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Question")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.ToTable("SECURITY_QUESTIONS");
                });

            modelBuilder.Entity("DataLayer.Model.Submission", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateSubmitted")
                        .HasColumnType("datetime2");

                    b.Property<long>("SurveySubCategoryId")
                        .HasColumnType("bigint");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("SurveySubCategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("USER_SURVEY_SUBMISSION");
                });

            modelBuilder.Entity("DataLayer.Model.SurveyCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("ClassName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("SURVEY_CATEGORY");
                });

            modelBuilder.Entity("DataLayer.Model.SurveyQuestionOptions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("SurveyQuestionsId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SurveyQuestionsId");

                    b.ToTable("SURVEY_QUESTION_OPTIONS");
                });

            modelBuilder.Entity("DataLayer.Model.SurveyQuestionSubOptions", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int>("SurveyQuestionOptionsId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SurveyQuestionOptionsId");

                    b.ToTable("SURVEY_QUESTION_OPTION_SUB_OPTIONS");
                });

            modelBuilder.Entity("DataLayer.Model.SurveyQuestions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<long>("SurveySubCategoryId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("SurveySubCategoryId");

                    b.ToTable("SURVEY_QUESTIONS");
                });

            modelBuilder.Entity("DataLayer.Model.SurveySelectionResultLinks", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<long>("SurveyQuestionSubOptionsId")
                        .HasColumnType("bigint");

                    b.Property<string>("VideoLink")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SurveyQuestionSubOptionsId");

                    b.ToTable("SURVEY_SELECTION_RESULT_LINKS");
                });

            modelBuilder.Entity("DataLayer.Model.SurveySubCategory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int>("SurveyCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("TimeAllowed")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SurveyCategoryId");

                    b.ToTable("SURVEY_SUB_CATEGORY");
                });

            modelBuilder.Entity("DataLayer.Model.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Guid")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAnsweredSecurityQuestion")
                        .HasColumnType("bit");

                    b.Property<bool>("IsUpdatedProfile")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastLogin")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<long>("PersonId")
                        .HasColumnType("bigint");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("SignUpDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.HasIndex("RoleId");

                    b.ToTable("USER");
                });

            modelBuilder.Entity("DataLayer.Model.UserFringeDetails", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AncestryIdentityId")
                        .HasColumnType("int");

                    b.Property<int?>("ClinicalTrialsId")
                        .HasColumnType("int");

                    b.Property<long?>("GenderId")
                        .HasColumnType("bigint");

                    b.Property<string>("Height")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsTextMessageContact")
                        .HasColumnType("bit");

                    b.Property<int?>("NationalityId")
                        .HasColumnType("int");

                    b.Property<int?>("PlatformDiscoveryTypeId")
                        .HasColumnType("int");

                    b.Property<string>("ReferalPersonName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SalivaBloodSharingId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Weight")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AncestryIdentityId");

                    b.HasIndex("ClinicalTrialsId");

                    b.HasIndex("GenderId");

                    b.HasIndex("NationalityId");

                    b.HasIndex("PlatformDiscoveryTypeId");

                    b.HasIndex("SalivaBloodSharingId");

                    b.HasIndex("UserId");

                    b.ToTable("USER_FRINGE_DETAILS");
                });

            modelBuilder.Entity("DataLayer.Model.UserSecurityQuestions", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int>("SecurityQuestionId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("SecurityQuestionId");

                    b.HasIndex("UserId");

                    b.ToTable("USER_SECURITY_QUESTIONS");
                });

            modelBuilder.Entity("DataLayer.Model.UserSurveyResponse", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("SurveyQuestionSubOptionsId")
                        .HasColumnType("bigint");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("SurveyQuestionSubOptionsId");

                    b.HasIndex("UserId");

                    b.ToTable("USER_SURVEY_RESPONSE");
                });

            modelBuilder.Entity("DataLayer.Model.Submission", b =>
                {
                    b.HasOne("DataLayer.Model.SurveySubCategory", "SurveySubCategory")
                        .WithMany()
                        .HasForeignKey("SurveySubCategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataLayer.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("SurveySubCategory");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataLayer.Model.SurveyQuestionOptions", b =>
                {
                    b.HasOne("DataLayer.Model.SurveyQuestions", "SurveyQuestions")
                        .WithMany()
                        .HasForeignKey("SurveyQuestionsId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("SurveyQuestions");
                });

            modelBuilder.Entity("DataLayer.Model.SurveyQuestionSubOptions", b =>
                {
                    b.HasOne("DataLayer.Model.SurveyQuestionOptions", "SurveyQuestionOptions")
                        .WithMany()
                        .HasForeignKey("SurveyQuestionOptionsId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("SurveyQuestionOptions");
                });

            modelBuilder.Entity("DataLayer.Model.SurveyQuestions", b =>
                {
                    b.HasOne("DataLayer.Model.SurveySubCategory", "SurveySubCategory")
                        .WithMany()
                        .HasForeignKey("SurveySubCategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("SurveySubCategory");
                });

            modelBuilder.Entity("DataLayer.Model.SurveySelectionResultLinks", b =>
                {
                    b.HasOne("DataLayer.Model.SurveyQuestionSubOptions", "SurveyQuestionSubOptions")
                        .WithMany()
                        .HasForeignKey("SurveyQuestionSubOptionsId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("SurveyQuestionSubOptions");
                });

            modelBuilder.Entity("DataLayer.Model.SurveySubCategory", b =>
                {
                    b.HasOne("DataLayer.Model.SurveyCategory", "SurveyCategory")
                        .WithMany()
                        .HasForeignKey("SurveyCategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("SurveyCategory");
                });

            modelBuilder.Entity("DataLayer.Model.User", b =>
                {
                    b.HasOne("DataLayer.Model.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataLayer.Model.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Person");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("DataLayer.Model.UserFringeDetails", b =>
                {
                    b.HasOne("DataLayer.Model.AncestryIdentity", "AncestryIdentity")
                        .WithMany()
                        .HasForeignKey("AncestryIdentityId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DataLayer.Model.ResponseType", "ClinicalTrials")
                        .WithMany()
                        .HasForeignKey("ClinicalTrialsId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DataLayer.Model.Gender", "Gender")
                        .WithMany()
                        .HasForeignKey("GenderId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DataLayer.Model.Nationality", "Nationality")
                        .WithMany()
                        .HasForeignKey("NationalityId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DataLayer.Model.PlatformDiscoveryType", "PlatformDiscoveryType")
                        .WithMany()
                        .HasForeignKey("PlatformDiscoveryTypeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DataLayer.Model.ResponseType", "SalivaBloodSharing")
                        .WithMany()
                        .HasForeignKey("SalivaBloodSharingId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DataLayer.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("AncestryIdentity");

                    b.Navigation("ClinicalTrials");

                    b.Navigation("Gender");

                    b.Navigation("Nationality");

                    b.Navigation("PlatformDiscoveryType");

                    b.Navigation("SalivaBloodSharing");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataLayer.Model.UserSecurityQuestions", b =>
                {
                    b.HasOne("DataLayer.Model.SecurityQuestion", "SecurityQuestion")
                        .WithMany()
                        .HasForeignKey("SecurityQuestionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataLayer.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("SecurityQuestion");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataLayer.Model.UserSurveyResponse", b =>
                {
                    b.HasOne("DataLayer.Model.SurveyQuestionSubOptions", "SurveyQuestionSubOptions")
                        .WithMany()
                        .HasForeignKey("SurveyQuestionSubOptionsId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataLayer.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("SurveyQuestionSubOptions");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
