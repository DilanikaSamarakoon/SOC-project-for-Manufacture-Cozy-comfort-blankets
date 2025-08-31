using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CozyComfort.Seller.API.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<SellerDbContext>
    {
        public SellerDbContext CreateDbContext(string[] args)
        {
            // Build configuration
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Create DbContextOptionsBuilder
            var builder = new DbContextOptionsBuilder<SellerDbContext>();

            // Get connection string
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Configure the DbContext to use SQL Server
            builder.UseSqlServer(connectionString);

            // Return a new instance of SellerDbContext
            return new SellerDbContext(builder.Options);
        }
    }
}