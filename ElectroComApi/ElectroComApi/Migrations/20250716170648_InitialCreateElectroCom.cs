using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ElectroComApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateElectroCom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "BasePrice", "ProductName", "Stock" },
                values: new object[,]
                {
                    { "PROD001", 145.00m, "Smartphone", 80 },
                    { "PROD002", 52.00m, "Wireless Earbuds", 300 },
                    { "PROD003", 1180.00m, "Laptop", 45 },
                    { "PROD007", 90.00m, "Gaming Keyboard", 120 },
                    { "PROD008", 110.00m, "External SSD", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
