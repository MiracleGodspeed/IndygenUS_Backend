using Microsoft.EntityFrameworkCore.Migrations;

namespace APIs.Migrations
{
    public partial class SurveySubOptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SURVEY_QUESTION_OPTION_SUB_OPTIONS_SURVEY_QUESTION_OPTIONS_SurveyQuestionOptionsId",
                table: "SURVEY_QUESTION_OPTION_SUB_OPTIONS");

            migrationBuilder.DropForeignKey(
                name: "FK_SURVEY_SELECTION_RESULT_LINKS_SURVEY_QUESTION_OPTION_SUB_OPTIONS_SurveyQuestionSubOptionsId",
                table: "SURVEY_SELECTION_RESULT_LINKS");

            migrationBuilder.DropForeignKey(
                name: "FK_USER_SURVEY_RESPONSE_SURVEY_QUESTION_OPTION_SUB_OPTIONS_SurveyQuestionSubOptionsId",
                table: "USER_SURVEY_RESPONSE");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SURVEY_QUESTION_OPTION_SUB_OPTIONS",
                table: "SURVEY_QUESTION_OPTION_SUB_OPTIONS");

            migrationBuilder.RenameTable(
                name: "SURVEY_QUESTION_OPTION_SUB_OPTIONS",
                newName: "SURVEY_QUESTION_SUB_OPTIONS");

            migrationBuilder.RenameIndex(
                name: "IX_SURVEY_QUESTION_OPTION_SUB_OPTIONS_SurveyQuestionOptionsId",
                table: "SURVEY_QUESTION_SUB_OPTIONS",
                newName: "IX_SURVEY_QUESTION_SUB_OPTIONS_SurveyQuestionOptionsId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SURVEY_QUESTION_SUB_OPTIONS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SURVEY_QUESTION_SUB_OPTIONS",
                table: "SURVEY_QUESTION_SUB_OPTIONS",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SURVEY_QUESTION_SUB_OPTIONS_SURVEY_QUESTION_OPTIONS_SurveyQuestionOptionsId",
                table: "SURVEY_QUESTION_SUB_OPTIONS",
                column: "SurveyQuestionOptionsId",
                principalTable: "SURVEY_QUESTION_OPTIONS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SURVEY_SELECTION_RESULT_LINKS_SURVEY_QUESTION_SUB_OPTIONS_SurveyQuestionSubOptionsId",
                table: "SURVEY_SELECTION_RESULT_LINKS",
                column: "SurveyQuestionSubOptionsId",
                principalTable: "SURVEY_QUESTION_SUB_OPTIONS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_USER_SURVEY_RESPONSE_SURVEY_QUESTION_SUB_OPTIONS_SurveyQuestionSubOptionsId",
                table: "USER_SURVEY_RESPONSE",
                column: "SurveyQuestionSubOptionsId",
                principalTable: "SURVEY_QUESTION_SUB_OPTIONS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SURVEY_QUESTION_SUB_OPTIONS_SURVEY_QUESTION_OPTIONS_SurveyQuestionOptionsId",
                table: "SURVEY_QUESTION_SUB_OPTIONS");

            migrationBuilder.DropForeignKey(
                name: "FK_SURVEY_SELECTION_RESULT_LINKS_SURVEY_QUESTION_SUB_OPTIONS_SurveyQuestionSubOptionsId",
                table: "SURVEY_SELECTION_RESULT_LINKS");

            migrationBuilder.DropForeignKey(
                name: "FK_USER_SURVEY_RESPONSE_SURVEY_QUESTION_SUB_OPTIONS_SurveyQuestionSubOptionsId",
                table: "USER_SURVEY_RESPONSE");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SURVEY_QUESTION_SUB_OPTIONS",
                table: "SURVEY_QUESTION_SUB_OPTIONS");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "SURVEY_QUESTION_SUB_OPTIONS");

            migrationBuilder.RenameTable(
                name: "SURVEY_QUESTION_SUB_OPTIONS",
                newName: "SURVEY_QUESTION_OPTION_SUB_OPTIONS");

            migrationBuilder.RenameIndex(
                name: "IX_SURVEY_QUESTION_SUB_OPTIONS_SurveyQuestionOptionsId",
                table: "SURVEY_QUESTION_OPTION_SUB_OPTIONS",
                newName: "IX_SURVEY_QUESTION_OPTION_SUB_OPTIONS_SurveyQuestionOptionsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SURVEY_QUESTION_OPTION_SUB_OPTIONS",
                table: "SURVEY_QUESTION_OPTION_SUB_OPTIONS",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SURVEY_QUESTION_OPTION_SUB_OPTIONS_SURVEY_QUESTION_OPTIONS_SurveyQuestionOptionsId",
                table: "SURVEY_QUESTION_OPTION_SUB_OPTIONS",
                column: "SurveyQuestionOptionsId",
                principalTable: "SURVEY_QUESTION_OPTIONS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SURVEY_SELECTION_RESULT_LINKS_SURVEY_QUESTION_OPTION_SUB_OPTIONS_SurveyQuestionSubOptionsId",
                table: "SURVEY_SELECTION_RESULT_LINKS",
                column: "SurveyQuestionSubOptionsId",
                principalTable: "SURVEY_QUESTION_OPTION_SUB_OPTIONS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_USER_SURVEY_RESPONSE_SURVEY_QUESTION_OPTION_SUB_OPTIONS_SurveyQuestionSubOptionsId",
                table: "USER_SURVEY_RESPONSE",
                column: "SurveyQuestionSubOptionsId",
                principalTable: "SURVEY_QUESTION_OPTION_SUB_OPTIONS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
