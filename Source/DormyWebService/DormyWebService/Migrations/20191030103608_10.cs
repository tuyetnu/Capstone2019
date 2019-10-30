using Microsoft.EntityFrameworkCore.Migrations;

namespace DormyWebService.Migrations
{
    public partial class _10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RomGroupsAndStaff_RoomGroups_RoomGroupId",
                table: "RomGroupsAndStaff");

            migrationBuilder.DropForeignKey(
                name: "FK_RomGroupsAndStaff_Staff_StaffId",
                table: "RomGroupsAndStaff");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomGroups_RoomDivision_RoomDivisionId",
                table: "RoomGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomDivision",
                table: "RoomDivision");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RomGroupsAndStaff",
                table: "RomGroupsAndStaff");

            migrationBuilder.RenameTable(
                name: "RoomDivision",
                newName: "RoomDivisions");

            migrationBuilder.RenameTable(
                name: "RomGroupsAndStaff",
                newName: "RoomGroupsAndStaff");

            migrationBuilder.RenameIndex(
                name: "IX_RomGroupsAndStaff_StaffId",
                table: "RoomGroupsAndStaff",
                newName: "IX_RoomGroupsAndStaff_StaffId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomDivisions",
                table: "RoomDivisions",
                column: "RoomDivisionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomGroupsAndStaff",
                table: "RoomGroupsAndStaff",
                columns: new[] { "RoomGroupId", "StaffId" });

            migrationBuilder.AddForeignKey(
                name: "FK_RoomGroups_RoomDivisions_RoomDivisionId",
                table: "RoomGroups",
                column: "RoomDivisionId",
                principalTable: "RoomDivisions",
                principalColumn: "RoomDivisionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomGroupsAndStaff_RoomGroups_RoomGroupId",
                table: "RoomGroupsAndStaff",
                column: "RoomGroupId",
                principalTable: "RoomGroups",
                principalColumn: "RoomGroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomGroupsAndStaff_Staff_StaffId",
                table: "RoomGroupsAndStaff",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "StaffId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomGroups_RoomDivisions_RoomDivisionId",
                table: "RoomGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomGroupsAndStaff_RoomGroups_RoomGroupId",
                table: "RoomGroupsAndStaff");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomGroupsAndStaff_Staff_StaffId",
                table: "RoomGroupsAndStaff");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomGroupsAndStaff",
                table: "RoomGroupsAndStaff");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomDivisions",
                table: "RoomDivisions");

            migrationBuilder.RenameTable(
                name: "RoomGroupsAndStaff",
                newName: "RomGroupsAndStaff");

            migrationBuilder.RenameTable(
                name: "RoomDivisions",
                newName: "RoomDivision");

            migrationBuilder.RenameIndex(
                name: "IX_RoomGroupsAndStaff_StaffId",
                table: "RomGroupsAndStaff",
                newName: "IX_RomGroupsAndStaff_StaffId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RomGroupsAndStaff",
                table: "RomGroupsAndStaff",
                columns: new[] { "RoomGroupId", "StaffId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomDivision",
                table: "RoomDivision",
                column: "RoomDivisionId");

            migrationBuilder.AddForeignKey(
                name: "FK_RomGroupsAndStaff_RoomGroups_RoomGroupId",
                table: "RomGroupsAndStaff",
                column: "RoomGroupId",
                principalTable: "RoomGroups",
                principalColumn: "RoomGroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RomGroupsAndStaff_Staff_StaffId",
                table: "RomGroupsAndStaff",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "StaffId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomGroups_RoomDivision_RoomDivisionId",
                table: "RoomGroups",
                column: "RoomDivisionId",
                principalTable: "RoomDivision",
                principalColumn: "RoomDivisionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
