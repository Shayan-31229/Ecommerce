using E_Commerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace E_Commerce.Data
{
  public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Gender> Genders { get; set; }
    public DbSet<Nationality> Nationalities { get; set; }

    public DbSet<Settings> Settings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Gender>().HasData(
                    new Gender { id = 1, title = "Male", sort = 1, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("00000000-0000-0000-0000-000000000001") },
                    new Gender { id = 2, title = "Female", sort = 2, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("00000000-0000-0000-0000-000000000001") }
                );
            modelBuilder.Entity<Nationality>().HasData(
                    new Gender { id = 1, title = "UAE", sort = 1, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("00000000-0000-0000-0000-000000000001") },
                    new Gender { id = 2, title = "PAK", sort = 2, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("00000000-0000-0000-0000-000000000001") }
                );

            modelBuilder.Entity<Settings>().HasData(
                    new Settings { id = 1, title = "app_name", value = "AB ECommerce", sort = 1, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("00000000-0000-0000-0000-000000000001") }, 
                    new Settings { id = 2, title = "logo", value = "logo.png", sort = 2, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("00000000-0000-0000-0000-000000000001") },
                    new Settings { id = 3, title = "currency", value = "PKR", sort = 2, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("00000000-0000-0000-0000-000000000001") },
                    new Settings { id = 4, title = "contact_number", value = "03335662558", sort = 2, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("00000000-0000-0000-0000-000000000001") },
                    new Settings { id = 5, title = "contact_email", value = "support@codingsips.com", sort = 2, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("00000000-0000-0000-0000-000000000001") },
                    new Settings { id = 6, title = "max_upload_size_in_mbs", value = "5", sort = 2, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("00000000-0000-0000-0000-000000000001") },
                    new Settings { id = 7, title = "allowed_upload_extensions", value = ".jpg,.jpeg,.png,.pdf,.docx,.xlsx,.txt", sort = 2, status = 1, created = DateTime.Parse("2024-09-29 19:45:33"), created_by = Guid.Parse("00000000-0000-0000-0000-000000000001") }
                );

            var passwordHasher = new PasswordHasher<ApplicationUser>();
            modelBuilder.Entity<ApplicationUser>().HasData(
                    new ApplicationUser
                    { 
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
                ); ;

        }

    }
}
