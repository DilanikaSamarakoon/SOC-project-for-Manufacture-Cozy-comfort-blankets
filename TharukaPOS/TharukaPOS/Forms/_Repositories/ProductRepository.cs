using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using TharukaPOS.Forms.Models;


namespace TharukaPOS.Forms._Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        //Constructor
        public ProductRepository(string connectionString) 
        { 
            this.connectionString = connectionString;
        }

        //Methods

        // NEW METHOD: Update product stock
        public bool UpdateStock(int productId, int quantityChange)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                // Use a WHERE clause to ensure quantity doesn't go below zero
                // and capture the number of rows affected to verify the update.
                command.CommandText = @"
                    UPDATE Products
                    SET quantity = quantity + @QuantityChange
                    WHERE product_id = @ProductId AND (quantity + @QuantityChange) >= 0;"; // Ensure non-negative quantity

                command.Parameters.Add("@ProductId", SqlDbType.Int).Value = productId;
                command.Parameters.Add("@QuantityChange", SqlDbType.Int).Value = quantityChange; // This will be negative for sales

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0; // Returns true if the update happened (i.e., stock was sufficient)
            }
        }

        // NEW METHOD: Get current stock for a product
        public int GetStock(int productId)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT quantity FROM Products WHERE product_id = @ProductId;";
                command.Parameters.Add("@ProductId", SqlDbType.Int).Value = productId;

                object result = command.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    return Convert.ToInt32(result);
                }
                return 0; // Product not found or quantity is null
            }
        }


        public void Add(ProductModel productModel)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "insert into Products values(@productTitle, @weight, @quantity, @buyingPrice, @sellingPrice)";
                command.Parameters.Add("@productTitle", SqlDbType.NVarChar).Value = productModel.ProductName;
                command.Parameters.Add("@weight", SqlDbType.Int).Value = productModel.Weight;
                command.Parameters.Add("@quantity", SqlDbType.Int).Value = productModel.Quantity;
                
                command.Parameters.Add("@buyingPrice", SqlDbType.Int).Value = productModel.BuyingPrice;
                command.Parameters.Add("@sellingPrice", SqlDbType.Int).Value = productModel.SellingPrice;
                command.ExecuteNonQuery();
            }
            
        }

        public void Delete(int ProductId)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "delete from Products where product_id=@ProductId";
                command.Parameters.Add("@ProductId", SqlDbType.Int).Value = ProductId;
                command.ExecuteNonQuery();
            }
        }

        public void Edit(ProductModel productModel)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"UPDATE Products SET 
                                product_name = @productTitle, 
                                weight = @weight, 
                                quantity = @quantity,
                                
                                buying_price = @buyingPrice, 
                                selling_price = @sellingPrice 
                                WHERE product_id = @ProductId";
                command.Parameters.Add("@ProductId", SqlDbType.Int).Value = productModel.ProductId;
                command.Parameters.Add("@productTitle", SqlDbType.NVarChar).Value = productModel.ProductName;
                command.Parameters.Add("@weight", SqlDbType.Decimal).Value = productModel.Weight;
                command.Parameters.Add("@quantity", SqlDbType.Int).Value = productModel.Quantity;
                
                command.Parameters.Add("@buyingPrice", SqlDbType.Decimal).Value = productModel.BuyingPrice;
                command.Parameters.Add("@sellingPrice", SqlDbType.Decimal).Value = productModel.SellingPrice;
                command.ExecuteNonQuery();
            }

          
        }

        public IEnumerable<ProductModel> GetAll()
        {
            var productList = new List<ProductModel>();
            
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM Products ORDER BY Product_Id DESC";


                using (var reader = command.ExecuteReader()) 
                {
                    while (reader.Read())
                    {
                        var productModel = new ProductModel();
                        productModel.ProductId = (int)reader[0];
                        productModel.ProductName = reader[1].ToString();
                        productModel.Weight = (decimal)reader[2];
                        productModel.Quantity = (int)reader[3];
                       
                        productModel.BuyingPrice = (decimal)reader[4];
                        productModel.SellingPrice = (decimal)reader[5];
                        productList.Add(productModel);
                    }

                }
            }
            return productList;
        }




        public IEnumerable<ProductModel> GetByValue(string value)
        {
            var productList = new List<ProductModel>();
            int productId = int.TryParse(value, out _) ? Convert.ToInt32(value) : 0;
            string productName = value;
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"Select *from Products where product_id=@ProductId or product_name like @ProductName+'%' order by Product_Id desc";
                command.Parameters.Add("@productId", SqlDbType.Int).Value = productId;
                command.Parameters.Add("@productName", SqlDbType.NVarChar).Value = productName;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var productModel = new ProductModel();
                        productModel.ProductId = (int)reader[0];
                        productModel.ProductName = reader[1].ToString();
                        productModel.Weight = (decimal)reader[2];
                        productModel.Quantity = (int)reader[3];
                       
                        productModel.BuyingPrice = (decimal)reader[4];
                        productModel.SellingPrice = (decimal)reader[5];
                        productList.Add(productModel);
                    }

                }
            }
            return productList;
        }

        public IEnumerable<ProductModel> GetLowStockProducts(int threshold)
        {
            List<ProductModel> products = new List<ProductModel>();
            // IMPORTANT: Adjust 'Products' table name and column names to match your actual database schema
            // (e.g., 'product_id', 'product_name', 'buying_price', etc., if your actual DB uses underscores)
            string sql = "SELECT product_id, product_name, weight, quantity, buying_price, selling_price FROM [THARUKAPOS].[dbo].[Products] WHERE quantity <= @Threshold ORDER BY product_name ASC;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@Threshold", SqlDbType.Int).Value = threshold;
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new ProductModel
                        {
                            ProductId = Convert.ToInt32(reader["product_id"]),
                            ProductName = reader["product_name"].ToString(),
                            Weight = Convert.ToDecimal(reader["weight"]),
                            Quantity = Convert.ToInt32(reader["quantity"]),
                            BuyingPrice = Convert.ToDecimal(reader["buying_price"]),
                            SellingPrice = Convert.ToDecimal(reader["selling_price"])
                        });
                    }
                }
            }
            return products;
        }

    }
    
}
