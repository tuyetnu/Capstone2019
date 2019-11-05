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
                name: "RoomTransferRequestForms",
                columns: table => new
                {
                    RoomTransferRequestFormId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    RejectDate = table.Column<DateTime>(nullable: false),
                    Reason = table.Column<string>(maxLength: 500, nullable: false),
                    StudentId = table.Column<int>(nullable: false),
                    TargetRoomType = table.Column<int>(nullable: false),
                    RoomId = table.Column<int>(nullable: true),
                    Status = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTransferRequestForms", x => x.RoomTransferRequestFormId);
                    table.ForeignKey(
                        name: "FK_RoomTransferRequestForms_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoomTransferRequestForms_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomTransferRequestForms_RoomId",
                table: "RoomTransferRequestForms",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomTransferRequestForms_StudentId",
                table: "RoomTransferRequestForms",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomTransferRequestForms");
        }
    }
}
