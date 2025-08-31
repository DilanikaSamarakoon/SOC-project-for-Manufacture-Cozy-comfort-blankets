using Microsoft.EntityFrameworkCore;
using Cozy_Comfort_Distributor.Models; // Ensure this matches your project's Models namespace

namespace Cozy_Comfort_Distributor.Data // Ensure this matches your project's Data namespace
{
    public class DistributorDbContext : DbContext
    {
        public DistributorDbContext(DbContextOptions<DistributorDbContext> options) : base(options) { }

        public DbSet<Distributor> Distributors { get; set; }
        public DbSet<DistributorOrder> DistributorOrders { get; set; }
        public DbSet<DistributorOrderItem> DistributorOrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Optional: Seed initial data for Distributors
            modelBuilder.Entity<Distributor>().HasData(
                new Distributor { Id = 1, Name = "Mega Mart", ContactPerson = "Jane Doe", Email = "jane@megamart.com", Phone = "987-654-3210", Address = "123 Main St" },
                new Distributor { Id = 2, Name = "Urban Goods", ContactPerson = "Mike Ross", Email = "mike@urbangoods.net", Phone = "555-123-4567", Address = "456 Oak Ave" }
            );

            // Configure relationships for entities within THIS DbContext
            modelBuilder.Entity<DistributorOrder>()
                .HasOne(doEntity => doEntity.Distributor)
                .WithMany(d => d.DistributorOrders)
                .HasForeignKey(doEntity => doEntity.DistributorId);

            modelBuilder.Entity<DistributorOrderItem>()
                .HasOne(doiEntity => doiEntity.DistributorOrder)
                .WithMany(doEntity => doEntity.Items)
                .HasForeignKey(doiEntity => doiEntity.DistributorOrderId);

            // REMOVED: The relationship to Blanket is removed because Blanket model is not in this project.
            // DistributorOrderItem.BlanketId is just an int here.

            base.OnModelCreating(modelBuilder);
        }
    }
}