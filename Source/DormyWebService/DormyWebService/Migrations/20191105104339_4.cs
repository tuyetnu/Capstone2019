using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DormyWebService.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TransferDate",
                table: "RoomTransferRequestForms",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransferDate",
                table: "RoomTransferRequestForms");
        }
    }
}
