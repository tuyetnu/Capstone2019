using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DormyWebService.Migrations
{
    public partial class _13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Percentage",
                table: "StudentMonthlyBills");

            migrationBuilder.DropColumn(
                name: "RoomFee",
                table: "StudentMonthlyBills");

            migrationBuilder.DropColumn(
                name: "RoomUtilityFee",
                table: "StudentMonthlyBills");

            migrationBuilder.DropColumn(
                name: "TargetMonth",
                table: "StudentMonthlyBills");

            migrationBuilder.DropColumn(
                name: "UtilityFee",
                table: "StudentMonthlyBills");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "RoomMonthlyBills");

            migrationBuilder.RenameColumn(
                name: "TargetYear",
                table: "StudentMonthlyBills",
                newName: "PaidDate");

            migrationBuilder.RenameColumn(
                name: "TotalRoomFee",
                table: "RoomMonthlyBills",
                newName: "RoomBill");

            migrationBuilder.RenameColumn(
                name: "TargetYear",
                table: "RoomMonthlyBills",
                newName: "PricePerWaterId");

            migrationBuilder.RenameColumn(
                name: "TargetMonth",
                table: "RoomMonthlyBills",
                newName: "PricePerRoomId");

            migrationBuilder.AddColumn<int>(
                name: "PricePerElectricityId",
                table: "RoomMonthlyBills",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "ContractRenewalForms",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PricePerUnits_Params_TypeId",
                table: "PricePerUnits");

            migrationBuilder.DropIndex(
                name: "IX_PricePerUnits_TypeId",
                table: "PricePerUnits");

            migrationBuilder.DropColumn(
                name: "PricePerElectricityId",
                table: "RoomMonthlyBills");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "PricePerUnits");

            migrationBuilder.RenameColumn(
                name: "PaidDate",
                table: "StudentMonthlyBills",
                newName: "TargetYear");

            migrationBuilder.RenameColumn(
                name: "RoomBill",
                table: "RoomMonthlyBills",
                newName: "TotalRoomFee");

            migrationBuilder.RenameColumn(
                name: "PricePerWaterId",
                table: "RoomMonthlyBills",
                newName: "TargetYear");

            migrationBuilder.RenameColumn(
                name: "PricePerRoomId",
                table: "RoomMonthlyBills",
                newName: "TargetMonth");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "PricePerUnits",
                newName: "TargetYear");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "PricePerUnits",
                newName: "WaterPricePerUnit");

            migrationBuilder.AddColumn<decimal>(
                name: "Percentage",
                table: "StudentMonthlyBills",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RoomFee",
                table: "StudentMonthlyBills",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RoomUtilityFee",
                table: "StudentMonthlyBills",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "TargetMonth",
                table: "StudentMonthlyBills",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "UtilityFee",
                table: "StudentMonthlyBills",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "RoomMonthlyBills",
                nullable: false,
                defaultValue: false);

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

            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "ContractRenewalForms",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
