using Microsoft.EntityFrameworkCore.Migrations;

namespace DormyWebService.Migrations
{
    public partial class ModifiedContractRenewalForm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Point",
                table: "IssueTickets");

            migrationBuilder.DropColumn(
                name: "StaffId",
                table: "IssueTickets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Point",
                table: "IssueTickets",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StaffId",
                table: "IssueTickets",
                nullable: true);
        }
    }
}
