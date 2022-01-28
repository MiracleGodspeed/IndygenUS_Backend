using Microsoft.EntityFrameworkCore.Migrations;

namespace APIs.Migrations
{
    public partial class QuestionOptionInputType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InputType",
                table: "SURVEY_QUESTION_OPTIONS",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InputType",
                table: "SURVEY_QUESTION_OPTIONS");
        }
    }
}
