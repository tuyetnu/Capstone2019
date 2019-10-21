using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DormyWebService.Migrations
{
    public partial class _22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Params",
                keyColumn: "ParamId",
                keyValue: 3);

            migrationBuilder.InsertData(
                table: "Params",
                columns: new[] { "ParamId", "Name", "ParamTypeId", "TextValue", "TimeValue", "Value" },
                values: new object[] { 2, "None", 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Params",
                keyColumn: "ParamId",
                keyValue: 2);

            migrationBuilder.InsertData(
                table: "Params",
                columns: new[] { "ParamId", "Name", "ParamTypeId", "TextValue", "TimeValue", "Value" },
                values: new object[] { 3, "None", 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 });
        }
    }
}
