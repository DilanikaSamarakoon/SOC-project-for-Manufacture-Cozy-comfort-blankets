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
    public class DealerRepository : IDealerRepository
    {
        private readonly string connectionString;

        public DealerRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        // Helper method to convert DataReader row to DealerModel object
        private DealerModel ReadDealer(SqlDataReader reader)
        {
            return new DealerModel
            {
                DealerId = Convert.ToInt32(reader["DealerId"]),
                DealerName = reader["DealerName"].ToString(),
                DealerAddress = reader["DealerAddress"].ToString(),
                DealerPhone = reader["DealerPhone"].ToString()
            };
        }

        public void Add(DealerModel dealer)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Include DealerId in the INSERT statement as it's manually entered
                string query = @"INSERT INTO Dealer (DealerId, DealerName, DealerAddress, DealerPhone)
                                 VALUES (@DealerId, @DealerName, @DealerAddress, @DealerPhone)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@DealerId", SqlDbType.Int).Value = dealer.DealerId;
                    command.Parameters.Add("@DealerName", SqlDbType.NVarChar).Value = dealer.DealerName;
                    command.Parameters.Add("@DealerAddress", SqlDbType.NVarChar).Value = dealer.DealerAddress;
                    command.Parameters.Add("@DealerPhone", SqlDbType.NVarChar).Value = dealer.DealerPhone; // Assuming phone as string
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(DealerModel dealer)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Dealer
                                 SET DealerName = @DealerName,
                                     DealerAddress = @DealerAddress,
                                     DealerPhone = @DealerPhone
                                 WHERE DealerId = @DealerId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@DealerName", SqlDbType.NVarChar).Value = dealer.DealerName;
                    command.Parameters.Add("@DealerAddress", SqlDbType.NVarChar).Value = dealer.DealerAddress;
                    command.Parameters.Add("@DealerPhone", SqlDbType.NVarChar).Value = dealer.DealerPhone;
                    command.Parameters.Add("@DealerId", SqlDbType.Int).Value = dealer.DealerId;

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int dealerId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Dealer WHERE DealerId = @DealerId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@DealerId", SqlDbType.Int).Value = dealerId;
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<DealerModel> GetAll()
        {
            List<DealerModel> dealers = new List<DealerModel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT DealerId, DealerName, DealerAddress, DealerPhone FROM Dealer ORDER BY DealerId DESC";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dealers.Add(ReadDealer(reader));
                        }
                    }
                }
            }
            return dealers;
        }

        public IEnumerable<DealerModel> GetByValue(string value)
        {
            List<DealerModel> dealers = new List<DealerModel>();
            int dealerId = 0;
            // Try to parse as int for ID search
            bool isId = int.TryParse(value, out dealerId);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT DealerId, DealerName, DealerAddress, DealerPhone
                                 FROM Dealer
                                 WHERE DealerName LIKE @Value OR DealerAddress LIKE @Value OR DealerPhone LIKE @Value";
                if (isId)
                {
                    query += " OR DealerId = @DealerId";
                }
                query += " ORDER BY DealerId DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@Value", SqlDbType.NVarChar).Value = "%" + value + "%";
                    if (isId)
                    {
                        command.Parameters.Add("@DealerId", SqlDbType.Int).Value = dealerId;
                    }
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dealers.Add(ReadDealer(reader));
                        }
                    }
                }
            }
            return dealers;
        }
    }
}
