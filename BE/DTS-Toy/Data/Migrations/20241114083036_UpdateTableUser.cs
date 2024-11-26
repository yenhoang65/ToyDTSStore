using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class UpdateTableUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Addresses_AddressID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_AddressID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AddressID",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "UserID",
                table: "Addresses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_UserID",
                table: "Addresses",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Users_UserID",
                table: "Addresses",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Users_UserID",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_UserID",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Addresses");

            migrationBuilder.AddColumn<Guid>(
                name: "AddressID",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_AddressID",
                table: "Users",
                column: "AddressID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Addresses_AddressID",
                table: "Users",
                column: "AddressID",
                principalTable: "Addresses",
                principalColumn: "ID");
        }
    }
}
