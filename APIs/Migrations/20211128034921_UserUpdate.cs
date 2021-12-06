using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace APIs.Migrations
{
    public partial class UserUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ANCESTRY_IDENTITY",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ancestry = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ANCESTRY_IDENTITY", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GENDER",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GENDER", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NATIONALITY",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NATIONALITY", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PERSON",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Othername = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PhoneNo = table.Column<byte[]>(type: "varbinary(50)", maxLength: 50, nullable: true),
                    Email = table.Column<byte[]>(type: "varbinary(50)", maxLength: 50, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERSON", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PLATFORM_DISCOVERY_TYPE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PLATFORM_DISCOVERY_TYPE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RESPONSE_TYPE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Response = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RESPONSE_TYPE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ROLE",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SECURITY_QUESTIONS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SECURITY_QUESTIONS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SURVEY_CATEGORY",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SURVEY_CATEGORY", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "USER",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    IsAnsweredSecurityQuestion = table.Column<bool>(type: "bit", nullable: false),
                    IsUpdatedProfile = table.Column<bool>(type: "bit", nullable: false),
                    Guid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SignUpDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PersonId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER", x => x.Id);
                    table.ForeignKey(
                        name: "FK_USER_PERSON_PersonId",
                        column: x => x.PersonId,
                        principalTable: "PERSON",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_USER_ROLE_RoleId",
                        column: x => x.RoleId,
                        principalTable: "ROLE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SURVEY_SUB_CATEGORY",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SurveyCategoryId = table.Column<int>(type: "int", nullable: false),
                    TimeAllowed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SURVEY_SUB_CATEGORY", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SURVEY_SUB_CATEGORY_SURVEY_CATEGORY_SurveyCategoryId",
                        column: x => x.SurveyCategoryId,
                        principalTable: "SURVEY_CATEGORY",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "USER_FRINGE_DETAILS",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    GenderId = table.Column<long>(type: "bigint", nullable: true),
                    IsTextMessageContact = table.Column<bool>(type: "bit", nullable: false),
                    AncestryIdentityId = table.Column<int>(type: "int", nullable: true),
                    SalivaBloodSharingId = table.Column<int>(type: "int", nullable: true),
                    ClinicalTrialsId = table.Column<int>(type: "int", nullable: true),
                    NationalityId = table.Column<int>(type: "int", nullable: true),
                    PlatformDiscoveryTypeId = table.Column<int>(type: "int", nullable: true),
                    ReferalPersonName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Height = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_FRINGE_DETAILS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_USER_FRINGE_DETAILS_ANCESTRY_IDENTITY_AncestryIdentityId",
                        column: x => x.AncestryIdentityId,
                        principalTable: "ANCESTRY_IDENTITY",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_USER_FRINGE_DETAILS_GENDER_GenderId",
                        column: x => x.GenderId,
                        principalTable: "GENDER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_USER_FRINGE_DETAILS_NATIONALITY_NationalityId",
                        column: x => x.NationalityId,
                        principalTable: "NATIONALITY",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_USER_FRINGE_DETAILS_PLATFORM_DISCOVERY_TYPE_PlatformDiscoveryTypeId",
                        column: x => x.PlatformDiscoveryTypeId,
                        principalTable: "PLATFORM_DISCOVERY_TYPE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_USER_FRINGE_DETAILS_RESPONSE_TYPE_ClinicalTrialsId",
                        column: x => x.ClinicalTrialsId,
                        principalTable: "RESPONSE_TYPE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_USER_FRINGE_DETAILS_RESPONSE_TYPE_SalivaBloodSharingId",
                        column: x => x.SalivaBloodSharingId,
                        principalTable: "RESPONSE_TYPE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_USER_FRINGE_DETAILS_USER_UserId",
                        column: x => x.UserId,
                        principalTable: "USER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "USER_SECURITY_QUESTIONS",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SecurityQuestionId = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_SECURITY_QUESTIONS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_USER_SECURITY_QUESTIONS_SECURITY_QUESTIONS_SecurityQuestionId",
                        column: x => x.SecurityQuestionId,
                        principalTable: "SECURITY_QUESTIONS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_USER_SECURITY_QUESTIONS_USER_UserId",
                        column: x => x.UserId,
                        principalTable: "USER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SURVEY_QUESTIONS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SurveySubCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SURVEY_QUESTIONS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SURVEY_QUESTIONS_SURVEY_SUB_CATEGORY_SurveySubCategoryId",
                        column: x => x.SurveySubCategoryId,
                        principalTable: "SURVEY_SUB_CATEGORY",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "USER_SURVEY_SUBMISSION",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateSubmitted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SurveySubCategoryId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_SURVEY_SUBMISSION", x => x.Id);
                    table.ForeignKey(
                        name: "FK_USER_SURVEY_SUBMISSION_SURVEY_SUB_CATEGORY_SurveySubCategoryId",
                        column: x => x.SurveySubCategoryId,
                        principalTable: "SURVEY_SUB_CATEGORY",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_USER_SURVEY_SUBMISSION_USER_UserId",
                        column: x => x.UserId,
                        principalTable: "USER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SURVEY_QUESTION_OPTIONS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SurveyQuestionsId = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SURVEY_QUESTION_OPTIONS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SURVEY_QUESTION_OPTIONS_SURVEY_QUESTIONS_SurveyQuestionsId",
                        column: x => x.SurveyQuestionsId,
                        principalTable: "SURVEY_QUESTIONS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SURVEY_QUESTION_OPTION_SUB_OPTIONS",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SurveyQuestionOptionsId = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SURVEY_QUESTION_OPTION_SUB_OPTIONS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SURVEY_QUESTION_OPTION_SUB_OPTIONS_SURVEY_QUESTION_OPTIONS_SurveyQuestionOptionsId",
                        column: x => x.SurveyQuestionOptionsId,
                        principalTable: "SURVEY_QUESTION_OPTIONS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SURVEY_SELECTION_RESULT_LINKS",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SurveyQuestionSubOptionsId = table.Column<long>(type: "bigint", nullable: false),
                    VideoLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SURVEY_SELECTION_RESULT_LINKS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SURVEY_SELECTION_RESULT_LINKS_SURVEY_QUESTION_OPTION_SUB_OPTIONS_SurveyQuestionSubOptionsId",
                        column: x => x.SurveyQuestionSubOptionsId,
                        principalTable: "SURVEY_QUESTION_OPTION_SUB_OPTIONS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "USER_SURVEY_RESPONSE",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SurveyQuestionSubOptionsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_SURVEY_RESPONSE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_USER_SURVEY_RESPONSE_SURVEY_QUESTION_OPTION_SUB_OPTIONS_SurveyQuestionSubOptionsId",
                        column: x => x.SurveyQuestionSubOptionsId,
                        principalTable: "SURVEY_QUESTION_OPTION_SUB_OPTIONS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_USER_SURVEY_RESPONSE_USER_UserId",
                        column: x => x.UserId,
                        principalTable: "USER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SURVEY_QUESTION_OPTION_SUB_OPTIONS_SurveyQuestionOptionsId",
                table: "SURVEY_QUESTION_OPTION_SUB_OPTIONS",
                column: "SurveyQuestionOptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_SURVEY_QUESTION_OPTIONS_SurveyQuestionsId",
                table: "SURVEY_QUESTION_OPTIONS",
                column: "SurveyQuestionsId");

            migrationBuilder.CreateIndex(
                name: "IX_SURVEY_QUESTIONS_SurveySubCategoryId",
                table: "SURVEY_QUESTIONS",
                column: "SurveySubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SURVEY_SELECTION_RESULT_LINKS_SurveyQuestionSubOptionsId",
                table: "SURVEY_SELECTION_RESULT_LINKS",
                column: "SurveyQuestionSubOptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_SURVEY_SUB_CATEGORY_SurveyCategoryId",
                table: "SURVEY_SUB_CATEGORY",
                column: "SurveyCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_USER_PersonId",
                table: "USER",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_USER_RoleId",
                table: "USER",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_USER_FRINGE_DETAILS_AncestryIdentityId",
                table: "USER_FRINGE_DETAILS",
                column: "AncestryIdentityId");

            migrationBuilder.CreateIndex(
                name: "IX_USER_FRINGE_DETAILS_ClinicalTrialsId",
                table: "USER_FRINGE_DETAILS",
                column: "ClinicalTrialsId");

            migrationBuilder.CreateIndex(
                name: "IX_USER_FRINGE_DETAILS_GenderId",
                table: "USER_FRINGE_DETAILS",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_USER_FRINGE_DETAILS_NationalityId",
                table: "USER_FRINGE_DETAILS",
                column: "NationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_USER_FRINGE_DETAILS_PlatformDiscoveryTypeId",
                table: "USER_FRINGE_DETAILS",
                column: "PlatformDiscoveryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_USER_FRINGE_DETAILS_SalivaBloodSharingId",
                table: "USER_FRINGE_DETAILS",
                column: "SalivaBloodSharingId");

            migrationBuilder.CreateIndex(
                name: "IX_USER_FRINGE_DETAILS_UserId",
                table: "USER_FRINGE_DETAILS",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_USER_SECURITY_QUESTIONS_SecurityQuestionId",
                table: "USER_SECURITY_QUESTIONS",
                column: "SecurityQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_USER_SECURITY_QUESTIONS_UserId",
                table: "USER_SECURITY_QUESTIONS",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_USER_SURVEY_RESPONSE_SurveyQuestionSubOptionsId",
                table: "USER_SURVEY_RESPONSE",
                column: "SurveyQuestionSubOptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_USER_SURVEY_RESPONSE_UserId",
                table: "USER_SURVEY_RESPONSE",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_USER_SURVEY_SUBMISSION_SurveySubCategoryId",
                table: "USER_SURVEY_SUBMISSION",
                column: "SurveySubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_USER_SURVEY_SUBMISSION_UserId",
                table: "USER_SURVEY_SUBMISSION",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SURVEY_SELECTION_RESULT_LINKS");

            migrationBuilder.DropTable(
                name: "USER_FRINGE_DETAILS");

            migrationBuilder.DropTable(
                name: "USER_SECURITY_QUESTIONS");

            migrationBuilder.DropTable(
                name: "USER_SURVEY_RESPONSE");

            migrationBuilder.DropTable(
                name: "USER_SURVEY_SUBMISSION");

            migrationBuilder.DropTable(
                name: "ANCESTRY_IDENTITY");

            migrationBuilder.DropTable(
                name: "GENDER");

            migrationBuilder.DropTable(
                name: "NATIONALITY");

            migrationBuilder.DropTable(
                name: "PLATFORM_DISCOVERY_TYPE");

            migrationBuilder.DropTable(
                name: "RESPONSE_TYPE");

            migrationBuilder.DropTable(
                name: "SECURITY_QUESTIONS");

            migrationBuilder.DropTable(
                name: "SURVEY_QUESTION_OPTION_SUB_OPTIONS");

            migrationBuilder.DropTable(
                name: "USER");

            migrationBuilder.DropTable(
                name: "SURVEY_QUESTION_OPTIONS");

            migrationBuilder.DropTable(
                name: "PERSON");

            migrationBuilder.DropTable(
                name: "ROLE");

            migrationBuilder.DropTable(
                name: "SURVEY_QUESTIONS");

            migrationBuilder.DropTable(
                name: "SURVEY_SUB_CATEGORY");

            migrationBuilder.DropTable(
                name: "SURVEY_CATEGORY");
        }
    }
}
