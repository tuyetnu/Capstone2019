using Microsoft.EntityFrameworkCore.Migrations;

namespace DormyWebService.Migrations
{
    public partial class _8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomGroupId",
                table: "Rooms",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoomGroupId",
                table: "Rooms",
                column: "RoomGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_RoomGroups_RoomGroupId",
                table: "Rooms",
                column: "RoomGroupId",
                principalTable: "RoomGroups",
                principalColumn: "RoomGroupId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_RoomGroups_RoomGroupId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_RoomGroupId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "RoomGroupId",
                table: "Rooms");
        }
    }
}
