using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Crs.Data
{
    public class CrsContext : DbContext, ICrsContext
    {

        public CrsContext(DbContextOptions<CrsContext> options)
           : base(options)
        { }
        public DbSet<UserDb> Users { get; set; }
        public DbSet<RoleDb> Roles { get; set; }
        public DbSet<CarDb> Cars { get; set; }
        public DbSet<UserCarDb> UserCars { get; set; }
        public DbSet<OrderDb> Orders { get; set; }
        public DbSet<NewsDb> News { get; set; }


        #region Required
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDb>().ToTable("Users");
            modelBuilder.Entity<RoleDb>().ToTable("Roles");
            modelBuilder.Entity<CarDb>().ToTable("Cars");
            modelBuilder.Entity<UserCarDb>().ToTable("UserCars");
            modelBuilder.Entity<OrderDb>().ToTable("Orders");
            modelBuilder.Entity<NewsDb>().ToTable("News");

            modelBuilder.Entity<OrderDb>()
               .HasOne(p => p.User)
               .WithMany(b => b.Orders);


            modelBuilder.Entity<UserCarDb>().HasKey(uc => new { uc.UserId, uc.CarId });
            modelBuilder.Entity<UserCarDb>()
                .HasOne<UserDb>(uc => uc.User)
                .WithMany(u => u.UserCars)
                .HasForeignKey(uc => uc.UserId);

            modelBuilder.Entity<UserCarDb>()
               .HasOne<CarDb>(uc => uc.Car)
               .WithMany(u => u.UserCars)
               .HasForeignKey(uc => uc.CarId);

            modelBuilder.Entity<UserDb>()
                .HasOne(p => p.Role)
                .WithMany(b => b.Users);


            modelBuilder.Entity<RoleDb>().HasData(
            new { Id = 1, Role = "Customer" },
            new { Id = 2, Role = "Mechanic" },
            new { Id = 3, Role = "Admin" }
            );

            modelBuilder.Entity<UserDb>().HasData(
            
            new { Id = 13, 
                Username = "Klient123", 
                Password = "AQAAAAEAACcQAAAAEI/95W3RVYvkKzfGU7R5FygehcQY+b2MuiEikNY9ozfOtNxJIWZzZEqClqVLHIhuXw==", // Klient1234
                RoleId = 1, 
                mail = "klient@test", 
                Name = "Klient", 
                Surname = "Testowy"},
             new
             {
                 Id = 14,
                 Username = "Mechanik123",
                 Password = "AQAAAAEAACcQAAAAEOxDc8W6wJqTqknZ9mlMGrU2V8G8L+3kRXuHKLZiOCFVw52hGyyZME7OMKbl8nkadg==",  // Mechanik1234
                 RoleId = 2,
                 mail = "mechanik@test",
                 Name = "Mechanik",
                 Surname = "Testowy"
             },
             new
             {
                 Id = 8,
                 Username = "Admin123",
                 Password = "AQAAAAEAACcQAAAAENJWyPXJfJkc5R/gBF4Q7zBhVpGXl5XrBBylEHWwr7eaCjM9sB7pA7k1NQ4g1aW9mw==", // Admin1234
                 RoleId = 3,
                 mail = "admin@admin",
                 Name = "Admin",
                 Surname = "Admin"
             }
            );
        }
        #endregion
    }

   
}

