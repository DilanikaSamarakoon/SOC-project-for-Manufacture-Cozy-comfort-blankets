using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Cozy_Comfort_Distributor.Migrations
{
    /// <inheritdoc />
    public partial class InitialDistributorDbSetup6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Distributors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactPerson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Distributors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DistributorOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistributorId = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributorOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DistributorOrders_Distributors_DistributorId",
                        column: x => x.DistributorId,
                        principalTable: "Distributors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DistributorOrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistributorOrderId = table.Column<int>(type: "int", nullable: false),
                    BlanketId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributorOrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DistributorOrderItems_DistributorOrders_DistributorOrderId",
                        column: x => x.DistributorOrderId,
                        principalTable: "DistributorOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Distributors",
                columns: new[] { "Id", "Address", "ContactPerson", "Email", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, "123 Main St", "Jane Doe", "jane@megamart.com", "Mega Mart", "987-654-3210" },
                    { 2, "456 Oak Ave", "Mike Ross", "mike@urbangoods.net", "Urban Goods", "555-123-4567" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DistributorOrderItems_DistributorOrderId",
                table: "DistributorOrderItems",
                column: "DistributorOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_DistributorOrders_DistributorId",
                table: "DistributorOrders",
                column: "DistributorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DistributorOrderItems");

            migrationBuilder.DropTable(
                name: "DistributorOrders");

            migrationBuilder.DropTable(
                name: "Distributors");
        }
    }
}
