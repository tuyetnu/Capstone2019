using Microsoft.EntityFrameworkCore.Migrations;

namespace DormyWebService.Migrations
{
    public partial class _21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AndEquipmentTypes_Rooms_RoomId",
                table: "AndEquipmentTypes");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_AndEquipmentTypes_EquipmentTypeId_RoomId",
                table: "AndEquipmentTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AndEquipmentTypes",
                table: "AndEquipmentTypes");

            migrationBuilder.RenameTable(
                name: "AndEquipmentTypes",
                newName: "RoomsAndEquipmentTypes");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_RoomsAndEquipmentTypes_EquipmentTypeId_RoomId",
                table: "RoomsAndEquipmentTypes",
                columns: new[] { "EquipmentTypeId", "RoomId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomsAndEquipmentTypes",
                table: "RoomsAndEquipmentTypes",
                columns: new[] { "RoomId", "EquipmentTypeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_RoomsAndEquipmentTypes_Rooms_RoomId",
                table: "RoomsAndEquipmentTypes",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomsAndEquipmentTypes_Rooms_RoomId",
                table: "RoomsAndEquipmentTypes");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_RoomsAndEquipmentTypes_EquipmentTypeId_RoomId",
                table: "RoomsAndEquipmentTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomsAndEquipmentTypes",
                table: "RoomsAndEquipmentTypes");

            migrationBuilder.RenameTable(
                name: "RoomsAndEquipmentTypes",
                newName: "AndEquipmentTypes");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_AndEquipmentTypes_EquipmentTypeId_RoomId",
                table: "AndEquipmentTypes",
                columns: new[] { "EquipmentTypeId", "RoomId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AndEquipmentTypes",
                table: "AndEquipmentTypes",
                columns: new[] { "RoomId", "EquipmentTypeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AndEquipmentTypes_Rooms_RoomId",
                table: "AndEquipmentTypes",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
