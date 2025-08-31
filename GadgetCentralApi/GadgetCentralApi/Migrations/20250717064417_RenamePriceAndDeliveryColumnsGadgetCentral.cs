using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GadgetCentralApi.Migrations
{
    /// <inheritdoc />
    public partial class RenamePriceAndDeliveryColumnsGadgetCentral : Migration
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
                values: new object[] { 4, 0.02m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "PROD002",
                columns: new[] { "DeliveryDays", "DiscountPercentage" },
                values: new object[] { 6, 0.12m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "PROD003",
                columns: new[] { "DeliveryDays", "DiscountPercentage" },
                values: new object[] { 5, 0.01m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "PROD009",
                columns: new[] { "DeliveryDays", "DiscountPercentage" },
                values: new object[] { 3, 0.05m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "PROD010",
                columns: new[] { "DeliveryDays", "DiscountPercentage" },
                values: new object[] { 2, 0.00m });
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
