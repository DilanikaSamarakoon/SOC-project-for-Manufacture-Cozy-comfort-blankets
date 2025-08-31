using Microsoft.EntityFrameworkCore;
using TechWorldApi.Models; // Import your Product model

namespace TechWorldApi.Data
{
    public class TechWorldDbContext : DbContext
    {
        public TechWorldDbContext(DbContextOptions<TechWorldDbContext> options)
            : base(options)
        {
        }

        // DbSet represents a collection of all entities in the context,
        // or that can be queried from the database.
        public DbSet<Product> Products { get; set; }

        // Optional: Seed initial data when the database is created
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = "PROD001", ProductName = "Smartphone", Price = 150.00m, Stock = 100, DiscountPercentage = 0.05m, DeliveryDays = 3 }, // Changed BasePrice to Price, BaseDeliveryDays to DeliveryDays
                new Product { ProductId = "PROD002", ProductName = "Wireless Earbuds", Price = 50.00m, Stock = 250, DiscountPercentage = 0.10m, DeliveryDays = 2 }, // Changed BasePrice to Price, BaseDeliveryDays to DeliveryDays
                new Product { ProductId = "PROD003", ProductName = "Laptop", Price = 1200.00m, Stock = 50, DiscountPercentage = 0.00m, DeliveryDays = 4 }, // Changed BasePrice to Price, BaseDeliveryDays to DeliveryDays
                new Product { ProductId = "PROD004", ProductName = "USB Cable", Price = 25.00m, Stock = 500, DiscountPercentage = 0.02m, DeliveryDays = 1 }, // Changed BasePrice to Price, BaseDeliveryDays to DeliveryDays
                new Product { ProductId = "PROD005", ProductName = "Gaming Console", Price = 300.00m, Stock = 0, DiscountPercentage = 0.15m, DeliveryDays = 7 }, // Changed BasePrice to Price, BaseDeliveryDays to DeliveryDays
                new Product { ProductId = "PROD006", ProductName = "Smartwatch", Price = 75.00m, Stock = 10, DiscountPercentage = 0.08m, DeliveryDays = 3 } // Changed BasePrice to Price, BaseDeliveryDays to DeliveryDays
            );
        }
    }
}
