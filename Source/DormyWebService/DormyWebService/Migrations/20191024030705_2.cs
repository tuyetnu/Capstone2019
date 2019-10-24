using Microsoft.EntityFrameworkCore.Migrations;

namespace DormyWebService.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomBookingRequestForm_Staff_StaffId",
                table: "RoomBookingRequestForm");

            migrationBuilder.AlterColumn<int>(
                name: "StaffId",
                table: "RoomBookingRequestForm",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "RoomBookingRequestForm",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomBookingRequestForm_Staff_StaffId",
                table: "RoomBookingRequestForm",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "StaffId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomBookingRequestForm_Staff_StaffId",
                table: "RoomBookingRequestForm");

            migrationBuilder.DropColumn(
                name: "Reason",
                table: "RoomBookingRequestForm");

            migrationBuilder.AlterColumn<int>(
                name: "StaffId",
                table: "RoomBookingRequestForm",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_RoomBookingRequestForm_Staff_StaffId",
                table: "RoomBookingRequestForm",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "StaffId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
