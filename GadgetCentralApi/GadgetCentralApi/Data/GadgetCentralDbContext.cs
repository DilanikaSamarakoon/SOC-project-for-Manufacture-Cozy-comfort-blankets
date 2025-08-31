using Microsoft.EntityFrameworkCore;
using GadgetCentralApi.Models; // Changed for Gadget Central

namespace GadgetCentralApi.Data
{
    public class GadgetCentralDbContext : DbContext
    {
        public GadgetCentralDbContext(DbContextOptions<GadgetCentralDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        // Seed initial data for Gadget Central
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = "PROD001", ProductName = "Smartphone", Price = 155.00m, Stock = 90, DiscountPercentage = 0.02m, DeliveryDays = 4 }, // Updated to Price and DeliveryDays
                new Product { ProductId = "PROD003", ProductName = "Laptop", Price = 1250.00m, Stock = 60, DiscountPercentage = 0.01m, DeliveryDays = 5 }, // Updated to Price and DeliveryDays
                new Product { ProductId = "PROD009", ProductName = "Gaming Mouse", Price = 60.00m, Stock = 200, DiscountPercentage = 0.05m, DeliveryDays = 3 }, // Updated to Price and DeliveryDays
                new Product { ProductId = "PROD010", ProductName = "Webcam", Price = 40.00m, Stock = 75, DiscountPercentage = 0.00m, DeliveryDays = 2 }, // Updated to Price and DeliveryDays
                new Product { ProductId = "PROD002", ProductName = "Wireless Earbuds", Price = 48.00m, Stock = 0, DiscountPercentage = 0.12m, DeliveryDays = 6 } // Updated to Price and DeliveryDays
            );
        }
    }
}
