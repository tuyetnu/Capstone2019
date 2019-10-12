using Microsoft.EntityFrameworkCore.Migrations;

namespace DormyWebService.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CancelContractForms_Params_StatusParamId",
                table: "CancelContractForms");

            migrationBuilder.DropIndex(
                name: "IX_CancelContractForms_StatusParamId",
                table: "CancelContractForms");

            migrationBuilder.RenameColumn(
                name: "StatusParamId",
                table: "CancelContractForms",
                newName: "Status");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "CancelContractForms",
                newName: "StatusParamId");

            migrationBuilder.CreateIndex(
                name: "IX_CancelContractForms_StatusParamId",
                table: "CancelContractForms",
                column: "StatusParamId");

            migrationBuilder.AddForeignKey(
                name: "FK_CancelContractForms_Params_StatusParamId",
                table: "CancelContractForms",
                column: "StatusParamId",
                principalTable: "Params",
                principalColumn: "ParamId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
