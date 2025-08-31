using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient; // Assuming SQL Server

namespace TharukaPOS.Forms._Repositories
{
    public class ReportRepository : IReportRepository // Assuming IReportRepository interface exists
    {
        private readonly string _connectionString;

        public ReportRepository(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString), "Connection string cannot be null or empty for ReportRepository.");
            }
            _connectionString = connectionString;
        }

        // --- Implement all report methods here ---

        // FIX 1: Adjusted for Sales table columns (no PaymentMethod, no DiscountAmount, no SaleReturns table)
        public DataTable GetDailySalesSummary(DateTime reportDate)
        {
            DataTable dt = new DataTable();
            string sql = @"
                SELECT
                    CAST(S.SaleDate AS DATE) AS SaleDate,
                    COUNT(DISTINCT S.SaleId) AS NumberOfTransactions,
                    SUM(S.TotalAmount) AS TotalSales -- Using TotalAmount from Sales table for total sales
                FROM Sales S
                WHERE CAST(S.SaleDate AS DATE) = @ReportDate
                GROUP BY CAST(S.SaleDate AS DATE);";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                command.Parameters.Add("@ReportDate", SqlDbType.Date).Value = reportDate.Date;
                connection.Open();
                adapter.Fill(dt);
            }
            return dt;
        }

        // FIX 2: Adjusted for Products table columns (no Categories table, no CategoryName, no ReorderLevel)
        // Using 'quantity' for stock, 'buying_price' for cost price
        public DataTable GetCurrentStockLevels()
        {
            DataTable dt = new DataTable();
            string sql = @"
                SELECT
                    P.product_name AS ProductName,
                    P.product_id AS ProductID, -- Using product_id as an identifier/SKU
                    P.quantity AS CurrentStock,
                    P.buying_price AS CostPrice,
                    P.quantity * P.buying_price AS TotalStockValue
                FROM Products P
                ORDER BY P.product_name;";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                adapter.Fill(dt);
            }
            return dt;
        }

        // You'll need to implement other report methods here,
        // making sure to use your actual table and column names.
        // For example, a method to get Product Profit (based on your initial error):
        public DataTable GetProductProfit(DateTime startDate, DateTime endDate)
        {
            DataTable dt = new DataTable();
            string sql = @"
                SELECT
                    P.product_name AS ProductName,
                    SUM(SD.Quantity) AS TotalQuantitySold,
                    SUM(SD.Quantity * SD.SellingPrice) AS TotalRevenue,
                    SUM(SD.Quantity * SD.BuyingPriceAtSale) AS TotalCostOfGoodsSold,
                    SUM(SD.LineTotal - (SD.Quantity * SD.BuyingPriceAtSale)) AS TotalProfit
                FROM Sales S
                INNER JOIN SaleDetails SD ON S.SaleId = SD.SaleId
                INNER JOIN Products P ON SD.ProductId = P.product_id -- Assuming ProductId in SaleDetails links to product_id in Products
                WHERE CAST(S.SaleDate AS DATE) BETWEEN @StartDate AND @EndDate
                GROUP BY P.product_name
                ORDER BY TotalProfit DESC;";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                command.Parameters.Add("@StartDate", SqlDbType.Date).Value = startDate.Date;
                command.Parameters.Add("@EndDate", SqlDbType.Date).Value = endDate.Date;
                connection.Open();
                adapter.Fill(dt);
            }
            return dt;
        }

        // Example for Customer History (from previous discussion - using DealerId as CustomerId proxy)
        // You mentioned DealerHistoryUserControl, so adapting this for Dealer's sales history
        public DataTable GetDealerHistory(int dealerId) // Renamed from GetCustomerHistory for clarity
        {
            DataTable dt = new DataTable();
            string sql = @"
                SELECT
                    S.SaleDate,
                    SD.LineTotal,
                    SD.Quantity,
                    P.product_name AS ProductName,
                    S.TotalAmount -- Total for the entire sale
                FROM Sales S
                INNER JOIN SaleDetails SD ON S.SaleId = SD.SaleId
                INNER JOIN Products P ON SD.ProductId = P.product_id
                WHERE S.DealerId = @DealerId -- Using DealerId from Sales table
                ORDER BY S.SaleDate DESC;";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                command.Parameters.Add("@DealerId", SqlDbType.Int).Value = dealerId;
                connection.Open();
                adapter.Fill(dt);
            }
            return dt;
        }

        public DataTable GetProductSales(DateTime startDate, DateTime endDate, string productName)
        {
            DataTable dt = new DataTable();
            string sql = @"
                SELECT
                    P.product_name AS ProductName,
                    SUM(SD.Quantity) AS TotalQuantitySold,
                    SUM(SD.Quantity * SD.SellingPrice) AS TotalRevenue,
                    -- TotalCostOfGoodsSold calculation depends on your needs, assuming it's BuyingPriceAtSale
                    SUM(SD.Quantity * SD.BuyingPriceAtSale) AS TotalCostOfGoodsSold,
                    -- TotalProfit: (Total Revenue for line - Total Cost for line)
                    SUM(SD.LineTotal - (SD.Quantity * SD.BuyingPriceAtSale)) AS TotalProfit
                FROM Sales S
                INNER JOIN SaleDetails SD ON S.SaleId = SD.SaleId
                INNER JOIN Products P ON SD.ProductId = P.product_id
                WHERE CAST(S.SaleDate AS DATE) BETWEEN @StartDate AND @EndDate";

            // Add product name filter if provided
            if (!string.IsNullOrWhiteSpace(productName))
            {
                sql += " AND P.product_name LIKE @ProductName";
            }

            sql += " GROUP BY P.product_name ORDER BY TotalRevenue DESC;";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                command.Parameters.Add("@StartDate", SqlDbType.Date).Value = startDate.Date;
                command.Parameters.Add("@EndDate", SqlDbType.Date).Value = endDate.Date;
                if (!string.IsNullOrWhiteSpace(productName))
                {
                    command.Parameters.Add("@ProductName", SqlDbType.NVarChar).Value = "%" + productName + "%";
                }

                connection.Open();
                adapter.Fill(dt);
            }
            return dt;
        }
    }
}
