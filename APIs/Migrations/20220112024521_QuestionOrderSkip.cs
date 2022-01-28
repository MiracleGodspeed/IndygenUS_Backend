using Microsoft.EntityFrameworkCore.Migrations;

namespace APIs.Migrations
{
    public partial class QuestionOrderSkip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "QuestionOrder",
                table: "SURVEY_QUESTIONS",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "SkipTo",
                table: "SURVEY_QUESTION_SUB_OPTIONS",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionOrder",
                table: "SURVEY_QUESTIONS");

            migrationBuilder.DropColumn(
                name: "SkipTo",
                table: "SURVEY_QUESTION_SUB_OPTIONS");
        }
    }
}
