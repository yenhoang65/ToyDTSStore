using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Data.Models
{
    public class ECommerceDBContext : DbContext
    {
        public ECommerceDBContext() { }

        public ECommerceDBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning   if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("MyCnn"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Translation>()
            .HasKey(t => t.ID);
            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new { rp.PermissionID, rp.RoleDetailID });


            modelBuilder.Entity<Favorite>()
                .HasKey(f => new { f.ProductID, f.UserID });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(p => p.ImagePathsJson)
                    .HasColumnType("nvarchar(max)");

                entity.Property(p => p.ImageNamesJson)
                    .HasColumnType("nvarchar(max)");
            });


            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(c => c.ImagePathsJson)
                    .HasColumnType("nvarchar(max)");

                entity.Property(c => c.ImageNamesJson)
                    .HasColumnType("nvarchar(max)");
            });

            modelBuilder.Entity<ContentPage>(entity =>
            {
                entity.Property(cp => cp.ImagePathsJson)
                    .HasColumnType("nvarchar(max)");

                entity.Property(cp => cp.ImageNamesJson)
                    .HasColumnType("nvarchar(max)");
            });


            base.OnModelCreating(modelBuilder);

            seedData(modelBuilder);

        }

        private void seedData(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ContentPage>().HasData(
                new ContentPage
                {
                    ID = Guid.Parse("d1069c03-6d5c-4c5e-9960-2562e7800e56"),
                    Email = "Admin@example.com",
                    Address = "143 thanh xuân",
                    Video = "https://www.youtube.com/watch?v=h52PcEuzUUA&list=RDh52PcEuzUUA&start_radio=1",
                    Description = "Đây là web bán đồ chơi",
                    PhoneNumber = "0123456789",
                    Introduction = "Đây là web bán đồ chơi thế hệ mới"
                }
            );

            // Category seed with fixed GUIDs
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    ID = Guid.Parse("7a0905a6-1ce3-4c50-ad2c-3808f24bc7a9"),
                    CategoryName = "Đồ chơi trẻ em",
                    Description = "đồ chơi trẻ em từ 1 - 4 tuổi"
                },
                new Category
                {
                    ID = Guid.Parse("8a0905a6-1ce3-4c50-ad2c-3808f24bc7aa"),
                    CategoryName = "Đồ chơi giáo dục",
                    Description = "đồ chơi giáo dục mọi lứa tuổi"
                },
                new Category
                {
                    ID = Guid.Parse("9a0905a6-1ce3-4c50-ad2c-3808f24bc7ab"),
                    CategoryName = "Đồ chơi mô hình",
                    Description = "đồ chơi mô hình"
                }
            );

            // Keep existing Permission seed data as it already has fixed GUIDs
            modelBuilder.Entity<Permission>().HasData(
               new Permission { ID = Guid.Parse("1c59f497-54fe-4306-a3b2-c88cd2b87a1b"), PermissionName = "User" },
               new Permission { ID = Guid.Parse("06f10e0e-bddf-4f50-82ae-1f00fb28037a"), PermissionName = "Admin" },
               new Permission { ID = Guid.Parse("bbc30cb3-43d4-4501-82f9-01764f61497e"), PermissionName = "Manager" }
           );

            modelBuilder.Entity<RoleDetail>().HasData(
               new RoleDetail
               {
                   ID = Guid.Parse("b0915908-c9d9-4c88-8137-b7c4625cd9ab"),
                   RoleDetailName = "Xem sản phẩm",
               },
               new RoleDetail
               {
                   ID = Guid.Parse("a0915908-c9d9-4c88-8137-b7c4625cd9ac"),
                   RoleDetailName = "Tìm kiếm sản phẩm",
               }
           );

            modelBuilder.Entity<RolePermission>().HasData(
                new RolePermission
                {
                    RoleDetailID = Guid.Parse("b0915908-c9d9-4c88-8137-b7c4625cd9ab"),
                    PermissionID = Guid.Parse("1c59f497-54fe-4306-a3b2-c88cd2b87a1b")
                },
                new RolePermission
                {
                    RoleDetailID = Guid.Parse("a0915908-c9d9-4c88-8137-b7c4625cd9ac"),
                    PermissionID = Guid.Parse("06f10e0e-bddf-4f50-82ae-1f00fb28037a")
                }
            );


            // User seed with fixed GUIDs
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    ID = Guid.Parse("c0915908-c9d9-4c88-8137-b7c4625cd9a7"),
                    FullName = "admin",
                    Email = "admin@gmail.com",
                    PhoneNumber = "9876",
                    Gender = "Name",
                    BOD = DateTime.Now,
                    AvatarPath = null,
                    PasswordHash = "A665A45920422F9D417E4867EFDC4FB8A04A1F3FFF1FA07E998E86F7F7A27AE3",
                    RefreshToken = null,
                    RefreshTokenCreated = null,
                    RefreshTokenExpires = null,
                    AccessToken = null,
                    AccessTokenCreated = null,
                    Status = "Active",
                    RoleID = Guid.Parse("06f10e0e-bddf-4f50-82ae-1f00fb28037a"),
                    AvatarName = "avt"
                },
                new User
                {
                    ID = Guid.Parse("d0915908-c9d9-4c88-8137-b7c4625cd9a8"),
                    FullName = "Manage",
                    Email = "manage@gmail.com",
                    PhoneNumber = "0123456879",
                    Gender = "Name",
                    BOD = DateTime.Now,
                    AvatarPath = null,
                    PasswordHash = "A665A45920422F9D417E4867EFDC4FB8A04A1F3FFF1FA07E998E86F7F7A27AE3",
                    RefreshToken = null,
                    RefreshTokenCreated = null,
                    RefreshTokenExpires = null,
                    AccessToken = null,
                    AccessTokenCreated = null,
                    Status = "Active",
                    RoleID = Guid.Parse("bbc30cb3-43d4-4501-82f9-01764f61497e"),
                    AvatarName = "avt"
                },
                new User
                {
                    ID = Guid.Parse("e0915908-c9d9-4c88-8137-b7c4625cd9a9"),
                    FullName = "pvp",
                    Email = "pvp@gmail.com",
                    PhoneNumber = "023456879",
                    Gender = "Name",
                    BOD = DateTime.Now,
                    AvatarPath = null,
                    PasswordHash = "A665A45920422F9D417E4867EFDC4FB8A04A1F3FFF1FA07E998E86F7F7A27AE3",
                    RefreshToken = null,
                    RefreshTokenCreated = null,
                    RefreshTokenExpires = null,
                    AccessToken = null,
                    AccessTokenCreated = null,
                    Status = "Active",
                    RoleID = Guid.Parse("1c59f497-54fe-4306-a3b2-c88cd2b87a1b"),
                    AvatarName = "avt"
                },
                new User
                {
                    ID = Guid.Parse("f0915908-c9d9-4c88-8137-b7c4625cd9aa"),
                    FullName = "hqh",
                    Email = "hqh@gmail.com",
                    PhoneNumber = "12311111",
                    Gender = "Name",
                    BOD = DateTime.Now,
                    AvatarPath = null,
                    PasswordHash = "A665A45920422F9D417E4867EFDC4FB8A04A1F3FFF1FA07E998E86F7F7A27AE3",
                    RefreshToken = null,
                    RefreshTokenCreated = null,
                    RefreshTokenExpires = null,
                    AccessToken = null,
                    AccessTokenCreated = null,
                    Status = "Active",
                    RoleID = Guid.Parse("1c59f497-54fe-4306-a3b2-c88cd2b87a1b"),
                    AvatarName = "avt"
                }
            );

            // RoleDetail seed with fixed GUIDs


            // Brand seed with fixed GUIDs
            modelBuilder.Entity<Brand>().HasData(
                new Brand
                {
                    ID = Guid.Parse("90915908-c9d9-4c88-8137-b7c4625cd9ad"),
                    BrandName = "Lego",
                    Link = null
                },
                new Brand
                {
                    ID = Guid.Parse("80915908-c9d9-4c88-8137-b7c4625cd9ae"),
                    BrandName = "Zozo",
                    Link = null
                }
            );
        }
        public DbSet<Translation> Translations { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RoleDetail> RoleDetails { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<FlashSale> FlashSales { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ContentPage> ContentPages { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Rate> Rates { get; set; }
    }




}
