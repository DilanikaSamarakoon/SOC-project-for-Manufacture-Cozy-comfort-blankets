using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectroComApi.Migrations
{
    /// <inheritdoc />
    public partial class RenamePriceAndDeliveryColumnsElectroCom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BasePrice",
                table: "Products",
                newName: "Price");

            migrationBuilder.AddColumn<int>(
                name: "DeliveryDays",
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
                columns: new[] { "DeliveryDays", "DiscountPercentage" },
                values: new object[] { 2, 0.03m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "PROD002",
                columns: new[] { "DeliveryDays", "DiscountPercentage" },
                values: new object[] { 1, 0.05m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "PROD003",
                columns: new[] { "DeliveryDays", "DiscountPercentage" },
                values: new object[] { 3, 0.00m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "PROD007",
                columns: new[] { "DeliveryDays", "DiscountPercentage" },
                values: new object[] { 2, 0.07m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "PROD008",
                columns: new[] { "DeliveryDays", "DiscountPercentage" },
                values: new object[] { 5, 0.10m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryDays",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DiscountPercentage",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Products",
                newName: "BasePrice");
        }
    }
}
