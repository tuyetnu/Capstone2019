using Microsoft.EntityFrameworkCore.Migrations;

namespace DormyWebService.Migrations
{
    public partial class _6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IssueTickets_Students_TargetStudentId",
                table: "IssueTickets");

            migrationBuilder.DropIndex(
                name: "IX_IssueTickets_TargetStudentId",
                table: "IssueTickets");

            migrationBuilder.DropColumn(
                name: "TargetStudentId",
                table: "IssueTickets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TargetStudentId",
                table: "IssueTickets",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IssueTickets_TargetStudentId",
                table: "IssueTickets",
                column: "TargetStudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueTickets_Students_TargetStudentId",
                table: "IssueTickets",
                column: "TargetStudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
