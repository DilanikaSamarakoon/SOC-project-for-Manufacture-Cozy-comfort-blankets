using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechWorldApi.Migrations
{
    /// <inheritdoc />
    public partial class AddDiscountAndDeliveryToProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BaseDeliveryDays",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPercentage",
                table: "Products",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "PROD001",
                columns: new[] { "BaseDeliveryDays", "DiscountPercentage" },
                values: new object[] { 3, 0.05m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "PROD002",
                columns: new[] { "BaseDeliveryDays", "DiscountPercentage" },
                values: new object[] { 2, 0.10m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "PROD003",
                columns: new[] { "BaseDeliveryDays", "DiscountPercentage" },
                values: new object[] { 4, 0.00m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "PROD004",
                columns: new[] { "BaseDeliveryDays", "DiscountPercentage" },
                values: new object[] { 1, 0.02m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "PROD005",
                columns: new[] { "BaseDeliveryDays", "DiscountPercentage" },
                values: new object[] { 7, 0.15m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "PROD006",
                columns: new[] { "BaseDeliveryDays", "DiscountPercentage" },
                values: new object[] { 3, 0.08m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseDeliveryDays",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DiscountPercentage",
                table: "Products");
        }
    }
}
