using Microsoft.EntityFrameworkCore.Migrations;

namespace DormyWebService.Migrations
{
    public partial class _11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ElectricityPricePerUnit",
                table: "PricePerUnits");

            migrationBuilder.DropColumn(
                name: "TargetMonth",
                table: "PricePerUnits");

            migrationBuilder.DropColumn(
                name: "TargetYear",
                table: "PricePerUnits");

            migrationBuilder.RenameColumn(
                name: "WaterPricePerUnit",
                table: "PricePerUnits",
                newName: "Price");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "PricePerUnits",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "PricePerUnits");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "PricePerUnits",
                newName: "WaterPricePerUnit");

            migrationBuilder.AddColumn<decimal>(
                name: "ElectricityPricePerUnit",
                table: "PricePerUnits",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "TargetMonth",
                table: "PricePerUnits",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TargetYear",
                table: "PricePerUnits",
                nullable: false,
                defaultValue: 0);
        }
    }
}
