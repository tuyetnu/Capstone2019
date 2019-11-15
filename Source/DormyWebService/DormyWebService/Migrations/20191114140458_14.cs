using Microsoft.EntityFrameworkCore.Migrations;

namespace DormyWebService.Migrations
{
    public partial class _14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TargetMonth",
                table: "StudentMonthlyBills",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TargetYear",
                table: "StudentMonthlyBills",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TargetMonth",
                table: "RoomMonthlyBills",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TargetYear",
                table: "RoomMonthlyBills",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TargetMonth",
                table: "StudentMonthlyBills");

            migrationBuilder.DropColumn(
                name: "TargetYear",
                table: "StudentMonthlyBills");

            migrationBuilder.DropColumn(
                name: "TargetMonth",
                table: "RoomMonthlyBills");

            migrationBuilder.DropColumn(
                name: "TargetYear",
                table: "RoomMonthlyBills");
        }
    }
}
