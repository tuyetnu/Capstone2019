using Microsoft.EntityFrameworkCore.Migrations;

namespace DormyWebService.Migrations
{
    public partial class addRoomToRoomBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "RoomBookingRequestForm",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoomBookingRequestForm_RoomId",
                table: "RoomBookingRequestForm",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomBookingRequestForm_Rooms_RoomId",
                table: "RoomBookingRequestForm",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomBookingRequestForm_Rooms_RoomId",
                table: "RoomBookingRequestForm");

            migrationBuilder.DropIndex(
                name: "IX_RoomBookingRequestForm_RoomId",
                table: "RoomBookingRequestForm");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "RoomBookingRequestForm");
        }
    }
}
