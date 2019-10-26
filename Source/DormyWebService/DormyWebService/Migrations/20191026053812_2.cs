using Microsoft.EntityFrameworkCore.Migrations;

namespace DormyWebService.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoneyTransactions_Rooms_RoomId",
                table: "MoneyTransactions");

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "MoneyTransactions",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_MoneyTransactions_Rooms_RoomId",
                table: "MoneyTransactions",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoneyTransactions_Rooms_RoomId",
                table: "MoneyTransactions");

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "MoneyTransactions",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MoneyTransactions_Rooms_RoomId",
                table: "MoneyTransactions",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
