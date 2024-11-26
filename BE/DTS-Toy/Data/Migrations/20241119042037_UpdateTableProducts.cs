using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class UpdateTableProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: new Guid("c0915908-c9d9-4c88-8137-b7c4625cd9a7"),
                column: "BOD",
                value: new DateTime(2024, 11, 19, 11, 20, 37, 481, DateTimeKind.Local).AddTicks(1313));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: new Guid("d0915908-c9d9-4c88-8137-b7c4625cd9a8"),
                column: "BOD",
                value: new DateTime(2024, 11, 19, 11, 20, 37, 481, DateTimeKind.Local).AddTicks(1327));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: new Guid("e0915908-c9d9-4c88-8137-b7c4625cd9a9"),
                column: "BOD",
                value: new DateTime(2024, 11, 19, 11, 20, 37, 481, DateTimeKind.Local).AddTicks(1331));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: new Guid("f0915908-c9d9-4c88-8137-b7c4625cd9aa"),
                column: "BOD",
                value: new DateTime(2024, 11, 19, 11, 20, 37, 481, DateTimeKind.Local).AddTicks(1334));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: new Guid("c0915908-c9d9-4c88-8137-b7c4625cd9a7"),
                column: "BOD",
                value: new DateTime(2024, 11, 18, 11, 38, 47, 833, DateTimeKind.Local).AddTicks(1742));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: new Guid("d0915908-c9d9-4c88-8137-b7c4625cd9a8"),
                column: "BOD",
                value: new DateTime(2024, 11, 18, 11, 38, 47, 833, DateTimeKind.Local).AddTicks(1765));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: new Guid("e0915908-c9d9-4c88-8137-b7c4625cd9a9"),
                column: "BOD",
                value: new DateTime(2024, 11, 18, 11, 38, 47, 833, DateTimeKind.Local).AddTicks(1769));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: new Guid("f0915908-c9d9-4c88-8137-b7c4625cd9aa"),
                column: "BOD",
                value: new DateTime(2024, 11, 18, 11, 38, 47, 833, DateTimeKind.Local).AddTicks(1825));
        }
    }
}
