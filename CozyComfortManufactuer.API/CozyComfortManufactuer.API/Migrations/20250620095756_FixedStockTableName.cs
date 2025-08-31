using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CozyComfort.Manufacturer.API.Migrations
{
    /// <inheritdoc />
    public partial class FixedStockTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DistributorStocks");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_BlanketId",
                table: "Stock",
                column: "BlanketId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Stock_BlanketId",
                table: "Stock");

            migrationBuilder.CreateTable(
                name: "DistributorStocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlanketId = table.Column<int>(type: "int", nullable: false),
                    BlanketModelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributorStocks", x => x.Id);
                });
        }
    }
}
