using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DormyWebService.Migrations
{
    public partial class _13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomTransferRequestForms");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoomTransferRequestForms",
                columns: table => new
                {
                    RoomTransferRequestFormId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    Reason = table.Column<string>(maxLength: 500, nullable: false),
                    StaffId = table.Column<int>(nullable: true),
                    Status = table.Column<string>(nullable: false),
                    StudentId = table.Column<int>(nullable: false),
                    TargetRoomId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTransferRequestForms", x => x.RoomTransferRequestFormId);
                    table.ForeignKey(
                        name: "FK_RoomTransferRequestForms_Staff_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoomTransferRequestForms_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomTransferRequestForms_Rooms_TargetRoomId",
                        column: x => x.TargetRoomId,
                        principalTable: "Rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomTransferRequestForms_StaffId",
                table: "RoomTransferRequestForms",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomTransferRequestForms_StudentId",
                table: "RoomTransferRequestForms",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomTransferRequestForms_TargetRoomId",
                table: "RoomTransferRequestForms",
                column: "TargetRoomId");
        }
    }
}
