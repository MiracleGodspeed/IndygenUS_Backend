using Microsoft.EntityFrameworkCore.Migrations;

namespace APIs.Migrations
{
    public partial class SubCategoryFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SURVEY_SUB_CATEGORY",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "SURVEY_SUB_CATEGORY");
        }
    }
}
