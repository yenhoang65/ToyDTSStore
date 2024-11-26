using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class UpdateTableProductsv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_FlashSales_FlashSaleID",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_FlashSaleID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "FlashSaleID",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: new Guid("c0915908-c9d9-4c88-8137-b7c4625cd9a7"),
                column: "BOD",
                value: new DateTime(2024, 11, 19, 13, 48, 31, 501, DateTimeKind.Local).AddTicks(258));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: new Guid("d0915908-c9d9-4c88-8137-b7c4625cd9a8"),
                column: "BOD",
                value: new DateTime(2024, 11, 19, 13, 48, 31, 501, DateTimeKind.Local).AddTicks(281));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: new Guid("e0915908-c9d9-4c88-8137-b7c4625cd9a9"),
                column: "BOD",
                value: new DateTime(2024, 11, 19, 13, 48, 31, 501, DateTimeKind.Local).AddTicks(285));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: new Guid("f0915908-c9d9-4c88-8137-b7c4625cd9aa"),
                column: "BOD",
                value: new DateTime(2024, 11, 19, 13, 48, 31, 501, DateTimeKind.Local).AddTicks(290));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FlashSaleID",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Products_FlashSaleID",
                table: "Products",
                column: "FlashSaleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_FlashSales_FlashSaleID",
                table: "Products",
                column: "FlashSaleID",
                principalTable: "FlashSales",
                principalColumn: "ID");
        }
    }
}
