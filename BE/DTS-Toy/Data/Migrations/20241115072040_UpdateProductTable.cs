using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class UpdateProductTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Rates_RateID",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_RateID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "RateID",
                table: "Products");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductID",
                table: "Rates",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rates_ProductID",
                table: "Rates",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_Rates_Products_ProductID",
                table: "Rates",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rates_Products_ProductID",
                table: "Rates");

            migrationBuilder.DropIndex(
                name: "IX_Rates_ProductID",
                table: "Rates");

            migrationBuilder.DropColumn(
                name: "ProductID",
                table: "Rates");

            migrationBuilder.AddColumn<Guid>(
                name: "RateID",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_RateID",
                table: "Products",
                column: "RateID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Rates_RateID",
                table: "Products",
                column: "RateID",
                principalTable: "Rates",
                principalColumn: "ID");
        }
    }
}
