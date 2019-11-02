using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DormyWebService.Migrations
{
    public partial class _15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomGroups_RoomDivisions_RoomDivisionId",
                table: "RoomGroups");

            migrationBuilder.DropTable(
                name: "RoomDivisions");

            migrationBuilder.DropIndex(
                name: "IX_RoomGroups_RoomDivisionId",
                table: "RoomGroups");

            migrationBuilder.DropColumn(
                name: "RoomDivisionId",
                table: "RoomGroups");

            migrationBuilder.CreateTable(
                name: "Buildings",
                columns: table => new
                {
                    BuildingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    NumberOfFloor = table.Column<int>(nullable: false),
                    RoomOnEachFloor = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.BuildingId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Buildings");

            migrationBuilder.AddColumn<int>(
                name: "RoomDivisionId",
                table: "RoomGroups",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RoomDivisions",
                columns: table => new
                {
                    RoomDivisionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    RoomGroupNumber = table.Column<int>(nullable: false),
                    RoomNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomDivisions", x => x.RoomDivisionId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomGroups_RoomDivisionId",
                table: "RoomGroups",
                column: "RoomDivisionId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomGroups_RoomDivisions_RoomDivisionId",
                table: "RoomGroups",
                column: "RoomDivisionId",
                principalTable: "RoomDivisions",
                principalColumn: "RoomDivisionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
