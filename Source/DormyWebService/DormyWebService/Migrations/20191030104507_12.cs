using Microsoft.EntityFrameworkCore.Migrations;

namespace DormyWebService.Migrations
{
    public partial class _12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomTypesEquipmentTypes");

            migrationBuilder.CreateTable(
                name: "RoomTypesAndEquipmentTypes",
                columns: table => new
                {
                    RoomTypeId = table.Column<int>(nullable: false),
                    EquipmentTypeId = table.Column<int>(nullable: false),
                    Amount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTypesAndEquipmentTypes", x => new { x.EquipmentTypeId, x.RoomTypeId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomTypesAndEquipmentTypes");

            migrationBuilder.CreateTable(
                name: "RoomTypesEquipmentTypes",
                columns: table => new
                {
                    EquipmentTypeId = table.Column<int>(nullable: false),
                    RoomTypeId = table.Column<int>(nullable: false),
                    Amount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTypesEquipmentTypes", x => new { x.EquipmentTypeId, x.RoomTypeId });
                });
        }
    }
}
