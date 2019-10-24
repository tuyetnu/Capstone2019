using Microsoft.EntityFrameworkCore.Migrations;

namespace DormyWebService.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityCardImageUrl",
                table: "RoomBookingRequestForm",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PriorityImageUrl",
                table: "RoomBookingRequestForm",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PriorityType",
                table: "RoomBookingRequestForm",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StudentCardImageUrl",
                table: "RoomBookingRequestForm",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdentityCardImageUrl",
                table: "RoomBookingRequestForm");

            migrationBuilder.DropColumn(
                name: "PriorityImageUrl",
                table: "RoomBookingRequestForm");

            migrationBuilder.DropColumn(
                name: "PriorityType",
                table: "RoomBookingRequestForm");

            migrationBuilder.DropColumn(
                name: "StudentCardImageUrl",
                table: "RoomBookingRequestForm");
        }
    }
}
