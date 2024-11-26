using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class UpdatepaymentV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Users_UserCreateId",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "UserCreateId",
                table: "Payments",
                newName: "UserPaymentId");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_UserCreateId",
                table: "Payments",
                newName: "IX_Payments_UserPaymentId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Users_UserPaymentId",
                table: "Payments",
                column: "UserPaymentId",
                principalTable: "Users",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Users_UserPaymentId",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "UserPaymentId",
                table: "Payments",
                newName: "UserCreateId");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_UserPaymentId",
                table: "Payments",
                newName: "IX_Payments_UserCreateId");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: new Guid("c0915908-c9d9-4c88-8137-b7c4625cd9a7"),
                column: "BOD",
                value: new DateTime(2024, 11, 19, 16, 44, 6, 608, DateTimeKind.Local).AddTicks(6716));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: new Guid("d0915908-c9d9-4c88-8137-b7c4625cd9a8"),
                column: "BOD",
                value: new DateTime(2024, 11, 19, 16, 44, 6, 608, DateTimeKind.Local).AddTicks(6733));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: new Guid("e0915908-c9d9-4c88-8137-b7c4625cd9a9"),
                column: "BOD",
                value: new DateTime(2024, 11, 19, 16, 44, 6, 608, DateTimeKind.Local).AddTicks(6737));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: new Guid("f0915908-c9d9-4c88-8137-b7c4625cd9aa"),
                column: "BOD",
                value: new DateTime(2024, 11, 19, 16, 44, 6, 608, DateTimeKind.Local).AddTicks(6740));

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Users_UserCreateId",
                table: "Payments",
                column: "UserCreateId",
                principalTable: "Users",
                principalColumn: "ID");
        }
    }
}
