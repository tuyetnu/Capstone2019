using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DormyWebService.Migrations
{
    public partial class _23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomBookingRequestForm_RoomTypes_TargetRoomTypeRoomTypeId",
                table: "RoomBookingRequestForm");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_RoomTypes_RoomTypeId",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomTransferRequestForms_RoomTypes_TargetRoomTypeRoomTypeId",
                table: "RoomTransferRequestForms");

            migrationBuilder.DropTable(
                name: "RoomTypes");

            migrationBuilder.DropIndex(
                name: "IX_RoomTransferRequestForms_TargetRoomTypeRoomTypeId",
                table: "RoomTransferRequestForms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_RoomTypeId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_RoomBookingRequestForm_TargetRoomTypeRoomTypeId",
                table: "RoomBookingRequestForm");

            migrationBuilder.RenameColumn(
                name: "TargetRoomTypeRoomTypeId",
                table: "RoomTransferRequestForms",
                newName: "TargetRoomType");

            migrationBuilder.RenameColumn(
                name: "RoomTypeId",
                table: "Rooms",
                newName: "RoomType");

            migrationBuilder.RenameColumn(
                name: "TargetRoomTypeRoomTypeId",
                table: "RoomBookingRequestForm",
                newName: "TargetRoomType");

            migrationBuilder.InsertData(
                table: "ParamTypes",
                columns: new[] { "ParamTypeId", "Name" },
                values: new object[] { 5, "RoomType" });

            migrationBuilder.InsertData(
                table: "Params",
                columns: new[] { "ParamId", "Name", "ParamTypeId", "TextValue", "TimeValue", "Value" },
                values: new object[] { 11, "Standard Room", 5, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 });

            migrationBuilder.InsertData(
                table: "Params",
                columns: new[] { "ParamId", "Name", "ParamTypeId", "TextValue", "TimeValue", "Value" },
                values: new object[] { 12, "Service Room", 5, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Params",
                keyColumn: "ParamId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Params",
                keyColumn: "ParamId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ParamTypes",
                keyColumn: "ParamTypeId",
                keyValue: 5);

            migrationBuilder.RenameColumn(
                name: "TargetRoomType",
                table: "RoomTransferRequestForms",
                newName: "TargetRoomTypeRoomTypeId");

            migrationBuilder.RenameColumn(
                name: "RoomType",
                table: "Rooms",
                newName: "RoomTypeId");

            migrationBuilder.RenameColumn(
                name: "TargetRoomType",
                table: "RoomBookingRequestForm",
                newName: "TargetRoomTypeRoomTypeId");

            migrationBuilder.CreateTable(
                name: "RoomTypes",
                columns: table => new
                {
                    RoomTypeId = table.Column<int>(nullable: false),
                    DefaultCapacity = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTypes", x => x.RoomTypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomTransferRequestForms_TargetRoomTypeRoomTypeId",
                table: "RoomTransferRequestForms",
                column: "TargetRoomTypeRoomTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoomTypeId",
                table: "Rooms",
                column: "RoomTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomBookingRequestForm_TargetRoomTypeRoomTypeId",
                table: "RoomBookingRequestForm",
                column: "TargetRoomTypeRoomTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomBookingRequestForm_RoomTypes_TargetRoomTypeRoomTypeId",
                table: "RoomBookingRequestForm",
                column: "TargetRoomTypeRoomTypeId",
                principalTable: "RoomTypes",
                principalColumn: "RoomTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_RoomTypes_RoomTypeId",
                table: "Rooms",
                column: "RoomTypeId",
                principalTable: "RoomTypes",
                principalColumn: "RoomTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomTransferRequestForms_RoomTypes_TargetRoomTypeRoomTypeId",
                table: "RoomTransferRequestForms",
                column: "TargetRoomTypeRoomTypeId",
                principalTable: "RoomTypes",
                principalColumn: "RoomTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
