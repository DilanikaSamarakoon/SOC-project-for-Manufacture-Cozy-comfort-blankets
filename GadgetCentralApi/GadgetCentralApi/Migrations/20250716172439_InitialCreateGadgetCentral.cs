using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GadgetCentralApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateGadgetCentral : Migration
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
                    { "PROD001", 155.00m, "Smartphone", 90 },
                    { "PROD002", 48.00m, "Wireless Earbuds", 0 },
                    { "PROD003", 1250.00m, "Laptop", 60 },
                    { "PROD009", 60.00m, "Gaming Mouse", 200 },
                    { "PROD010", 40.00m, "Webcam", 75 }
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
