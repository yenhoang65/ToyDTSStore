using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class UpdateDatabasev2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Permissions_RoleDetailID",
                table: "RolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Roles_PermissionID",
                table: "RolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleID",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.RenameColumn(
                name: "RoleDetailName",
                table: "Permissions",
                newName: "PermissionName");

            migrationBuilder.CreateTable(
                name: "RoleDetails",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleDetailName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleDetails", x => x.ID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_Permissions_PermissionID",
                table: "RolePermissions",
                column: "PermissionID",
                principalTable: "Permissions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_RoleDetails_RoleDetailID",
                table: "RolePermissions",
                column: "RoleDetailID",
                principalTable: "RoleDetails",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Permissions_RoleID",
                table: "Users",
                column: "RoleID",
                principalTable: "Permissions",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Permissions_PermissionID",
                table: "RolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_RoleDetails_RoleDetailID",
                table: "RolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Permissions_RoleID",
                table: "Users");

            migrationBuilder.DropTable(
                name: "RoleDetails");

            migrationBuilder.RenameColumn(
                name: "PermissionName",
                table: "Permissions",
                newName: "RoleDetailName");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.ID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_Permissions_RoleDetailID",
                table: "RolePermissions",
                column: "RoleDetailID",
                principalTable: "Permissions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_Roles_PermissionID",
                table: "RolePermissions",
                column: "PermissionID",
                principalTable: "Roles",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleID",
                table: "Users",
                column: "RoleID",
                principalTable: "Roles",
                principalColumn: "ID");
        }
    }
}
