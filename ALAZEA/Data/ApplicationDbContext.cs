using ALAZEA.Models;
using ALAZEA.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace ALAZEA.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Admin> Admin { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Plant> Plant { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<OrderPlant> OrderPlant { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define the relationship between User and Order (One-to-Many)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User) // Each Order has one User
                .WithMany(u => u.Orders) // Each User can have many Orders
                .HasForeignKey(o => o.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            // Define the relationship between Review and User (One-to-Many)
            modelBuilder.Entity<Review>()
                .HasOne(r => r.User) // Each Review has one User
                .WithMany(u => u.Reviews) // Each User can have many Reviews
                .HasForeignKey(r => r.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            // Define the relationship between Review and Plant (One-to-Many)
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Plant) // Each Review has one Plant
                .WithMany(p => p.Reviews) // Each Plant can have many Reviews
                .HasForeignKey(r => r.PlantID)
                .OnDelete(DeleteBehavior.Cascade);

            // Define the relationship between Order and OrderPlant (Many-to-Many)
            modelBuilder.Entity<OrderPlant>()
                .HasOne(op => op.Order)
                .WithMany(o => o.OrderPlants)
                .HasForeignKey(op => op.OrderID)
                .OnDelete(DeleteBehavior.Cascade);

            // Define the relationship between Plant and OrderPlant (Many-to-Many)
            modelBuilder.Entity<OrderPlant>()
                .HasOne(op => op.Plant)
                .WithMany(p => p.OrderPlants)
                .HasForeignKey(op => op.PlantID)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    AdminID = new Guid("81b20d3b-eed0-4d41-ae86-b3dcb6cf32ed"),
                    Username = "admin@gmail.com",
                    Password = "1234" // Always encrypt passwords!
                }
            );

            // to Handle the Enum
            modelBuilder.Entity<Plant>().Property(p => p.Category)
               .HasConversion(
                   v => v.ToString(),
                   v => (PlantCategory)Enum.Parse(typeof(PlantCategory), v));

        }

    }
}
