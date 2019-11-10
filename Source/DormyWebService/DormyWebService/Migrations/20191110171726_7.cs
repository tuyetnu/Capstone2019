using Microsoft.EntityFrameworkCore.Migrations;

namespace DormyWebService.Migrations
{
    public partial class _7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CancelContractForms_Staff_StaffId",
                table: "CancelContractForms");

            migrationBuilder.AlterColumn<int>(
                name: "StaffId",
                table: "CancelContractForms",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_CancelContractForms_Staff_StaffId",
                table: "CancelContractForms",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "StaffId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CancelContractForms_Staff_StaffId",
                table: "CancelContractForms");

            migrationBuilder.AlterColumn<int>(
                name: "StaffId",
                table: "CancelContractForms",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CancelContractForms_Staff_StaffId",
                table: "CancelContractForms",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "StaffId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
