using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DormyWebService.Migrations
{
    public partial class _7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoomGroups",
                columns: table => new
                {
                    RoomGroupId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    RoomNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomGroups", x => x.RoomGroupId);
                });

            migrationBuilder.CreateTable(
                name: "RomGroupsAndStaff",
                columns: table => new
                {
                    RoomGroupId = table.Column<int>(nullable: false),
                    StaffId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RomGroupsAndStaff", x => new { x.RoomGroupId, x.StaffId });
                    table.ForeignKey(
                        name: "FK_RomGroupsAndStaff_RoomGroups_RoomGroupId",
                        column: x => x.RoomGroupId,
                        principalTable: "RoomGroups",
                        principalColumn: "RoomGroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RomGroupsAndStaff_Staff_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RomGroupsAndStaff_StaffId",
                table: "RomGroupsAndStaff",
                column: "StaffId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RomGroupsAndStaff");

            migrationBuilder.DropTable(
                name: "RoomGroups");
        }
    }
}
