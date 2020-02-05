using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopSolution.Data.Migrations
{
    public partial class addDataTableAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2020, 2, 5, 21, 29, 55, 12, DateTimeKind.Local).AddTicks(61),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 2, 5, 21, 21, 38, 798, DateTimeKind.Local).AddTicks(8199));

            migrationBuilder.InsertData(
                table: "Actions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 3 },
                    { 3, 0 },
                    { 4, 2 }
                });

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "0ba7a9ae-f96d-453c-be4a-4fb7fa3473f3");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b1731663-fc0c-40e4-bb75-16bb0c1f3bd5", "AQAAAAEAACcQAAAAENca2Qoz0Z4giZr32oV2VDNykOhQWIZL4lOCp/YwvDnT4l0LI7+EV8wm3vJLFbTD0g==" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 2, 5, 21, 29, 55, 32, DateTimeKind.Local).AddTicks(3670));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 2, 5, 21, 21, 38, 798, DateTimeKind.Local).AddTicks(8199),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 2, 5, 21, 29, 55, 12, DateTimeKind.Local).AddTicks(61));

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "a708cf16-1cbd-429e-9d74-c5ceeaea8b7d");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c66e54a5-f798-4667-882b-5a0356ef860f", "AQAAAAEAACcQAAAAEBt/stvgzoTIG/RTlyz2vBTH+f9sU6ed1Dj+oMKM7ViSFkwGeYKiXRN88fR0u2m0OQ==" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 2, 5, 21, 21, 38, 830, DateTimeKind.Local).AddTicks(3976));
        }
    }
}
