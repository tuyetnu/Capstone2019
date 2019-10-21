using Microsoft.EntityFrameworkCore.Migrations;

namespace DormyWebService.Migrations
{
    public partial class _28 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ParamTypes",
                keyColumn: "ParamTypeId",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Equipments");

            migrationBuilder.UpdateData(
                table: "ParamTypes",
                keyColumn: "ParamTypeId",
                keyValue: 0,
                column: "Name",
                value: "MoneyTransactionType");

            migrationBuilder.UpdateData(
                table: "ParamTypes",
                keyColumn: "ParamTypeId",
                keyValue: 1,
                column: "Name",
                value: "NotificationType");

            migrationBuilder.UpdateData(
                table: "ParamTypes",
                keyColumn: "ParamTypeId",
                keyValue: 2,
                column: "Name",
                value: "StudentPriorityType");

            migrationBuilder.UpdateData(
                table: "ParamTypes",
                keyColumn: "ParamTypeId",
                keyValue: 3,
                column: "Name",
                value: "AcceptedEmailHost");

            migrationBuilder.UpdateData(
                table: "ParamTypes",
                keyColumn: "ParamTypeId",
                keyValue: 4,
                column: "Name",
                value: "RoomType");

            migrationBuilder.UpdateData(
                table: "Params",
                keyColumn: "ParamId",
                keyValue: 0,
                column: "ParamTypeId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Params",
                keyColumn: "ParamId",
                keyValue: 1,
                column: "ParamTypeId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Params",
                keyColumn: "ParamId",
                keyValue: 2,
                column: "ParamTypeId",
                value: 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Equipments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "ParamTypes",
                keyColumn: "ParamTypeId",
                keyValue: 0,
                column: "Name",
                value: "EquipmentType");

            migrationBuilder.UpdateData(
                table: "ParamTypes",
                keyColumn: "ParamTypeId",
                keyValue: 1,
                column: "Name",
                value: "MoneyTransactionType");

            migrationBuilder.UpdateData(
                table: "ParamTypes",
                keyColumn: "ParamTypeId",
                keyValue: 2,
                column: "Name",
                value: "NotificationType");

            migrationBuilder.UpdateData(
                table: "ParamTypes",
                keyColumn: "ParamTypeId",
                keyValue: 3,
                column: "Name",
                value: "StudentPriorityType");

            migrationBuilder.UpdateData(
                table: "ParamTypes",
                keyColumn: "ParamTypeId",
                keyValue: 4,
                column: "Name",
                value: "AcceptedEmailHost");

            migrationBuilder.InsertData(
                table: "ParamTypes",
                columns: new[] { "ParamTypeId", "Name" },
                values: new object[] { 5, "RoomType" });

            migrationBuilder.UpdateData(
                table: "Params",
                keyColumn: "ParamId",
                keyValue: 0,
                column: "ParamTypeId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Params",
                keyColumn: "ParamId",
                keyValue: 1,
                column: "ParamTypeId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Params",
                keyColumn: "ParamId",
                keyValue: 2,
                column: "ParamTypeId",
                value: 3);
        }
    }
}
