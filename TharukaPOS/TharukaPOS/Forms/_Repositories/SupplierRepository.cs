using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using TharukaPOS.Forms.Models;

namespace TharukaPOS.Forms._Repositories
{
    // Make sure this class is public
    public class SupplierRepository : ISupplierRepository
    {
        private readonly string connectionString;

        public SupplierRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        // Helper method to convert DataReader row to SupplierModel object
        private SupplierModel ReadSupplier(SqlDataReader reader)
        {
            return new SupplierModel
            {
                SupplierId = Convert.ToInt32(reader["SupplierId"]),
                SupplierName = reader["SupplierName"].ToString(),
                SupplierAddress = reader["SupplierAddress"].ToString(),
                SupplierPhone = reader["SupplierPhone"].ToString()
            };
        }

        public void Add(SupplierModel supplier)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Include SupplierId in the INSERT statement as it's manually entered
                string query = @"INSERT INTO Supplier (SupplierId, SupplierName, SupplierAddress, SupplierPhone)
                                 VALUES (@SupplierId, @SupplierName, @SupplierAddress, @SupplierPhone)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@SupplierId", SqlDbType.Int).Value = supplier.SupplierId;
                    command.Parameters.Add("@SupplierName", SqlDbType.NVarChar).Value = supplier.SupplierName;
                    command.Parameters.Add("@SupplierAddress", SqlDbType.NVarChar).Value = supplier.SupplierAddress;
                    command.Parameters.Add("@SupplierPhone", SqlDbType.NVarChar).Value = supplier.SupplierPhone; // Assuming phone as string
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(SupplierModel supplier)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Supplier
                                 SET SupplierName = @SupplierName,
                                     SupplierAddress = @SupplierAddress,
                                     SupplierPhone = @SupplierPhone
                                 WHERE SupplierId = @SupplierId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@SupplierName", SqlDbType.NVarChar).Value = supplier.SupplierName;
                    command.Parameters.Add("@SupplierAddress", SqlDbType.NVarChar).Value = supplier.SupplierAddress;
                    command.Parameters.Add("@SupplierPhone", SqlDbType.NVarChar).Value = supplier.SupplierPhone;
                    command.Parameters.Add("@SupplierId", SqlDbType.Int).Value = supplier.SupplierId;

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int supplierId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Supplier WHERE SupplierId = @SupplierId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@SupplierId", SqlDbType.Int).Value = supplierId;
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<SupplierModel> GetAll()
        {
            List<SupplierModel> suppliers = new List<SupplierModel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT SupplierId, SupplierName, SupplierAddress, SupplierPhone FROM Supplier ORDER BY SupplierId DESC";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            suppliers.Add(ReadSupplier(reader));
                        }
                    }
                }
            }
            return suppliers;
        }

        public IEnumerable<SupplierModel> GetByValue(string value)
        {
            List<SupplierModel> suppliers = new List<SupplierModel>();
            int supplierId = 0;
            // Try to parse as int for ID search
            bool isId = int.TryParse(value, out supplierId);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT SupplierId, SupplierName, SupplierAddress, SupplierPhone
                                 FROM Supplier
                                 WHERE SupplierName LIKE @Value OR SupplierAddress LIKE @Value OR SupplierPhone LIKE @Value";
                if (isId)
                {
                    query += " OR SupplierId = @SupplierId";
                }
                query += " ORDER BY SupplierId DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@Value", SqlDbType.NVarChar).Value = "%" + value + "%";
                    if (isId)
                    {
                        command.Parameters.Add("@SupplierId", SqlDbType.Int).Value = supplierId;
                    }
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            suppliers.Add(ReadSupplier(reader));
                        }
                    }
                }
            }
            return suppliers;
        }
    }
}
