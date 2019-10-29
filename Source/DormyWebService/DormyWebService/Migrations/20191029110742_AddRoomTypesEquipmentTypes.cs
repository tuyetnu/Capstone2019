using Microsoft.EntityFrameworkCore.Migrations;

namespace DormyWebService.Migrations
{
    public partial class AddRoomTypesEquipmentTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoomTypesEquipmentTypes",
                columns: table => new
                {
                    RoomTypeId = table.Column<int>(nullable: false),
                    EquipmentTypeId = table.Column<int>(nullable: false),
                    Amount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTypesEquipmentTypes", x => new { x.EquipmentTypeId, x.RoomTypeId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomTypesEquipmentTypes");
        }
    }
}
