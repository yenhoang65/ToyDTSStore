using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class Updatepayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentCode",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentEndDate",
                table: "Payments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "UserCreateId",
                table: "Payments",
                type: "uniqueidentifier",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UserCreateId",
                table: "Payments",
                column: "UserCreateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Users_UserCreateId",
                table: "Payments",
                column: "UserCreateId",
                principalTable: "Users",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Users_UserCreateId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_UserCreateId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PaymentCode",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PaymentEndDate",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "UserCreateId",
                table: "Payments");

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
    }
}
