using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using TharukaPOS.Forms.Models;
using System.Data;

namespace TharukaPOS.Forms._Repositories
{
    public class BulkRepository :IBulkRepository 
    {
        private readonly string connectionString;

        public BulkRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        // Helper method to convert DataReader row to BulkProductModel object
        private BulkProductModel ReadBulkProduct(SqlDataReader reader)
        {
            return new BulkProductModel
            {
                BulkProductId = Convert.ToInt32(reader["BulkProductId"]),
                BulkProductName = reader["BulkProductName"].ToString(),
                SupplierId = Convert.ToInt32(reader["SupplierId"]),
                BulkQuantity = Convert.ToInt32(reader["BulkQuantity"]),
                Price = Convert.ToDecimal(reader["Price"])
            };
        }

        public void Add(BulkProductModel bulkProduct)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"INSERT INTO BulkProduct (BulkProductId, BulkProductName, SupplierId, BulkQuantity, Price)
                                        VALUES (@BulkProductId, @BulkProductName, @SupplierId, @BulkQuantity, @Price)";
                command.Parameters.Add("@BulkProductId", SqlDbType.Int).Value = bulkProduct.BulkProductId;
                command.Parameters.Add("@BulkProductName", SqlDbType.NVarChar).Value = bulkProduct.BulkProductName;
                command.Parameters.Add("@SupplierId", SqlDbType.Int).Value = bulkProduct.SupplierId;
                command.Parameters.Add("@BulkQuantity", SqlDbType.Int).Value = bulkProduct.BulkQuantity;
                command.Parameters.Add("@Price", SqlDbType.Decimal).Value = bulkProduct.Price;
                command.ExecuteNonQuery();
            }
        }

        public void Update(BulkProductModel bulkProduct)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"UPDATE BulkProduct
                                        SET BulkProductName = @BulkProductName,
                                            SupplierId = @SupplierId,
                                            BulkQuantity = @BulkQuantity,
                                            Price = @Price
                                        WHERE BulkProductId = @BulkProductId";
                command.Parameters.Add("@BulkProductId", SqlDbType.Int).Value = bulkProduct.BulkProductId;
                command.Parameters.Add("@BulkProductName", SqlDbType.NVarChar).Value = bulkProduct.BulkProductName;
                command.Parameters.Add("@SupplierId", SqlDbType.Int).Value = bulkProduct.SupplierId;
                command.Parameters.Add("@BulkQuantity", SqlDbType.Int).Value = bulkProduct.BulkQuantity;
                command.Parameters.Add("@Price", SqlDbType.Decimal).Value = bulkProduct.Price;
                command.ExecuteNonQuery();
            }
        }

       public void Delete(int bulkProductId)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "DELETE FROM BulkProduct WHERE BulkProductId = @BulkProductId";
                command.Parameters.Add("@BulkProductId", SqlDbType.Int).Value = bulkProductId;
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<BulkProductModel> GetAll()
        {
            var bulkProductList = new List<BulkProductModel>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                // MODIFIED: Join with Supplier table to get SupplierName
                command.CommandText = @"
                    SELECT bp.BulkProductId, bp.BulkProductName, bp.SupplierId, s.SupplierName, bp.BulkQuantity, bp.Price
                    FROM BulkProduct bp
                    INNER JOIN Supplier s ON bp.SupplierId = s.SupplierId
                    ORDER BY bp.BulkProductId ASC"; // Or by BulkProductName

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var bulkProduct = new BulkProductModel
                        {
                            BulkProductId = (int)reader["BulkProductId"],
                            BulkProductName = reader["BulkProductName"].ToString(),
                            SupplierId = (int)reader["SupplierId"],
                            SupplierName = reader["SupplierName"].ToString(), // ADDED: Read SupplierName
                            BulkQuantity = (int)reader["BulkQuantity"],
                            Price = (decimal)reader["Price"]
                        };
                        bulkProductList.Add(bulkProduct);
                    }
                }
            }
            return bulkProductList;
        }

        public IEnumerable<BulkProductModel> GetByValue(string searchValue)
        {
            var bulkProductList = new List<BulkProductModel>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    SELECT bp.BulkProductId, bp.BulkProductName, bp.SupplierId, s.SupplierName, bp.BulkQuantity, bp.Price
                    FROM BulkProduct bp
                    INNER JOIN Supplier s ON bp.SupplierId = s.SupplierId
                    WHERE bp.BulkProductName LIKE @searchValue OR s.SupplierName LIKE @searchValue
                    ORDER BY bp.BulkProductId ASC";
                command.Parameters.Add("@searchValue", SqlDbType.NVarChar).Value = "%" + searchValue + "%";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var bulkProduct = new BulkProductModel
                        {
                            BulkProductId = (int)reader["BulkProductId"],
                            BulkProductName = reader["BulkProductName"].ToString(),
                            SupplierId = (int)reader["SupplierId"],
                            SupplierName = reader["SupplierName"].ToString(),
                            BulkQuantity = (int)reader["BulkQuantity"],
                            Price = (decimal)reader["Price"]
                        };
                        bulkProductList.Add(bulkProduct);
                    }
                }
            }
            return bulkProductList;
        }


        public IEnumerable<BulkProductModel> GetAllBulkProducts()
        {
            List<BulkProductModel> products = new List<BulkProductModel>();
            // Adjust table and column names to match your actual database schema
            string sql = "SELECT BulkProductId, BulkProductName, SupplierId, BulkQuantity, Price FROM [THARUKAPOS].[dbo].[BulkProduct] ORDER BY BulkProductId ASC;";
            

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new BulkProductModel
                        {
                            BulkProductId = Convert.ToInt32(reader["BulkProductId"]),
                            BulkProductName = reader["BulkProductName"].ToString(),
                            SupplierId = Convert.ToInt32(reader["SupplierId"]),
                            BulkQuantity = Convert.ToInt32(reader["BulkQuantity"]),
                            Price = Convert.ToDecimal(reader["Price"]) // Use ToDecimal for price
                        });
                    }
                }
            }
            return products;
        }

        public IEnumerable<BulkProductModel> SearchBulkProducts(string searchTerm)
        {
            List<BulkProductModel> products = new List<BulkProductModel>();
            string sql = "SELECT BulkProductId, BulkProductName, SupplierId, BulkQuantity, Price FROM [THARUKAPOS].[dbo].[BulkProduct] WHERE BulkProductName LIKE @SearchTerm OR BulkProductId = TRY_CAST(@SearchTerm AS INT) ORDER BY BulkProductName;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@SearchTerm", SqlDbType.NVarChar).Value = "%" + searchTerm + "%";
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new BulkProductModel
                        {
                            BulkProductId = Convert.ToInt32(reader["BulkProductId"]),
                            BulkProductName = reader["BulkProductName"].ToString(),
                            SupplierId = Convert.ToInt32(reader["SupplierId"]),
                            BulkQuantity = Convert.ToInt32(reader["BulkQuantity"]),
                            Price = Convert.ToDecimal(reader["Price"])
                        });
                    }
                }
            }
            return products;
        }
    }
}
