using Microsoft.EntityFrameworkCore.Migrations;

namespace DormyWebService.Migrations
{
    public partial class _12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "PricePerUnits",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PricePerUnits_TypeId",
                table: "PricePerUnits",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PricePerUnits_Params_TypeId",
                table: "PricePerUnits",
                column: "TypeId",
                principalTable: "Params",
                principalColumn: "ParamId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PricePerUnits_Params_TypeId",
                table: "PricePerUnits");

            migrationBuilder.DropIndex(
                name: "IX_PricePerUnits_TypeId",
                table: "PricePerUnits");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "PricePerUnits");
        }
    }
}
