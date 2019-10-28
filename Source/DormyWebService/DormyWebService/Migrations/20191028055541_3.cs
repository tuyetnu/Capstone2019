using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DormyWebService.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IssueTickets",
                columns: table => new
                {
                    IssueTicketId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    OwnerId = table.Column<int>(nullable: false),
                    EquipmentId = table.Column<int>(nullable: true),
                    TargetStudentId = table.Column<int>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false)
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
                        name: "FK_IssueTickets_Students_TargetStudentId",
                        column: x => x.TargetStudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
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
                name: "IX_IssueTickets_TargetStudentId",
                table: "IssueTickets",
                column: "TargetStudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IssueTickets");
        }
    }
}
