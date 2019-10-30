using Microsoft.EntityFrameworkCore.Migrations;

namespace DormyWebService.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContractId",
                table: "ContractRenewalForms",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ContractRenewalForms_ContractId",
                table: "ContractRenewalForms",
                column: "ContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractRenewalForms_Contracts_ContractId",
                table: "ContractRenewalForms",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "ContractId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractRenewalForms_Contracts_ContractId",
                table: "ContractRenewalForms");

            migrationBuilder.DropIndex(
                name: "IX_ContractRenewalForms_ContractId",
                table: "ContractRenewalForms");

            migrationBuilder.DropColumn(
                name: "ContractId",
                table: "ContractRenewalForms");
        }
    }
}
