using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class Updateseedsample : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: new Guid("d8a416a7-207f-4083-ad51-49c9c6ac1635"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: new Guid("dac5014b-b816-4def-b810-6521b6d378a6"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: new Guid("e81db790-69f8-4215-a9c5-e9b646adcae8"));

            migrationBuilder.DeleteData(
                table: "ContentPages",
                keyColumn: "ID",
                keyValue: new Guid("3e5c6586-e840-4e46-9b33-555ddd2def12"));

            migrationBuilder.AddColumn<Guid>(
                name: "OrderID",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FlashSaleID",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "ID", "BrandName", "Link" },
                values: new object[,]
                {
                    { new Guid("80915908-c9d9-4c88-8137-b7c4625cd9ae"), "Zozo", null },
                    { new Guid("90915908-c9d9-4c88-8137-b7c4625cd9ad"), "Lego", null }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "ID", "CategoryName", "Description", "ImageNamesJson", "ImagePathsJson", "ParentID" },
                values: new object[,]
                {
                    { new Guid("7a0905a6-1ce3-4c50-ad2c-3808f24bc7a9"), "Đồ chơi trẻ em", "đồ chơi trẻ em từ 1 - 4 tuổi", null, null, null },
                    { new Guid("8a0905a6-1ce3-4c50-ad2c-3808f24bc7aa"), "Đồ chơi giáo dục", "đồ chơi giáo dục mọi lứa tuổi", null, null, null },
                    { new Guid("9a0905a6-1ce3-4c50-ad2c-3808f24bc7ab"), "Đồ chơi mô hình", "đồ chơi mô hình", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "ContentPages",
                columns: new[] { "ID", "Address", "Description", "Email", "ImageNamesJson", "ImagePathsJson", "Introduction", "PhoneNumber", "Video" },
                values: new object[] { new Guid("d1069c03-6d5c-4c5e-9960-2562e7800e56"), "143 thanh xuân", "Đây là web bán đồ chơi", "Admin@example.com", null, null, "Đây là web bán đồ chơi thế hệ mới", "0123456789", "https://www.youtube.com/watch?v=h52PcEuzUUA&list=RDh52PcEuzUUA&start_radio=1" });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "ID", "PermissionName" },
                values: new object[,]
                {
                    { new Guid("06f10e0e-bddf-4f50-82ae-1f00fb28037a"), "Admin" },
                    { new Guid("1c59f497-54fe-4306-a3b2-c88cd2b87a1b"), "User" },
                    { new Guid("bbc30cb3-43d4-4501-82f9-01764f61497e"), "Manager" }
                });

            migrationBuilder.InsertData(
                table: "RoleDetails",
                columns: new[] { "ID", "RoleDetailName" },
                values: new object[,]
                {
                    { new Guid("a0915908-c9d9-4c88-8137-b7c4625cd9ac"), "Tìm kiếm sản phẩm" },
                    { new Guid("b0915908-c9d9-4c88-8137-b7c4625cd9ab"), "Xem sản phẩm" }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "PermissionID", "RoleDetailID", "IsEnable" },
                values: new object[,]
                {
                    { new Guid("06f10e0e-bddf-4f50-82ae-1f00fb28037a"), new Guid("a0915908-c9d9-4c88-8137-b7c4625cd9ac"), false },
                    { new Guid("1c59f497-54fe-4306-a3b2-c88cd2b87a1b"), new Guid("b0915908-c9d9-4c88-8137-b7c4625cd9ab"), false }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "AccessToken", "AccessTokenCreated", "AvatarName", "AvatarPath", "BOD", "Email", "FullName", "Gender", "OrderID", "PasswordHash", "PhoneNumber", "RefreshToken", "RefreshTokenCreated", "RefreshTokenExpires", "RoleID", "Status" },
                values: new object[,]
                {
                    { new Guid("c0915908-c9d9-4c88-8137-b7c4625cd9a7"), null, null, "avt", null, new DateTime(2024, 11, 18, 11, 38, 47, 833, DateTimeKind.Local).AddTicks(1742), "admin@gmail.com", "admin", "Name", null, "A665A45920422F9D417E4867EFDC4FB8A04A1F3FFF1FA07E998E86F7F7A27AE3", "9876", null, null, null, new Guid("06f10e0e-bddf-4f50-82ae-1f00fb28037a"), "Active" },
                    { new Guid("d0915908-c9d9-4c88-8137-b7c4625cd9a8"), null, null, "avt", null, new DateTime(2024, 11, 18, 11, 38, 47, 833, DateTimeKind.Local).AddTicks(1765), "manage@gmail.com", "Manage", "Name", null, "A665A45920422F9D417E4867EFDC4FB8A04A1F3FFF1FA07E998E86F7F7A27AE3", "0123456879", null, null, null, new Guid("bbc30cb3-43d4-4501-82f9-01764f61497e"), "Active" },
                    { new Guid("e0915908-c9d9-4c88-8137-b7c4625cd9a9"), null, null, "avt", null, new DateTime(2024, 11, 18, 11, 38, 47, 833, DateTimeKind.Local).AddTicks(1769), "pvp@gmail.com", "pvp", "Name", null, "A665A45920422F9D417E4867EFDC4FB8A04A1F3FFF1FA07E998E86F7F7A27AE3", "023456879", null, null, null, new Guid("1c59f497-54fe-4306-a3b2-c88cd2b87a1b"), "Active" },
                    { new Guid("f0915908-c9d9-4c88-8137-b7c4625cd9aa"), null, null, "avt", null, new DateTime(2024, 11, 18, 11, 38, 47, 833, DateTimeKind.Local).AddTicks(1825), "hqh@gmail.com", "hqh", "Name", null, "A665A45920422F9D417E4867EFDC4FB8A04A1F3FFF1FA07E998E86F7F7A27AE3", "12311111", null, null, null, new Guid("1c59f497-54fe-4306-a3b2-c88cd2b87a1b"), "Active" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_OrderID",
                table: "Users",
                column: "OrderID");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Orders_OrderID",
                table: "Users",
                column: "OrderID",
                principalTable: "Orders",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_FlashSales_FlashSaleID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Orders_OrderID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_OrderID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Products_FlashSaleID",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: new Guid("80915908-c9d9-4c88-8137-b7c4625cd9ae"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: new Guid("90915908-c9d9-4c88-8137-b7c4625cd9ad"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: new Guid("7a0905a6-1ce3-4c50-ad2c-3808f24bc7a9"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: new Guid("8a0905a6-1ce3-4c50-ad2c-3808f24bc7aa"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: new Guid("9a0905a6-1ce3-4c50-ad2c-3808f24bc7ab"));

            migrationBuilder.DeleteData(
                table: "ContentPages",
                keyColumn: "ID",
                keyValue: new Guid("d1069c03-6d5c-4c5e-9960-2562e7800e56"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionID", "RoleDetailID" },
                keyValues: new object[] { new Guid("06f10e0e-bddf-4f50-82ae-1f00fb28037a"), new Guid("a0915908-c9d9-4c88-8137-b7c4625cd9ac") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionID", "RoleDetailID" },
                keyValues: new object[] { new Guid("1c59f497-54fe-4306-a3b2-c88cd2b87a1b"), new Guid("b0915908-c9d9-4c88-8137-b7c4625cd9ab") });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "ID",
                keyValue: new Guid("c0915908-c9d9-4c88-8137-b7c4625cd9a7"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "ID",
                keyValue: new Guid("d0915908-c9d9-4c88-8137-b7c4625cd9a8"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "ID",
                keyValue: new Guid("e0915908-c9d9-4c88-8137-b7c4625cd9a9"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "ID",
                keyValue: new Guid("f0915908-c9d9-4c88-8137-b7c4625cd9aa"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "ID",
                keyValue: new Guid("06f10e0e-bddf-4f50-82ae-1f00fb28037a"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "ID",
                keyValue: new Guid("1c59f497-54fe-4306-a3b2-c88cd2b87a1b"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "ID",
                keyValue: new Guid("bbc30cb3-43d4-4501-82f9-01764f61497e"));

            migrationBuilder.DeleteData(
                table: "RoleDetails",
                keyColumn: "ID",
                keyValue: new Guid("a0915908-c9d9-4c88-8137-b7c4625cd9ac"));

            migrationBuilder.DeleteData(
                table: "RoleDetails",
                keyColumn: "ID",
                keyValue: new Guid("b0915908-c9d9-4c88-8137-b7c4625cd9ab"));

            migrationBuilder.DropColumn(
                name: "OrderID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FlashSaleID",
                table: "Products");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "ID", "CategoryName", "Description", "ImageNamesJson", "ImagePathsJson", "ParentID" },
                values: new object[,]
                {
                    { new Guid("d8a416a7-207f-4083-ad51-49c9c6ac1635"), "Đồ chơi trẻ em", "đồ chơi trẻ em từ 1 - 4 tuổi", null, null, null },
                    { new Guid("dac5014b-b816-4def-b810-6521b6d378a6"), "Đồ chơi giáo dục", "đồ chơi giáo dục mọi lứa tuổi", null, null, null },
                    { new Guid("e81db790-69f8-4215-a9c5-e9b646adcae8"), "Đồ chơi mô hình", "đồ chơi mô hình", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "ContentPages",
                columns: new[] { "ID", "Address", "Description", "Email", "ImageNamesJson", "ImagePathsJson", "Introduction", "PhoneNumber", "Video" },
                values: new object[] { new Guid("3e5c6586-e840-4e46-9b33-555ddd2def12"), "143 thanh xuân", "Đây là web bán đồ chơi", "Admin@example.com", null, null, "Đây là web bán đồ chơi thế hệ mới", "0123456789", "https://www.youtube.com/watch?v=h52PcEuzUUA&list=RDh52PcEuzUUA&start_radio=1" });
        }
    }
}
