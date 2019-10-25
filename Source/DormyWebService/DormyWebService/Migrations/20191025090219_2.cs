using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DormyWebService.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IssueTickets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IssueTickets",
                columns: table => new
                {
                    IssueTicketId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    EquipmentId = table.Column<int>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    OwnerId = table.Column<int>(nullable: false),
                    Point = table.Column<int>(nullable: true),
                    RoomId = table.Column<int>(nullable: true),
                    StaffId = table.Column<int>(nullable: true),
                    Status = table.Column<string>(nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueTickets", x => x.IssueTicketId);
                    table.ForeignKey(
                        name: "FK_IssueTickets_Equipments_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipments",
                        principalColumn: "EquipmentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IssueTickets_Students_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IssueTickets_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IssueTickets_Staff_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IssueTickets_EquipmentId",
                table: "IssueTickets",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueTickets_OwnerId",
                table: "IssueTickets",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueTickets_RoomId",
                table: "IssueTickets",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueTickets_StaffId",
                table: "IssueTickets",
                column: "StaffId");
        }
    }
}
