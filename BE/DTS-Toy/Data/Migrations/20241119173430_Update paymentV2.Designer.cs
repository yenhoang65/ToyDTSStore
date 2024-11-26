﻿// <auto-generated />
using System;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(ECommerceDBContext))]
    [Migration("20241119173430_Update paymentV2")]
    partial class UpdatepaymentV2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Data.Models.Address", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Data.Models.Brand", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BrandName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Brands");

                    b.HasData(
                        new
                        {
                            ID = new Guid("90915908-c9d9-4c88-8137-b7c4625cd9ad"),
                            BrandName = "Lego"
                        },
                        new
                        {
                            ID = new Guid("80915908-c9d9-4c88-8137-b7c4625cd9ae"),
                            BrandName = "Zozo"
                        });
                });

            modelBuilder.Entity("Data.Models.Category", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageNamesJson")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePathsJson")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ParentID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            ID = new Guid("7a0905a6-1ce3-4c50-ad2c-3808f24bc7a9"),
                            CategoryName = "Đồ chơi trẻ em",
                            Description = "đồ chơi trẻ em từ 1 - 4 tuổi"
                        },
                        new
                        {
                            ID = new Guid("8a0905a6-1ce3-4c50-ad2c-3808f24bc7aa"),
                            CategoryName = "Đồ chơi giáo dục",
                            Description = "đồ chơi giáo dục mọi lứa tuổi"
                        },
                        new
                        {
                            ID = new Guid("9a0905a6-1ce3-4c50-ad2c-3808f24bc7ab"),
                            CategoryName = "Đồ chơi mô hình",
                            Description = "đồ chơi mô hình"
                        });
                });

            modelBuilder.Entity("Data.Models.ContentPage", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageNamesJson")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePathsJson")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Introduction")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Video")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("ContentPages");

                    b.HasData(
                        new
                        {
                            ID = new Guid("d1069c03-6d5c-4c5e-9960-2562e7800e56"),
                            Address = "143 thanh xuân",
                            Description = "Đây là web bán đồ chơi",
                            Email = "Admin@example.com",
                            Introduction = "Đây là web bán đồ chơi thế hệ mới",
                            PhoneNumber = "0123456789",
                            Video = "https://www.youtube.com/watch?v=h52PcEuzUUA&list=RDh52PcEuzUUA&start_radio=1"
                        });
                });

            modelBuilder.Entity("Data.Models.Favorite", b =>
                {
                    b.Property<Guid>("ProductID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ProductID", "UserID");

                    b.HasIndex("UserID");

                    b.ToTable("Favorites");
                });

            modelBuilder.Entity("Data.Models.FlashSale", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ProductID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("ProductID");

                    b.ToTable("FlashSales");
                });

            modelBuilder.Entity("Data.Models.Order", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Data.Models.OrderDetail", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("OrderID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProductID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal?>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ID");

                    b.HasIndex("OrderID");

                    b.HasIndex("ProductID");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("Data.Models.Payment", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("OrderDetailID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PaymentCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("PaymentEndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentMethod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal?>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("UserPaymentId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("OrderDetailID");

                    b.HasIndex("UserPaymentId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("Data.Models.Permission", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PermissionName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Permissions");

                    b.HasData(
                        new
                        {
                            ID = new Guid("1c59f497-54fe-4306-a3b2-c88cd2b87a1b"),
                            PermissionName = "User"
                        },
                        new
                        {
                            ID = new Guid("06f10e0e-bddf-4f50-82ae-1f00fb28037a"),
                            PermissionName = "Admin"
                        },
                        new
                        {
                            ID = new Guid("bbc30cb3-43d4-4501-82f9-01764f61497e"),
                            PermissionName = "Manager"
                        });
                });

            modelBuilder.Entity("Data.Models.Product", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BrandID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CategoryID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Desciption")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ImageNamesJson")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePathsJson")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("PrevPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Sku")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("BrandID");

                    b.HasIndex("CategoryID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Data.Models.Rate", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ProductID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("RateValue")
                        .HasColumnType("int");

                    b.Property<Guid?>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("ProductID");

                    b.HasIndex("UserID");

                    b.ToTable("Rates");
                });

            modelBuilder.Entity("Data.Models.RoleDetail", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RoleDetailName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("RoleDetails");

                    b.HasData(
                        new
                        {
                            ID = new Guid("b0915908-c9d9-4c88-8137-b7c4625cd9ab"),
                            RoleDetailName = "Xem sản phẩm"
                        },
                        new
                        {
                            ID = new Guid("a0915908-c9d9-4c88-8137-b7c4625cd9ac"),
                            RoleDetailName = "Tìm kiếm sản phẩm"
                        });
                });

            modelBuilder.Entity("Data.Models.RolePermission", b =>
                {
                    b.Property<Guid>("PermissionID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("RoleDetailID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsEnable")
                        .HasColumnType("bit");

                    b.HasKey("PermissionID", "RoleDetailID");

                    b.HasIndex("RoleDetailID");

                    b.ToTable("RolePermissions");

                    b.HasData(
                        new
                        {
                            PermissionID = new Guid("1c59f497-54fe-4306-a3b2-c88cd2b87a1b"),
                            RoleDetailID = new Guid("b0915908-c9d9-4c88-8137-b7c4625cd9ab"),
                            IsEnable = false
                        },
                        new
                        {
                            PermissionID = new Guid("06f10e0e-bddf-4f50-82ae-1f00fb28037a"),
                            RoleDetailID = new Guid("a0915908-c9d9-4c88-8137-b7c4625cd9ac"),
                            IsEnable = false
                        });
                });

            modelBuilder.Entity("Data.Models.User", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AccessToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("AccessTokenCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("AvatarName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AvatarPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("BOD")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OrderID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RefreshTokenCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("RefreshTokenExpires")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("RoleID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("OrderID");

                    b.HasIndex("RoleID");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            ID = new Guid("c0915908-c9d9-4c88-8137-b7c4625cd9a7"),
                            AvatarName = "avt",
                            BOD = new DateTime(2024, 11, 20, 0, 34, 29, 123, DateTimeKind.Local).AddTicks(8805),
                            Email = "admin@gmail.com",
                            FullName = "admin",
                            Gender = "Name",
                            PasswordHash = "A665A45920422F9D417E4867EFDC4FB8A04A1F3FFF1FA07E998E86F7F7A27AE3",
                            PhoneNumber = "9876",
                            RoleID = new Guid("06f10e0e-bddf-4f50-82ae-1f00fb28037a"),
                            Status = "Active"
                        },
                        new
                        {
                            ID = new Guid("d0915908-c9d9-4c88-8137-b7c4625cd9a8"),
                            AvatarName = "avt",
                            BOD = new DateTime(2024, 11, 20, 0, 34, 29, 123, DateTimeKind.Local).AddTicks(8826),
                            Email = "manage@gmail.com",
                            FullName = "Manage",
                            Gender = "Name",
                            PasswordHash = "A665A45920422F9D417E4867EFDC4FB8A04A1F3FFF1FA07E998E86F7F7A27AE3",
                            PhoneNumber = "0123456879",
                            RoleID = new Guid("bbc30cb3-43d4-4501-82f9-01764f61497e"),
                            Status = "Active"
                        },
                        new
                        {
                            ID = new Guid("e0915908-c9d9-4c88-8137-b7c4625cd9a9"),
                            AvatarName = "avt",
                            BOD = new DateTime(2024, 11, 20, 0, 34, 29, 123, DateTimeKind.Local).AddTicks(8831),
                            Email = "pvp@gmail.com",
                            FullName = "pvp",
                            Gender = "Name",
                            PasswordHash = "A665A45920422F9D417E4867EFDC4FB8A04A1F3FFF1FA07E998E86F7F7A27AE3",
                            PhoneNumber = "023456879",
                            RoleID = new Guid("1c59f497-54fe-4306-a3b2-c88cd2b87a1b"),
                            Status = "Active"
                        },
                        new
                        {
                            ID = new Guid("f0915908-c9d9-4c88-8137-b7c4625cd9aa"),
                            AvatarName = "avt",
                            BOD = new DateTime(2024, 11, 20, 0, 34, 29, 123, DateTimeKind.Local).AddTicks(8834),
                            Email = "hqh@gmail.com",
                            FullName = "hqh",
                            Gender = "Name",
                            PasswordHash = "A665A45920422F9D417E4867EFDC4FB8A04A1F3FFF1FA07E998E86F7F7A27AE3",
                            PhoneNumber = "12311111",
                            RoleID = new Guid("1c59f497-54fe-4306-a3b2-c88cd2b87a1b"),
                            Status = "Active"
                        });
                });

            modelBuilder.Entity("Data.Models.Address", b =>
                {
                    b.HasOne("Data.Models.User", "User")
                        .WithMany("Addresses")
                        .HasForeignKey("UserID");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Data.Models.Favorite", b =>
                {
                    b.HasOne("Data.Models.Product", "Product")
                        .WithMany("Favorites")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Models.User", "User")
                        .WithMany("Favorites")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Data.Models.FlashSale", b =>
                {
                    b.HasOne("Data.Models.Product", "Product")
                        .WithMany("FlashSales")
                        .HasForeignKey("ProductID");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Data.Models.Order", b =>
                {
                    b.HasOne("Data.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Data.Models.OrderDetail", b =>
                {
                    b.HasOne("Data.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderID");

                    b.HasOne("Data.Models.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductID");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Data.Models.Payment", b =>
                {
                    b.HasOne("Data.Models.OrderDetail", "OrderDetail")
                        .WithMany()
                        .HasForeignKey("OrderDetailID");

                    b.HasOne("Data.Models.User", "UserPayment")
                        .WithMany()
                        .HasForeignKey("UserPaymentId");

                    b.Navigation("OrderDetail");

                    b.Navigation("UserPayment");
                });

            modelBuilder.Entity("Data.Models.Product", b =>
                {
                    b.HasOne("Data.Models.Brand", "Brand")
                        .WithMany("Products")
                        .HasForeignKey("BrandID");

                    b.HasOne("Data.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryID");

                    b.Navigation("Brand");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Data.Models.Rate", b =>
                {
                    b.HasOne("Data.Models.Product", "Product")
                        .WithMany("Rates")
                        .HasForeignKey("ProductID");

                    b.HasOne("Data.Models.User", "User")
                        .WithMany("Rates")
                        .HasForeignKey("UserID");

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Data.Models.RolePermission", b =>
                {
                    b.HasOne("Data.Models.Permission", "Permission")
                        .WithMany("RolePermissions")
                        .HasForeignKey("PermissionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Models.RoleDetail", "RoleDetail")
                        .WithMany()
                        .HasForeignKey("RoleDetailID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("RoleDetail");
                });

            modelBuilder.Entity("Data.Models.User", b =>
                {
                    b.HasOne("Data.Models.Order", null)
                        .WithMany("Users")
                        .HasForeignKey("OrderID");

                    b.HasOne("Data.Models.Permission", "Role")
                        .WithMany()
                        .HasForeignKey("RoleID");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Data.Models.Brand", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Data.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Data.Models.Order", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Data.Models.Permission", b =>
                {
                    b.Navigation("RolePermissions");
                });

            modelBuilder.Entity("Data.Models.Product", b =>
                {
                    b.Navigation("Favorites");

                    b.Navigation("FlashSales");

                    b.Navigation("OrderDetails");

                    b.Navigation("Rates");
                });

            modelBuilder.Entity("Data.Models.User", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Favorites");

                    b.Navigation("Rates");
                });
#pragma warning restore 612, 618
        }
    }
}
