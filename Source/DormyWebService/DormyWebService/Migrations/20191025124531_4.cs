using Microsoft.EntityFrameworkCore.Migrations;

namespace DormyWebService.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TargetRoomType",
                table: "RoomTransferRequestForms",
                newName: "TargetRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomTransferRequestForms_TargetRoomId",
                table: "RoomTransferRequestForms",
                column: "TargetRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomTransferRequestForms_Rooms_TargetRoomId",
                table: "RoomTransferRequestForms",
                column: "TargetRoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomTransferRequestForms_Rooms_TargetRoomId",
                table: "RoomTransferRequestForms");

            migrationBuilder.DropIndex(
                name: "IX_RoomTransferRequestForms_TargetRoomId",
                table: "RoomTransferRequestForms");

            migrationBuilder.RenameColumn(
                name: "TargetRoomId",
                table: "RoomTransferRequestForms",
                newName: "TargetRoomType");
        }
    }
}
