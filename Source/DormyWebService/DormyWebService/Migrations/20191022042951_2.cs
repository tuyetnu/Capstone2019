using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DormyWebService.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Value",
                table: "Params",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeValue",
                table: "Params",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.UpdateData(
                table: "Params",
                keyColumn: "ParamId",
                keyValue: 0,
                columns: new[] { "TimeValue", "Value" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Params",
                keyColumn: "ParamId",
                keyValue: 1,
                columns: new[] { "TimeValue", "Value" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Params",
                keyColumn: "ParamId",
                keyValue: 2,
                columns: new[] { "TimeValue", "Value" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Params",
                keyColumn: "ParamId",
                keyValue: 10,
                columns: new[] { "TimeValue", "Value" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Params",
                keyColumn: "ParamId",
                keyValue: 11,
                columns: new[] { "TimeValue", "Value" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Params",
                keyColumn: "ParamId",
                keyValue: 12,
                columns: new[] { "TimeValue", "Value" },
                values: new object[] { null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Value",
                table: "Params",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeValue",
                table: "Params",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Params",
                keyColumn: "ParamId",
                keyValue: 0,
                columns: new[] { "TimeValue", "Value" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 });

            migrationBuilder.UpdateData(
                table: "Params",
                keyColumn: "ParamId",
                keyValue: 1,
                columns: new[] { "TimeValue", "Value" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 });

            migrationBuilder.UpdateData(
                table: "Params",
                keyColumn: "ParamId",
                keyValue: 2,
                columns: new[] { "TimeValue", "Value" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 });

            migrationBuilder.UpdateData(
                table: "Params",
                keyColumn: "ParamId",
                keyValue: 10,
                columns: new[] { "TimeValue", "Value" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 });

            migrationBuilder.UpdateData(
                table: "Params",
                keyColumn: "ParamId",
                keyValue: 11,
                columns: new[] { "TimeValue", "Value" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 });

            migrationBuilder.UpdateData(
                table: "Params",
                keyColumn: "ParamId",
                keyValue: 12,
                columns: new[] { "TimeValue", "Value" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 });
        }
    }
}
