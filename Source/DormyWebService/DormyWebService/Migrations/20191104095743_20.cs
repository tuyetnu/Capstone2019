using Microsoft.EntityFrameworkCore.Migrations;

namespace DormyWebService.Migrations
{
    public partial class _20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StudentCardImageUrl",
                table: "RoomBookingRequestForm",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "PriorityImageUrl",
                table: "RoomBookingRequestForm",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "IdentityCardImageUrl",
                table: "RoomBookingRequestForm",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StudentCardImageUrl",
                table: "RoomBookingRequestForm",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PriorityImageUrl",
                table: "RoomBookingRequestForm",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdentityCardImageUrl",
                table: "RoomBookingRequestForm",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
