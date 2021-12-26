using Microsoft.EntityFrameworkCore.Migrations;

namespace APIs.Migrations
{
    public partial class RegionTable1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "RegionId",
                table: "USER_FRINGE_DETAILS",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "REGION",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REGION", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_USER_FRINGE_DETAILS_RegionId",
                table: "USER_FRINGE_DETAILS",
                column: "RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_USER_FRINGE_DETAILS_REGION_RegionId",
                table: "USER_FRINGE_DETAILS",
                column: "RegionId",
                principalTable: "REGION",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_USER_FRINGE_DETAILS_REGION_RegionId",
                table: "USER_FRINGE_DETAILS");

            migrationBuilder.DropTable(
                name: "REGION");

            migrationBuilder.DropIndex(
                name: "IX_USER_FRINGE_DETAILS_RegionId",
                table: "USER_FRINGE_DETAILS");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "USER_FRINGE_DETAILS");
        }
    }
}
