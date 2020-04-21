using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopSolution.Data.Migrations
{
    public partial class changecolumnUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AppUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "AppUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "AppUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "AppUsers",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "3081d893-000e-4b90-949b-02faf8298ffa");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "CreatedBy", "PasswordHash" },
                values: new object[] { "7cffc5e9-a558-40cf-a41a-c8bc3c29bd5d", new DateTime(2020, 4, 21, 15, 3, 30, 444, DateTimeKind.Local).AddTicks(3893), "system", "AQAAAAEAACcQAAAAEGYr3bOjq1Y+aSUvUluQPf5kFzW9OhMPHY8LP1w9t3/QAb7F0hEQqWyagR8sNSZFcQ==" });

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
                column: "CreatedDate",
                value: new DateTime(2020, 4, 21, 15, 3, 30, 403, DateTimeKind.Local).AddTicks(9739));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "AppUsers");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "e4448a92-5f0a-4b3c-b9b9-5029da24bf96");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0f6d7235-21a1-4f56-9c57-c65741d0b41d", "AQAAAAEAACcQAAAAEImGmH3goLyOjirmkFHwcfA8LFHtBULhmAQWY+HUUIYxFSe6n/22zCZy7GPVihfUcQ==" });

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
                column: "CreatedDate",
                value: new DateTime(2020, 4, 2, 8, 58, 42, 108, DateTimeKind.Local).AddTicks(8461));
        }
    }
}
