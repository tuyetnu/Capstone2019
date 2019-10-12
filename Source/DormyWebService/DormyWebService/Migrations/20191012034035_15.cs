using Microsoft.EntityFrameworkCore.Migrations;

namespace DormyWebService.Migrations
{
    public partial class _15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_Students_StudentId",
                table: "Contract");

            migrationBuilder.DropIndex(
                name: "IX_Contract_StudentId",
                table: "Contract");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Contract",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Contract_StudentId",
                table: "Contract",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Students_StudentId",
                table: "Contract",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_Students_StudentId",
                table: "Contract");

            migrationBuilder.DropIndex(
                name: "IX_Contract_StudentId",
                table: "Contract");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Contract",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

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
