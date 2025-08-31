// TharukaPOS\Forms\_Repositories\UserRepository.cs

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
// using System.Threading.Tasks; // Not needed here
using TharukaPOS.Forms.Models;


namespace TharukaPOS.Forms._Repositories
{
    // Make sure IUserRepository defines ALL public methods that UserRepository implements
    public interface IUserRepository
    {
        UserModel GetUserByUsername(string username);
        void AddUser(UserModel user, string plainPassword);
        void UpdateUser(UserModel user, string plainPassword = null);
        void DeleteUser(int userId);
        List<UserModel> GetAllUsers();
        UserModel GetUserById(int userId);
        UserModel ValidateUser(string username, string plainPassword); // Add this
        Dictionary<int, string> GetAllRoles(); // Add this
        int GetAdminCount(); // Add this
    }

    public class UserRepository : IUserRepository
    {
        private readonly string connectionString; // Correct field declaration

        public UserRepository(string connectionString)
        {

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString), "Connection string cannot be null or empty when initializing UserRepository.");
            }
            this.connectionString = connectionString; // <--- FIX: Assign to the field using 'this.'
        }

        // --- Password Hashing Helper Methods (KEEP THESE, THEY ARE CRUCIAL) ---
        private static byte[] GenerateSalt()
        {
            byte[] salt = new byte[16]; // 128-bit salt
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        private static string HashPassword(string password, byte[] salt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000)) // 10000 iterations recommended
            {
                byte[] hash = pbkdf2.GetBytes(20); // 160-bit hash
                return Convert.ToBase64String(hash);
            }
        }
        // --- End Password Hashing Helper Methods ---


        // --- CRUD Operations ---

        public void AddUser(UserModel user, string plainPassword)
        {
            if (string.IsNullOrWhiteSpace(plainPassword))
                throw new ArgumentException("Password cannot be empty.", nameof(plainPassword));

            byte[] saltBytes = GenerateSalt();
            string saltString = Convert.ToBase64String(saltBytes); // Convert byte[] to Base64 string for DB
            string passwordHash = HashPassword(plainPassword, saltBytes); // HashPassword returns Base64 string

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("INSERT INTO Users (username, Password, RoleId, Salt) VALUES (@Username, @PasswordHash, @RoleId, @Salt)", connection))
            {
                command.Parameters.Add("@Username", SqlDbType.NVarChar).Value = user.Username;
                command.Parameters.Add("@PasswordHash", SqlDbType.NVarChar).Value = passwordHash; // Assign string to NVARCHAR parameter
                command.Parameters.Add("@RoleId", SqlDbType.Int).Value = user.RoleId;
                command.Parameters.Add("@Salt", SqlDbType.NVarChar).Value = saltString; // Assign string to NVARCHAR parameter
                connection.Open();
                command.ExecuteNonQuery();
            }
        }


        public void UpdateUser(UserModel user, string plainPassword = null)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string sql = "UPDATE Users SET username = @Username, RoleId = @RoleId";
                byte[] saltBytes = null; // Declare outside if block

                if (!string.IsNullOrEmpty(plainPassword))
                {
                    saltBytes = GenerateSalt(); // Generate new salt once
                    string saltString = Convert.ToBase64String(saltBytes);
                    string passwordHash = HashPassword(plainPassword, saltBytes); // Use the same saltBytes

                    // FIX: Use "password" and "Salt" to match your DB columns
                    sql += ", password = @PasswordHash, Salt = @Salt";
                }
                sql += " WHERE UserId = @UserId";

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@Username", SqlDbType.NVarChar).Value = user.Username;
                    command.Parameters.Add("@RoleId", SqlDbType.Int).Value = user.RoleId;
                    command.Parameters.Add("@UserId", SqlDbType.Int).Value = user.UserId;

                    if (!string.IsNullOrEmpty(plainPassword))
                    {
                        // Use the saltBytes generated above
                        command.Parameters.Add("@PasswordHash", SqlDbType.NVarChar).Value = HashPassword(plainPassword, saltBytes);
                        command.Parameters.Add("@Salt", SqlDbType.NVarChar).Value = Convert.ToBase64String(saltBytes);
                    }
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

     

        public void DeleteUser(int userId)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("DELETE FROM Users WHERE UserId = @UserId", connection))
            {
                command.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public List<UserModel> GetAllUsers()
        {
            var users = new List<UserModel>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("SELECT u.UserId, u.username, ur.RoleName, u.RoleId FROM Users u JOIN UserRoles ur ON u.RoleId = ur.RoleId", connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new UserModel
                        {
                            UserId = Convert.ToInt32(reader["UserId"]),
                            Username = reader["username"].ToString(),
                            RoleId = Convert.ToInt32(reader["RoleId"]),
                            RoleName = reader["RoleName"].ToString()
                        });
                    }
                }
            }
            return users;
        }

        public UserModel GetUserById(int userId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                // Select 'Password' and 'Salt' which are your actual DB column names
                using (var command = new SqlCommand("SELECT u.UserId, u.username, u.Password, u.Salt, u.RoleId, ur.RoleName FROM Users u JOIN UserRoles ur ON u.RoleId = ur.RoleId WHERE u.UserId = @UserId", connection))
                {
                    command.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new UserModel
                            {
                                UserId = Convert.ToInt32(reader["UserId"]),
                                Username = reader["username"].ToString(),
                                Password = reader["Password"].ToString(), // Assign string to string
                                Salt = reader["Salt"].ToString(),         // Assign string to string
                                RoleId = Convert.ToInt32(reader["RoleId"]),
                                RoleName = reader["RoleName"].ToString()
                            };
                        }
                        return null;
                    }
                }
            }
        }


        // --- FIX: This is the correct login validation method ---
        public UserModel ValidateUser(string username, string plainPassword)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                
                using (var command = new SqlCommand("SELECT u.UserId, u.username, u.Password, u.Salt, ur.RoleName, u.RoleId FROM Users u JOIN UserRoles ur ON u.RoleId = ur.RoleId WHERE u.username = @Username", connection))
                {
                    command.Parameters.Add("@Username", SqlDbType.NVarChar).Value = username;
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedHash = reader["Password"].ToString(); 
                            string storedSalt = reader["Salt"].ToString();    

                            
                            byte[] saltBytes = Convert.FromBase64String(storedSalt);

                            string enteredPasswordHash = HashPassword(plainPassword, saltBytes);

                            if (enteredPasswordHash == storedHash)
                            {
                                return new UserModel
                                {
                                    UserId = Convert.ToInt32(reader["UserId"]),
                                    Username = reader["username"].ToString(),
                                    Password = storedHash, 
                                    Salt = storedSalt,     
                                    RoleId = Convert.ToInt32(reader["RoleId"]),
                                    RoleName = reader["RoleName"].ToString()
                                };
                            }
                        }
                        return null; 
                    }
                }
            }
        }

        public Dictionary<int, string> GetAllRoles()
        {
            var roles = new Dictionary<int, string>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("SELECT RoleId, RoleName FROM UserRoles", connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        roles.Add(Convert.ToInt32(reader["RoleId"]), reader["RoleName"].ToString());
                    }
                }
            }
            return roles;
        }

        public int GetAdminCount()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                int adminRoleId;
                using (var cmd = new SqlCommand("SELECT RoleId FROM UserRoles WHERE RoleName = 'Admin'", connection))
                {
                    connection.Open();
                    object result = cmd.ExecuteScalar();
                    if (result == null) return 0;
                    adminRoleId = Convert.ToInt32(result);
                }
                connection.Close();

                using (var command = new SqlCommand("SELECT COUNT(*) FROM Users WHERE RoleId = @AdminRoleId", connection))
                {
                    command.Parameters.Add("@AdminRoleId", SqlDbType.Int).Value = adminRoleId;
                    connection.Open();
                    return (int)command.ExecuteScalar();
                }
            }
        }

        public UserModel GetUserByUsername(string username) // Your existing method, now consistent with UserModel
        {
            UserModel user = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Ensure your database has a 'UserId' column if you plan to use it
                // If not, remove 'UserId' from SELECT and the UserModel property.
                // NOTE: This method does NOT do password validation. Use ValidateUser for that.
                string query = "SELECT UserId, username, password, RoleId, Salt FROM Users WHERE Username = @Username"; // Include Salt if needed for other ops
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new UserModel
                            {
                                UserId = Convert.ToInt32(reader["UserId"]),
                                Username = reader["username"].ToString(),
                                Password = reader["password"].ToString(), // This is the HASHED password from DB
                                RoleId = Convert.ToInt32(reader["RoleId"])
                                // RoleName is not selected here, so it will be null unless you join.
                                // For basic GetUserByUsername, this might be fine.
                            };
                        }
                    }
                }
            }
            return user;
        }

        public UserModel AuthenticateUser(string username, string plainPassword)
        {
            UserModel user = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // FIX: Change u.PasswordHash to u.Password to match your DB column name
                string query = @"
                    SELECT u.UserId, u.Username, u.Password, u.Salt, u.RoleId, r.RoleName
                    FROM Users u
                    JOIN UserRoles r ON u.RoleId = r.RoleId
                    WHERE u.Username = @Username";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Correctly read Password and Salt as strings from the DB
                            // Your DB columns are named 'Password' and 'Salt'
                            string storedPasswordBase64 = reader["Password"].ToString(); // Use "Password"
                            string storedSaltBase64 = reader["Salt"].ToString();

                            // Convert Base64 strings to byte arrays for hashing comparison
                            byte[] storedSaltBytes = Convert.FromBase64String(storedSaltBase64);

                            // Hash the plain password with the *retrieved* salt
                            string enteredPasswordHash = HashPassword(plainPassword, storedSaltBytes);

                            // Compare the newly hashed password with the stored hash
                            if (enteredPasswordHash == storedPasswordBase64)
                            {
                                user = new UserModel
                                {
                                    UserId = (int)reader["UserId"],
                                    Username = reader["Username"].ToString(),
                                    // Only assign relevant data after successful authentication
                                    RoleId = (int)reader["RoleId"],
                                    RoleName = reader["RoleName"].ToString()
                                };
                            }
                            // If passwords don't match, user remains null
                        }
                    }
                }
            }
            return user; // Will be null if authentication fails
        }
    }
}