using Microsoft.EntityFrameworkCore.Migrations;

namespace APIs.Migrations
{
    public partial class SubCategoryFixII : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "SURVEY_SUB_CATEGORY",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Summary",
                table: "SURVEY_SUB_CATEGORY");
        }
    }
}
