using E_Commerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace E_Commerce.Data
{
  public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Nationality> Nationalities { get; set; }

        public DbSet<Settings> Settings { get; set; }

        public DbSet<LoginLog> LoginLogs { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }

        public DbSet<EndCategory> EndCategories { get; set; }

        public DbSet<Item> Items { get; set; }
        public DbSet<ItemPhoto> ItemPhotos { get; set; }
        public DbSet<Color> Colors{ get; set; }
        public DbSet<ItemColor> ItemColors { get; set; }
        public DbSet<Size> Sizes{ get; set; }
        public DbSet<ItemSize> ItemSizes { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Size>().HasData(
                    new Size { id = 1, title = "S", sort = 1, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("9055afc0-351b-4dcd-80cf-11b1fb0a729b") },
                    new Size { id = 2, title = "M", sort = 2, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("9055afc0-351b-4dcd-80cf-11b1fb0a729b") },
                    new Size { id = 3, title = "L", sort = 3, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("9055afc0-351b-4dcd-80cf-11b1fb0a729b") },
                    new Size { id = 4, title = "XL", sort = 4, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("9055afc0-351b-4dcd-80cf-11b1fb0a729b") }
                );

            modelBuilder.Entity<Color>().HasData(
                    new Color { id = 1, title = "Red", bgcolor = "#ff0000", textcolor = "#ffffff", sort = 1, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("9055afc0-351b-4dcd-80cf-11b1fb0a729b") },
                    new Color { id = 2, title = "Black", bgcolor = "#000000",textcolor = "#ffffff", sort = 2, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("9055afc0-351b-4dcd-80cf-11b1fb0a729b") },
                    new Color { id = 3, title = "Blue", bgcolor = "#0000ff",textcolor = "#ffffff", sort = 3, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("9055afc0-351b-4dcd-80cf-11b1fb0a729b") },
                    new Color { id = 4, title = "Yellow", bgcolor = "#ffeb3b",textcolor = "#000000", sort = 4, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("9055afc0-351b-4dcd-80cf-11b1fb0a729b") }
                );

            modelBuilder.Entity<Category>().HasData(
                    new Category { id = 1, title = "Men",  sort = 1, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("9055afc0-351b-4dcd-80cf-11b1fb0a729b") },
                    new Category { id = 2, title = "Women", sort = 2, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("9055afc0-351b-4dcd-80cf-11b1fb0a729b") },
                    new Category { id = 3, title = "Kids", sort = 3, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("9055afc0-351b-4dcd-80cf-11b1fb0a729b") } 
                );
            modelBuilder.Entity<SubCategory>().HasData(
                    new SubCategory { id = 1,category_id=1, title = "Men Accessories", sort = 1, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("9055afc0-351b-4dcd-80cf-11b1fb0a729b") },
                    new SubCategory { id = 2,category_id=1, title = "Shoes", sort = 2, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("9055afc0-351b-4dcd-80cf-11b1fb0a729b") },
                    new SubCategory { id = 3,category_id=2, title = "Women Accessories", sort = 3, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("9055afc0-351b-4dcd-80cf-11b1fb0a729b") },
                    new SubCategory { id = 4,category_id=2, title = "Women Shoes", sort = 2, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("9055afc0-351b-4dcd-80cf-11b1fb0a729b") },
                    new SubCategory { id = 5,category_id=3, title = "Kids Accessories", sort = 3, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("9055afc0-351b-4dcd-80cf-11b1fb0a729b") },
                    new SubCategory { id = 6,category_id = 3, title = "Kids Shoes", sort = 3, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("9055afc0-351b-4dcd-80cf-11b1fb0a729b") }
                );

            modelBuilder.Entity<EndCategory>().HasData(
                    new EndCategory { id = 1, sub_category_id = 1, title = "Watches", sort = 1, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("9055afc0-351b-4dcd-80cf-11b1fb0a729b") },
                    new EndCategory { id = 2, sub_category_id = 1, title = "Suits", sort = 2, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("9055afc0-351b-4dcd-80cf-11b1fb0a729b") },
                    new EndCategory { id = 3, sub_category_id = 2, title = "Slippers", sort = 3, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("9055afc0-351b-4dcd-80cf-11b1fb0a729b") },
                    new EndCategory { id = 4, sub_category_id = 2, title = "Jogers", sort = 2, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("9055afc0-351b-4dcd-80cf-11b1fb0a729b") },
                    new EndCategory { id = 5, sub_category_id = 3, title = "Suits", sort = 3, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("9055afc0-351b-4dcd-80cf-11b1fb0a729b") },
                    new EndCategory { id = 6, sub_category_id = 3, title = "Dopatas", sort = 3, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("9055afc0-351b-4dcd-80cf-11b1fb0a729b") },
                    new EndCategory { id = 7, sub_category_id = 4, title = "Sandals", sort = 2, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("9055afc0-351b-4dcd-80cf-11b1fb0a729b") },
                    new EndCategory { id = 8, sub_category_id = 5, title = "Dipers", sort = 3, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("9055afc0-351b-4dcd-80cf-11b1fb0a729b") },
                    new EndCategory { id = 9, sub_category_id = 6, title = "Caps", sort = 2, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("9055afc0-351b-4dcd-80cf-11b1fb0a729b") },
                    new EndCategory { id = 10, sub_category_id = 6, title = "Suits", sort = 3, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("9055afc0-351b-4dcd-80cf-11b1fb0a729b") },
                    new EndCategory { id = 11, sub_category_id = 6, title = "Towels", sort = 3, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("9055afc0-351b-4dcd-80cf-11b1fb0a729b") }
                );

            modelBuilder.Entity<Item>().HasData(
                    new Item { id = 1, end_category_id = 1, title = "Rolex T20", sort = 1, barcode = "123456789", old_price = 177, price = 99, qty = 123, featured_pic = "1.jpg", is_featured = 1, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("9055afc0-351b-4dcd-80cf-11b1fb0a729b"),hits=1 },
                    new Item { id = 2, end_category_id = 1, title = "Apple Smart watch i16 pro max", sort = 2, barcode = "123456789", old_price = 177, price = 99, qty = 123, featured_pic = "2.jpg", is_featured = 1, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("9055afc0-351b-4dcd-80cf-11b1fb0a729b"),hits=1 },
                    new Item { id = 3, end_category_id = 2, title = "Bannu warai cloths w99", sort = 3, barcode = "123456789", old_price = 177, price = 99, qty = 123, featured_pic = "3.jpg", is_featured = 1, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("9055afc0-351b-4dcd-80cf-11b1fb0a729b"),hits=1 },
                    new Item { id = 4, end_category_id = 3, title = "Lahori Chappal", sort = 1, barcode = "123456789", old_price = 177, price = 99, qty = 123, featured_pic = "1.jpg", is_featured = 1, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("9055afc0-351b-4dcd-80cf-11b1fb0a729b"),hits=1 },
                    new Item { id = 5, end_category_id = 4, title = "Batta Sports T35", sort = 2, barcode = "123456789", old_price = 177, price = 99, qty = 123, featured_pic = "2.jpg", is_featured = 1, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("9055afc0-351b-4dcd-80cf-11b1fb0a729b"),hits=1 },
                    new Item { id = 6, end_category_id = 4, title = "Service plain shoes", sort = 3, barcode = "123456789", old_price = 177, price = 99, qty = 123, featured_pic = "3.jpg", is_featured = 1, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("9055afc0-351b-4dcd-80cf-11b1fb0a729b"),hits=1 }
                );
             

            modelBuilder.Entity<Gender>().HasData(
                    new Gender { id = 1, title = "Male", sort = 1, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("9055afc0-351b-4dcd-80cf-11b1fb0a729b") },
                    new Gender { id = 2, title = "Female", sort = 2, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("9055afc0-351b-4dcd-80cf-11b1fb0a729b") }
                );
            modelBuilder.Entity<Nationality>().HasData(
                    new Gender { id = 1, title = "UAE", sort = 1, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("9055afc0-351b-4dcd-80cf-11b1fb0a729b") },
                    new Gender { id = 2, title = "PAK", sort = 2, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("9055afc0-351b-4dcd-80cf-11b1fb0a729b") }
                );

            modelBuilder.Entity<Settings>().HasData(
                    new Settings { id = 1, title = "app_name", value = "AB ECommerce", sort = 1, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("9055afc0-351b-4dcd-80cf-11b1fb0a729b") }, 
                    new Settings { id = 2, title = "logo", value = "logo.png", sort = 2, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("9055afc0-351b-4dcd-80cf-11b1fb0a729b") },
                    new Settings { id = 3, title = "currency", value = "PKR", sort = 2, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("9055afc0-351b-4dcd-80cf-11b1fb0a729b") },
                    new Settings { id = 4, title = "contact_number", value = "03335662558", sort = 2, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("9055afc0-351b-4dcd-80cf-11b1fb0a729b") },
                    new Settings { id = 5, title = "contact_email", value = "support@codingsips.com", sort = 2, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("9055afc0-351b-4dcd-80cf-11b1fb0a729b") },
                    new Settings { id = 6, title = "max_upload_size_in_mbs", value = "5", sort = 2, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("9055afc0-351b-4dcd-80cf-11b1fb0a729b") },
                    new Settings { id = 7, title = "allowed_upload_extensions", value = ".jpg,.jpeg,.png,.pdf,.docx,.xlsx,.txt", sort = 2, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("9055afc0-351b-4dcd-80cf-11b1fb0a729b") }
                );

            modelBuilder.Entity<IdentityRole>().HasData(
                    new IdentityRole { Id= "12329f10-ef4e-4922-bb05-3e7a3ffdd125", Name = "sa", NormalizedName = "SA" },
                    new IdentityRole { Id= "e32c6f7a-3cf5-4ee0-9d1c-13e4af64d364", Name = "can_view_users", NormalizedName = "CAN_VIEW_USERS" }
                );

            var passwordHasher = new PasswordHasher<ApplicationUser>();
            modelBuilder.Entity<ApplicationUser>().HasData(
                    new ApplicationUser
                    {
                        Id = "9055afc0-351b-4dcd-80cf-11b1fb0a729b",
                        UserName = "alamnaryab@gmail.com",
                        NormalizedUserName = "ALAMNARYAB@GMAIL.COM",
                        Email = "alamnaryab@gmail.com",
                        NormalizedEmail = "ALAMNARYAB@GMAIL.COM",
                        IsAdmin = true,
                        FullName = "Fakhre Alam",
                        address = "UAE",
                        gender_id = 1,
                        dob = DateOnly.Parse("1992-09-29"),
                        nationality_id = 1, 
                        created = DateTime.Parse("2024-09-29 19:45:33"),
                        created_by = 1,
                        PhoneNumber = "+111111111111",
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        PasswordHash = "AQAAAAIAAYagAAAAEAgwzx5s+AhmTjSDN9xXrHZDKNl8YG8xao+5jUGw9tyTc+hDJX/R73bgdgTtRpEqng==",
                        SecurityStamp = "04d6b296-74a4-4b6b-ac6d-3a693cd8ccb1",
                        ConcurrencyStamp = "6ab77101-b4bc-4dc9-9986-cc508e30d30d",
                        dp = "user1.png",
                        status = 1
                    },
                    new ApplicationUser
                    { 
                        Id= "1da7364d-fc6b-4e3e-b290-56727fec2c65",
                        UserName = "alamadcs@gmail.com",
                        NormalizedUserName = "ALAMADCS@GMAIL.COM",
                        Email = "alamadcs@gmail.com",
                        NormalizedEmail = "ALAMADCS@GMAIL.COM",
                        IsAdmin = false,
                        FullName = "Alam",
                        address = "UAE",
                        gender_id = 1,
                        dob = DateOnly.Parse("1991-09-29"),
                        nationality_id = 1, 
                        created = DateTime.Parse("2024-09-29 19:45:33"),
                        created_by = 1,
                        PhoneNumber = "+121111111111",
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        PasswordHash = "AQAAAAIAAYagAAAAEAgwzx5s+AhmTjSDN9xXrHZDKNl8YG8xao+5jUGw9tyTc+hDJX/R73bgdgTtRpEqng==",
                        SecurityStamp = "014b6990-e713-414c-86d4-8467adc71061",
                        ConcurrencyStamp = "276362ce-72e3-4fef-a19e-54ff316295b4",
                        dp = "user2.png",
                        status = 1
                    }
                );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { RoleId = "12329f10-ef4e-4922-bb05-3e7a3ffdd125", UserId = "9055afc0-351b-4dcd-80cf-11b1fb0a729b" },
                new IdentityUserRole<string> { RoleId = "e32c6f7a-3cf5-4ee0-9d1c-13e4af64d364", UserId = "1da7364d-fc6b-4e3e-b290-56727fec2c65" }
            );



        }

    }
}
