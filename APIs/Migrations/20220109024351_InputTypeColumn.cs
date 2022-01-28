using Microsoft.EntityFrameworkCore.Migrations;

namespace APIs.Migrations
{
    public partial class InputTypeColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InputType",
                table: "SURVEY_QUESTIONS",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InputType",
                table: "SURVEY_QUESTIONS");
        }
    }
}
