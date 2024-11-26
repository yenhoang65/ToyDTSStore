using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class addseedata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
