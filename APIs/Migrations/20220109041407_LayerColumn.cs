using Microsoft.EntityFrameworkCore.Migrations;

namespace APIs.Migrations
{
    public partial class LayerColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Layer",
                table: "SURVEY_QUESTIONS",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InputType",
                table: "SURVEY_QUESTION_SUB_OPTIONS",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Layer",
                table: "SURVEY_QUESTIONS");

            migrationBuilder.DropColumn(
                name: "InputType",
                table: "SURVEY_QUESTION_SUB_OPTIONS");
        }
    }
}
