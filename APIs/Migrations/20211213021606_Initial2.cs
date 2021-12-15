using Microsoft.EntityFrameworkCore.Migrations;

namespace APIs.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SecurityAnswer",
                table: "USER_SECURITY_QUESTIONS",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecurityAnswer",
                table: "USER_SECURITY_QUESTIONS");
        }
    }
}
