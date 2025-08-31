using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace TharukaPOS.Forms._Repositories
{

    
    
        public class DashboardRepository // If you prefer, rename this to DashboardRepository
        {
        public string ConnectionString => connectionString;
        public string connectionString { get; private set; } // Capitalize for property naming convention


        // Change the constructor to accept the connection string
        public DashboardRepository(string connString) // Add this parameter
        {
            connectionString = connString; // Assign it to the field

            GrossRevenueList = new List<dynamic>();
            TopProductsList = new List<dynamic>();
            UnderstockList = new DataTable();
        }

        // Remove the parameterless constructor if it exists, or just keep this one
        // public DataAccess() { /* ... */ } // REMOVE THIS IF YOU ONLY WANT TO PASS IT IN

        // Properties to hold the fetched data
        public int NumOrders { get; private set; }
            public decimal TotalRevenue { get; private set; }
            public decimal TotalProfit { get; private set; }
            public int NumCustomers { get; private set; }
            public int NumSuppliers { get; private set; }
            public int NumProducts { get; private set; }

            // Lists for charts (you'll need to define these structures)
            // For simplicity, let's use List<dynamic> for now, but custom classes are better
            public List<dynamic> GrossRevenueList { get; private set; }
            public List<dynamic> TopProductsList { get; private set; }
            public DataTable UnderstockList { get; private set; } // DataTable is good for DataGridView

            public DashboardRepository() // Constructor
            {
                connectionString = ConfigurationManager.ConnectionStrings["THARUKAPOS"].ConnectionString;
                // Initialize lists to avoid NullReferenceExceptions
                GrossRevenueList = new List<dynamic>();
                TopProductsList = new List<dynamic>();
                UnderstockList = new DataTable();
            }

            // This method will now fetch ALL the dashboard data and populate the properties
            public bool LoadData(DateTime startDate, DateTime endDate) // Renamed from refreshData for clarity
            {
                // For simplicity, I'm re-calling the previous methods here.
                // In a real scenario, you might have one SQL query that fetches multiple aggregate values
                // or optimized queries for charts.

                try
                {
                    // Update simple counters
                    NumOrders = GetNumberOfSales(startDate, endDate); // Pass dates
                    TotalRevenue = GetTotalRevenue(startDate, endDate); // Pass dates
                    TotalProfit = GetTotalProfit(startDate, endDate); // Pass dates

                    NumCustomers = GetTotalCustomers(); // These don't depend on date range
                    NumSuppliers = GetTotalSuppliers();
                    NumProducts = GetTotalProducts();

                    // Populate chart data (dummy data for now, you'll implement the SQL later)
                    GrossRevenueList = GetGrossRevenueData(startDate, endDate);
                    TopProductsList = GetTopProductsData(startDate, endDate);
                    UnderstockList = GetUnderstockData(); // Assuming understock doesn't change with date range

                    return true; // Data loaded successfully
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Error loading dashboard data: " + ex.Message);
                    // Log the full exception details here
                    return false; // Data load failed
                }
            }

            // --- UPDATED METHODS (now accepting date ranges for relevant ones) ---

            public int GetNumberOfSales(DateTime startDate, DateTime endDate)
            {
                int numberOfOrders = 0;
                string query = "SELECT COUNT(SaleId) FROM Sales WHERE SaleDate BETWEEN @startDate AND @endDate";
            using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@startDate", SqlDbType.DateTime).Value = startDate;
                        command.Parameters.Add("@endDate", SqlDbType.DateTime).Value = endDate;
                        try
                        {
                            connection.Open();
                            object result = command.ExecuteScalar();
                            if (result != DBNull.Value) numberOfOrders = Convert.ToInt32(result);
                        }
                        catch (SqlException ex) { System.Windows.Forms.MessageBox.Show("Error getting number of orders: " + ex.Message); }
                    }
                }
                return numberOfOrders;
            }

            public decimal GetTotalRevenue(DateTime startDate, DateTime endDate)
            {
                decimal totalRevenue = 0;
                string query = "SELECT ISNULL(SUM(TotalAmount), 0) FROM Sales WHERE SaleDate BETWEEN @startDate AND @endDate";
            using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@startDate", SqlDbType.DateTime).Value = startDate;
                        command.Parameters.Add("@endDate", SqlDbType.DateTime).Value = endDate;
                        try
                        {
                            connection.Open();
                            object result = command.ExecuteScalar();
                            if (result != DBNull.Value) totalRevenue = Convert.ToDecimal(result);
                        }
                        catch (SqlException ex) { System.Windows.Forms.MessageBox.Show("Error getting total revenue: " + ex.Message); }
                    }
                }
                return totalRevenue;
            }

            public decimal GetTotalProfit(DateTime startDate, DateTime endDate)
            {
                decimal totalProfit = 0;
            string query = @"
                SELECT ISNULL(SUM(SD.Quantity * (SD.SellingPrice - SD.BuyingPriceAtSale)), 0) AS TotalProfit
                FROM SaleDetails SD
                INNER JOIN Sales S ON SD.SaleId = S.SaleId
                WHERE S.SaleDate BETWEEN @startDate AND @endDate";
            using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@startDate", SqlDbType.DateTime).Value = startDate;
                        command.Parameters.Add("@endDate", SqlDbType.DateTime).Value = endDate;
                        try
                        {
                            connection.Open();
                            object result = command.ExecuteScalar();
                            if (result != DBNull.Value) totalProfit = Convert.ToDecimal(result);
                        }
                        catch (SqlException ex) { System.Windows.Forms.MessageBox.Show("Error getting total profit: " + ex.Message); }
                    }
                }
                return totalProfit;
            }

        // These don't change based on date range, so keep them as is
        public int GetTotalCustomers()
        {
            int totalCustomers = 0;
            string query = "SELECT COUNT(DealerId) FROM Dealer"; // Assuming 'Dealer' table for customers
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            totalCustomers = Convert.ToInt32(result);
                        }
                    }
                    catch (SqlException ex)
                    {
                        System.Windows.Forms.MessageBox.Show("Error getting total customers: " + ex.Message);
                        // Log the full exception details here for debugging
                    }
                }
            }
            return totalCustomers;
        }

        public int GetTotalSuppliers()
        {
            int totalSuppliers = 0;
            string query = "SELECT COUNT(SupplierId) FROM Supplier"; // Assuming 'Supplier' table for suppliers
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            totalSuppliers = Convert.ToInt32(result);
                        }
                    }
                    catch (SqlException ex)
                    {
                        System.Windows.Forms.MessageBox.Show("Error getting total suppliers: " + ex.Message);
                        // Log the full exception details here for debugging
                    }
                }
            }
            return totalSuppliers;
        }

        public int GetTotalProducts()
        {
            int totalProducts = 0;
            string query = "SELECT COUNT(product_id) FROM Products"; // Assuming 'Products' table for products
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            totalProducts = Convert.ToInt32(result);
                        }
                    }
                    catch (SqlException ex)
                    {
                        System.Windows.Forms.MessageBox.Show("Error getting total products: " + ex.Message);
                        // Log the full exception details here for debugging
                    }
                }
            }
            return totalProducts;
        }

        // --- Methods for chart data (example structure) ---
        private List<dynamic> GetGrossRevenueData(DateTime startDate, DateTime endDate)
            {
                List<dynamic> list = new List<dynamic>();
               
            string query = @"
                SELECT CAST(SaleDate AS DATE) AS SaleDay, ISNULL(SUM(TotalAmount), 0) AS DailyRevenue
                FROM Sales
                WHERE SaleDate BETWEEN @startDate AND @endDate
                GROUP BY CAST(SaleDate AS DATE)
                ORDER BY SaleDay ASC";

            using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@startDate", SqlDbType.DateTime).Value = startDate;
                        command.Parameters.Add("@endDate", SqlDbType.DateTime).Value = endDate;
                        try
                        {
                            connection.Open();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    list.Add(new { Date = reader["SaleDay"], TotalAmount = reader["DailyRevenue"] });
                                }
                            }
                        }
                        catch (SqlException ex) { System.Windows.Forms.MessageBox.Show("Error getting gross revenue data: " + ex.Message); }
                    }
                }
                return list;
            }

            private List<dynamic> GetTopProductsData(DateTime startDate, DateTime endDate)
            {
                List<dynamic> list = new List<dynamic>();
                
            string query = @"
                SELECT TOP 5 P.product_name AS ProductName, ISNULL(SUM(SD.Quantity), 0) AS TotalQuantitySold
                FROM SaleDetails SD
                INNER JOIN Products P ON SD.ProductId = P.product_id
                INNER JOIN Sales S ON SD.SaleId = S.SaleId
                WHERE S.SaleDate BETWEEN @startDate AND @endDate
                GROUP BY P.product_name
                ORDER BY TotalQuantitySold DESC";

            using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@startDate", SqlDbType.DateTime).Value = startDate;
                        command.Parameters.Add("@endDate", SqlDbType.DateTime).Value = endDate;
                        try
                        {
                            connection.Open();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    // Map to "Key" and "Value" as your chart expects
                                    list.Add(new { Key = reader["ProductName"], Value = reader["TotalQuantitySold"] });
                                }
                            }
                        }
                        catch (SqlException ex) { System.Windows.Forms.MessageBox.Show("Error getting top products data: " + ex.Message); }
                    }
                }
                return list;
            }

            private DataTable GetUnderstockData()
            {
                DataTable dt = new DataTable();
                // Define your understock threshold (e.g., quantity < 15)
                string query = "SELECT product_name, quantity FROM Products WHERE quantity < 50 ORDER BY quantity ASC";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        try
                        {
                            connection.Open();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                dt.Load(reader);
                            }
                        }
                        catch (SqlException ex) { System.Windows.Forms.MessageBox.Show("Error getting understock data: " + ex.Message); }
                    }
                }
                return dt;
            }


        }
    }

