using Microsoft.EntityFrameworkCore.Migrations;

namespace DormyWebService.Migrations
{
    public partial class _5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IssueTickets_Rooms_RoomId",
                table: "IssueTickets");

            migrationBuilder.DropIndex(
                name: "IX_IssueTickets_RoomId",
                table: "IssueTickets");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "IssueTickets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "IssueTickets",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IssueTickets_RoomId",
                table: "IssueTickets",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueTickets_Rooms_RoomId",
                table: "IssueTickets",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
