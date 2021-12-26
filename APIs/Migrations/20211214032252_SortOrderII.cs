using Microsoft.EntityFrameworkCore.Migrations;

namespace APIs.Migrations
{
    public partial class SortOrderII : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<long>(
                name: "SortOrder",
                table: "SURVEY_CATEGORY",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "SURVEY_CATEGORY");

           
        }
    }
}
