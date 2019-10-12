using Microsoft.EntityFrameworkCore.Migrations;

namespace DormyWebService.Migrations
{
    public partial class _12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_Students_StudentId",
                table: "Contract");

            migrationBuilder.DropIndex(
                name: "IX_Contract_StudentId",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Contract");

            migrationBuilder.AddColumn<int>(
                name: "CurrentContractId",
                table: "Students",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_CurrentContractId",
                table: "Students",
                column: "CurrentContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Contract_CurrentContractId",
                table: "Students",
                column: "CurrentContractId",
                principalTable: "Contract",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Contract_CurrentContractId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_CurrentContractId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CurrentContractId",
                table: "Students");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Contract",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Contract_StudentId",
                table: "Contract",
                column: "StudentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Students_StudentId",
                table: "Contract",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
