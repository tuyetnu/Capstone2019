using Microsoft.EntityFrameworkCore.Migrations;

namespace DormyWebService.Migrations
{
    public partial class _10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Contract",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contract_UserId",
                table: "Contract",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Users_UserId",
                table: "Contract",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_Users_UserId",
                table: "Contract");

            migrationBuilder.DropIndex(
                name: "IX_Contract_UserId",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Contract");
        }
    }
}
