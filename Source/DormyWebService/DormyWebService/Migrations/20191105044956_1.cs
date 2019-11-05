using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DormyWebService.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CancelContractForms");

            migrationBuilder.DropTable(
                name: "ContractRenewalForms");

            migrationBuilder.DropTable(
                name: "IssueTickets");

            migrationBuilder.DropTable(
                name: "MoneyTransactions");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Params");

            migrationBuilder.DropTable(
                name: "PricePerUnits");

            migrationBuilder.DropTable(
                name: "RoomBookingRequestForm");

            migrationBuilder.DropTable(
                name: "RoomGroupsAndStaff");

            migrationBuilder.DropTable(
                name: "RoomsAndEquipmentTypes");

            migrationBuilder.DropTable(
                name: "RoomTransferRequestForms");

            migrationBuilder.DropTable(
                name: "RoomTypesAndEquipmentTypes");

            migrationBuilder.DropTable(
                name: "StudentMonthlyBills");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "Equipments");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "ParamTypes");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "RoomMonthlyBills");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Buildings");

            migrationBuilder.DropTable(
                name: "RoomGroups");
        }
    }
}
