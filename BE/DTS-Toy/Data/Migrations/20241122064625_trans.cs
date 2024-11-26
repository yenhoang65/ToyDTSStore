using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class trans : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Translations",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TranslationKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TranslationValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translations", x => x.ID);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: new Guid("c0915908-c9d9-4c88-8137-b7c4625cd9a7"),
                column: "BOD",
                value: new DateTime(2024, 11, 22, 13, 46, 24, 958, DateTimeKind.Local).AddTicks(23));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: new Guid("d0915908-c9d9-4c88-8137-b7c4625cd9a8"),
                column: "BOD",
                value: new DateTime(2024, 11, 22, 13, 46, 24, 958, DateTimeKind.Local).AddTicks(40));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: new Guid("e0915908-c9d9-4c88-8137-b7c4625cd9a9"),
                column: "BOD",
                value: new DateTime(2024, 11, 22, 13, 46, 24, 958, DateTimeKind.Local).AddTicks(44));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: new Guid("f0915908-c9d9-4c88-8137-b7c4625cd9aa"),
                column: "BOD",
                value: new DateTime(2024, 11, 22, 13, 46, 24, 958, DateTimeKind.Local).AddTicks(47));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Translations");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: new Guid("c0915908-c9d9-4c88-8137-b7c4625cd9a7"),
                column: "BOD",
                value: new DateTime(2024, 11, 20, 0, 34, 29, 123, DateTimeKind.Local).AddTicks(8805));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: new Guid("d0915908-c9d9-4c88-8137-b7c4625cd9a8"),
                column: "BOD",
                value: new DateTime(2024, 11, 20, 0, 34, 29, 123, DateTimeKind.Local).AddTicks(8826));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: new Guid("e0915908-c9d9-4c88-8137-b7c4625cd9a9"),
                column: "BOD",
                value: new DateTime(2024, 11, 20, 0, 34, 29, 123, DateTimeKind.Local).AddTicks(8831));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: new Guid("f0915908-c9d9-4c88-8137-b7c4625cd9aa"),
                column: "BOD",
                value: new DateTime(2024, 11, 20, 0, 34, 29, 123, DateTimeKind.Local).AddTicks(8834));
        }
    }
}
