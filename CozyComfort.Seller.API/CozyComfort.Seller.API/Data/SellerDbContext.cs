using CozyComfort.Seller.API.Models;
using Microsoft.EntityFrameworkCore;
using Model = CozyComfort.Seller.API.Models;

namespace CozyComfort.Seller.API.Data
{
    public class SellerDbContext : DbContext
    {
        public SellerDbContext(DbContextOptions<SellerDbContext> options) : base(options)
        {
        }

        public DbSet<Model.Seller> Sellers { get; set; }
        public DbSet<Model.SellerOrder> SellerOrders { get; set; }
        public DbSet<Model.SellerOrderItem> SellerOrderItems { get; set; }
        public DbSet<SellerStock> SellerStocks { get; set; }

        // ADD THIS METHOD TO CONFIGURE YOUR DECIMAL PROPERTIES
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Model.SellerOrder>()
                .Property(so => so.TotalAmount)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Model.SellerOrderItem>()
                .Property(soi => soi.UnitPrice)
                .HasColumnType("decimal(18, 2)");
        }
    }
}