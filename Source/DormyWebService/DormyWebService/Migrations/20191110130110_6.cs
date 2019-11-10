using Microsoft.EntityFrameworkCore.Migrations;

namespace DormyWebService.Migrations
{
    public partial class _6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "ContractRenewalForms",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ContractId",
                table: "CancelContractForms",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CancelContractForms_ContractId",
                table: "CancelContractForms",
                column: "ContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_CancelContractForms_Contracts_ContractId",
                table: "CancelContractForms",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "ContractId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CancelContractForms_Contracts_ContractId",
                table: "CancelContractForms");

            migrationBuilder.DropIndex(
                name: "IX_CancelContractForms_ContractId",
                table: "CancelContractForms");

            migrationBuilder.DropColumn(
                name: "Reason",
                table: "ContractRenewalForms");

            migrationBuilder.DropColumn(
                name: "ContractId",
                table: "CancelContractForms");
        }
    }
}
