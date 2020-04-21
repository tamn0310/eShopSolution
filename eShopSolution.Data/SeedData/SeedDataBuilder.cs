using eShopSolution.Data.Entities;
using eShopSolution.Data.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace eShopSolution.Data.SeedData
{
    public static class SeedDataBuilder
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            //data table AppConfig
            modelBuilder.Entity<AppConfig>().HasData(
               new AppConfig() { Key = "HomeTitle", Value = "This is home page of eShopSolution" },
               new AppConfig() { Key = "HomeKeyword", Value = "This is keyword of eShopSolution" },
               new AppConfig() { Key = "HomeDescription", Value = "This is description of eShopSolution" }
               );

            //data table Language
            modelBuilder.Entity<Language>().HasData(
                new Language() { Id = "vi-VN", Name = "Tiếng Việt", IsDefaults = true },
                new Language() { Id = "en-US", Name = "English", IsDefaults = false });

            //data table Category
            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    Id = 1,
                    IsShowHome = true,
                    ParentId = null,
                    SortOrder = 1,
                    Status = Status.Active,
                },
                 new Category()
                 {
                     Id = 2,
                     IsShowHome = true,
                     ParentId = null,
                     SortOrder = 2,
                     Status = Status.Active
                 });

            //data table CategoryTranslation
            modelBuilder.Entity<CategoryTranslation>().HasData(
                  new CategoryTranslation() { Id = 1, CategoryId = 1, Name = "Áo nam", LanguageId = "vi-VN", SeoAlias = "ao-nam", SeoDescription = "Sản phẩm áo thời trang nam", SeoTitle = "Sản phẩm áo thời trang nam" },
                  new CategoryTranslation() { Id = 2, CategoryId = 1, Name = "Men Shirt", LanguageId = "en-US", SeoAlias = "men-shirt", SeoDescription = "The shirt products for men", SeoTitle = "The shirt products for men" },
                  new CategoryTranslation() { Id = 3, CategoryId = 2, Name = "Áo nữ", LanguageId = "vi-VN", SeoAlias = "ao-nu", SeoDescription = "Sản phẩm áo thời trang nữ", SeoTitle = "Sản phẩm áo thời trang women" },
                  new CategoryTranslation() { Id = 4, CategoryId = 2, Name = "Women Shirt", LanguageId = "en-US", SeoAlias = "women-shirt", SeoDescription = "The shirt products for women", SeoTitle = "The shirt products for women" }
                    );

            //data table Product
            modelBuilder.Entity<Product>().HasData(
               new Product()
               {
                   Id = 1,
                   CreatedDate = DateTime.Now,
                   OriginalPrice = 100000,
                   Price = 200000,
                   Stock = 0,
                   ViewCount = 0,
               });

            //data table ProductTranslation
            modelBuilder.Entity<ProductTranslation>().HasData(
                 new ProductTranslation()
                 {
                     Id = 1,
                     ProductId = 1,
                     Name = "Áo sơ mi nam trắng Việt Tiến",
                     LanguageId = "vi-VN",
                     SeoAlias = "ao-so-mi-nam-trang-viet-tien",
                     SeoDescription = "Áo sơ mi nam trắng Việt Tiến",
                     SeoTitle = "Áo sơ mi nam trắng Việt Tiến",
                     Details = "Áo sơ mi nam trắng Việt Tiến",
                     Description = "Áo sơ mi nam trắng Việt Tiến"
                 },
                 new ProductTranslation()
                 {
                     Id = 2,
                     ProductId = 1,
                     Name = "Viet Tien Men T-Shirt",
                     LanguageId = "en-US",
                     SeoAlias = "viet-tien-men-t-shirt",
                     SeoDescription = "Viet Tien Men T-Shirt",
                     SeoTitle = "Viet Tien Men T-Shirt",
                     Details = "Viet Tien Men T-Shirt",
                     Description = "Viet Tien Men T-Shirt"
                 });

            //data table ProductInCategory
            modelBuilder.Entity<ProductInCategory>().HasData(
                new ProductInCategory() { ProductId = 1, CategoryId = 1 }
                );

            //data table AppRole
            var roleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
            var adminId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");
            modelBuilder.Entity<AppRole>().HasData(
                new AppRole
                {
                    Id = roleId,
                    Name = "admin",
                    NormalizedName = "admin",
                    Description = "Administrator role"
                });

            //data table AppUser
            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = adminId,
                    UserName = "tamn0310",
                    NormalizedUserName = "tamn0310",
                    Email = "tamn0310@gmail.com",
                    NormalizedEmail = "tamn0310@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Thanhthuy2512@"),
                    SecurityStamp = string.Empty,
                    FirstName = "Nguyễn",
                    LastName = "Tâm",
                    Dob = new DateTime(1998, 10, 03),
                    CreatedAt = DateTime.Now,
                    CreatedBy = "system",
                    UpdatedBy = null
                });

            //data table AppUserRole
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = adminId
            });

            //data table Action
            modelBuilder.Entity<Entities.Action>().HasData(
                new Entities.Action() { Id = 1, Name = ActionName.CanCreate },
                new Entities.Action() { Id = 2, Name = ActionName.CanDelete },
                new Entities.Action() { Id = 3, Name = ActionName.CanRead },
                new Entities.Action() { Id = 4, Name = ActionName.CanUpdate }
                );
        }
    }
}