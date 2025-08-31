using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechWorldApi.Migrations
{
    /// <inheritdoc />
    public partial class RenamePriceAndDeliveryColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BasePrice",
                table: "Products",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "BaseDeliveryDays",
                table: "Products",
                newName: "DeliveryDays");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Products",
                newName: "BasePrice");

            migrationBuilder.RenameColumn(
                name: "DeliveryDays",
                table: "Products",
                newName: "BaseDeliveryDays");
        }
    }
}
