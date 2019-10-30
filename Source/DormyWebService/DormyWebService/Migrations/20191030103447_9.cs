using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DormyWebService.Migrations
{
    public partial class _9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomDivisionId",
                table: "RoomGroups",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RoomDivision",
                columns: table => new
                {
                    RoomDivisionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoomGroupNumber = table.Column<int>(nullable: false),
                    RoomNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomDivision", x => x.RoomDivisionId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomGroups_RoomDivisionId",
                table: "RoomGroups",
                column: "RoomDivisionId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomGroups_RoomDivision_RoomDivisionId",
                table: "RoomGroups",
                column: "RoomDivisionId",
                principalTable: "RoomDivision",
                principalColumn: "RoomDivisionId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomGroups_RoomDivision_RoomDivisionId",
                table: "RoomGroups");

            migrationBuilder.DropTable(
                name: "RoomDivision");

            migrationBuilder.DropIndex(
                name: "IX_RoomGroups_RoomDivisionId",
                table: "RoomGroups");

            migrationBuilder.DropColumn(
                name: "RoomDivisionId",
                table: "RoomGroups");
        }
    }
}
