// This code goes in your API Project, inside the ApplicationDbContext.cs file.

using CozyComfort.Manufacturer.API.Data; // Ensure this matches your project's models location
using CozyComfort.Manufacturer.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CozyComfort.Manufacturer.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Blanket> Blankets { get; set; }
        public DbSet<Stock> Stocks { get; set; } // This should be plural here
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        // Add other DbSets if you have them...

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // !! ERROR FIX !!
            // This line explicitly tells Entity Framework that the 'Stock' class
            // maps to a database table named "Stock" (singular), fixing the
            // "Invalid object name 'Stocks'" error.
            modelBuilder.Entity<Stock>().ToTable("Stock");

            // This configuration for a unique index on Stock's BlanketId is also important
            modelBuilder.Entity<Stock>()
                .HasIndex(s => s.BlanketId)
                .IsUnique();

            // This fixes the warning about the decimal 'Price' property
            modelBuilder.Entity<Blanket>()
                .Property(b => b.Price)
                .HasColumnType("decimal(18, 2)");
        }
    }
}
