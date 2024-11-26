using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class UpdateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Images_ImageID",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Images_ImageID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Permissions_PermissionID",
                table: "RolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Roles_RoleID",
                table: "RolePermissions");

            migrationBuilder.DropTable(
                name: "ContentImages");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RolePermissions",
                table: "RolePermissions");

            migrationBuilder.DropIndex(
                name: "IX_RolePermissions_PermissionID",
                table: "RolePermissions");

            migrationBuilder.DropIndex(
                name: "IX_Products_ImageID",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ImageID",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ImageID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ImageID",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "RoleName",
                table: "Roles",
                newName: "PermissionName");

            migrationBuilder.RenameColumn(
                name: "RoleID",
                table: "RolePermissions",
                newName: "RoleDetailID");

            migrationBuilder.RenameColumn(
                name: "PermissionName",
                table: "Permissions",
                newName: "RoleDetailName");

            migrationBuilder.AddColumn<string>(
                name: "AvatarName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageNamesJson",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePathsJson",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageNamesJson",
                table: "ContentPages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePathsJson",
                table: "ContentPages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageNamesJson",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePathsJson",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolePermissions",
                table: "RolePermissions",
                columns: new[] { "PermissionID", "RoleDetailID" });

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleDetailID",
                table: "RolePermissions",
                column: "RoleDetailID");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Permissions_RoleDetailID",
                table: "RolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Roles_PermissionID",
                table: "RolePermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RolePermissions",
                table: "RolePermissions");

            migrationBuilder.DropIndex(
                name: "IX_RolePermissions_RoleDetailID",
                table: "RolePermissions");

            migrationBuilder.DropColumn(
                name: "AvatarName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ImageNamesJson",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ImagePathsJson",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ImageNamesJson",
                table: "ContentPages");

            migrationBuilder.DropColumn(
                name: "ImagePathsJson",
                table: "ContentPages");

            migrationBuilder.DropColumn(
                name: "ImageNamesJson",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ImagePathsJson",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "PermissionName",
                table: "Roles",
                newName: "RoleName");

            migrationBuilder.RenameColumn(
                name: "RoleDetailID",
                table: "RolePermissions",
                newName: "RoleID");

            migrationBuilder.RenameColumn(
                name: "RoleDetailName",
                table: "Permissions",
                newName: "PermissionName");

            migrationBuilder.AddColumn<Guid>(
                name: "ImageID",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ImageID",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolePermissions",
                table: "RolePermissions",
                columns: new[] { "RoleID", "PermissionID" });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImgName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImgPath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ContentImages",
                columns: table => new
                {
                    ImageID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContentPageID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentImages", x => new { x.ImageID, x.ContentID });
                    table.ForeignKey(
                        name: "FK_ContentImages_ContentPages_ContentPageID",
                        column: x => x.ContentPageID,
                        principalTable: "ContentPages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContentImages_Images_ImageID",
                        column: x => x.ImageID,
                        principalTable: "Images",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => new { x.ProductID, x.ImageID });
                    table.ForeignKey(
                        name: "FK_ProductImages_Images_ImageID",
                        column: x => x.ImageID,
                        principalTable: "Images",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionID",
                table: "RolePermissions",
                column: "PermissionID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ImageID",
                table: "Products",
                column: "ImageID");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ImageID",
                table: "Categories",
                column: "ImageID");

            migrationBuilder.CreateIndex(
                name: "IX_ContentImages_ContentPageID",
                table: "ContentImages",
                column: "ContentPageID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ImageID",
                table: "ProductImages",
                column: "ImageID");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Images_ImageID",
                table: "Categories",
                column: "ImageID",
                principalTable: "Images",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Images_ImageID",
                table: "Products",
                column: "ImageID",
                principalTable: "Images",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_Permissions_PermissionID",
                table: "RolePermissions",
                column: "PermissionID",
                principalTable: "Permissions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_Roles_RoleID",
                table: "RolePermissions",
                column: "RoleID",
                principalTable: "Roles",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
