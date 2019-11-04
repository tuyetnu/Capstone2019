using Microsoft.EntityFrameworkCore.Migrations;

namespace DormyWebService.Migrations
{
    public partial class _18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoomTypeId",
                table: "AndEquipmentTypes",
                newName: "EquipmentTypeId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_AndEquipmentTypes_EquipmentTypeId_RoomId",
                table: "AndEquipmentTypes",
                columns: new[] { "EquipmentTypeId", "RoomId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_AndEquipmentTypes_EquipmentTypeId_RoomId",
                table: "AndEquipmentTypes");

            migrationBuilder.RenameColumn(
                name: "EquipmentTypeId",
                table: "AndEquipmentTypes",
                newName: "RoomTypeId");
        }
    }
}
