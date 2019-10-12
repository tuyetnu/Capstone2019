using Microsoft.EntityFrameworkCore.Migrations;

namespace DormyWebService.Migrations
{
    public partial class _11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_Users_UserId",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Contract_CurrentContractId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Users_Id",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_CurrentContractId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Contract_UserId",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "CurrentContractId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Contract");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Students",
                newName: "StudentId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Users_StudentId",
                table: "Students",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_Students_StudentId",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Users_StudentId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Contract_StudentId",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Contract");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Students",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "CurrentContractId",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Contract",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_CurrentContractId",
                table: "Students",
                column: "CurrentContractId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Contract_CurrentContractId",
                table: "Students",
                column: "CurrentContractId",
                principalTable: "Contract",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Users_Id",
                table: "Students",
                column: "Id",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
