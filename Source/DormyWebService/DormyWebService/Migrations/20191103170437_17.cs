using Microsoft.EntityFrameworkCore.Migrations;

namespace DormyWebService.Migrations
{
    public partial class _17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AndEquipmentTypes",
                columns: table => new
                {
                    RoomId = table.Column<int>(nullable: false),
                    RoomTypeId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    RealQuantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AndEquipmentTypes", x => new { x.RoomId, x.RoomTypeId });
                    table.ForeignKey(
                        name: "FK_AndEquipmentTypes_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AndEquipmentTypes");
        }
    }
}
