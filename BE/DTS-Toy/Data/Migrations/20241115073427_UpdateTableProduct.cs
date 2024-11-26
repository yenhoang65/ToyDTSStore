using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class UpdateTableProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brands_Products_ProductID",
                table: "Brands");

            migrationBuilder.DropIndex(
                name: "IX_Brands_ProductID",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "ProductID",
                table: "Brands");

            migrationBuilder.AddColumn<Guid>(
                name: "BrandID",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Desciption",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sku",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandID",
                table: "Products",
                column: "BrandID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_BrandID",
                table: "Products",
                column: "BrandID",
                principalTable: "Brands",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_BrandID",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_BrandID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "BrandID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Desciption",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Sku",
                table: "Products");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductID",
                table: "Brands",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Brands_ProductID",
                table: "Brands",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_Products_ProductID",
                table: "Brands",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID");
        }
    }
}
