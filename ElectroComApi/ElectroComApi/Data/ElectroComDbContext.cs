using Microsoft.EntityFrameworkCore;
using ElectroComApi.Models; // Import your Product model

namespace ElectroComApi.Data
{
    public class ElectroComDbContext : DbContext
    {
        public ElectroComDbContext(DbContextOptions<ElectroComDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        // Seed initial data for ElectroCom
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = "PROD001", ProductName = "Smartphone", Price = 145.00m, Stock = 80, DiscountPercentage = 0.03m, DeliveryDays = 2 }, // Updated to Price and DeliveryDays
                new Product { ProductId = "PROD002", ProductName = "Wireless Earbuds", Price = 52.00m, Stock = 300, DiscountPercentage = 0.05m, DeliveryDays = 1 }, // Updated to Price and DeliveryDays
                new Product { ProductId = "PROD003", ProductName = "Laptop", Price = 1180.00m, Stock = 45, DiscountPercentage = 0.00m, DeliveryDays = 3 }, // Updated to Price and DeliveryDays
                new Product { ProductId = "PROD007", ProductName = "Gaming Keyboard", Price = 90.00m, Stock = 120, DiscountPercentage = 0.07m, DeliveryDays = 2 }, // Updated to Price and DeliveryDays
                new Product { ProductId = "PROD008", ProductName = "External SSD", Price = 110.00m, Stock = 0, DiscountPercentage = 0.10m, DeliveryDays = 5 } // Updated to Price and DeliveryDays
            );
        }
    }
}
