using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TharukaPOS.Forms.Models; // Reference your UserModel

namespace TharukaPOS.Forms.Common
{
    public static class CurrentUser // <--- Notice the 'static' keyword here
    {
        // Properties for user authentication status and details
        public static bool IsLoggedIn { get; private set; }
        public static string Username { get; private set; } // Add this property
        public static int RoleId { get; private set; }       // Add this property
        public static string RoleName { get; private set; }  // Add this property
        public static bool IsAdmin { get; private set; }
        public static bool IsRegularUser { get; private set; }
        // Add other roles as needed, e.g., public static bool IsManager { get; private set; }

        // Static method to set user details upon successful login
        public static void SetUser(string username, int roleId, string roleName)
        {
            IsLoggedIn = true;
            Username = username;
            RoleId = roleId;
            RoleName = roleName;

            // Determine specific role flags based on RoleName
            IsAdmin = roleName.Equals("Admin", StringComparison.OrdinalIgnoreCase);
            IsRegularUser = roleName.Equals("User", StringComparison.OrdinalIgnoreCase);
            // Add checks for other roles here if you have them
        }

        // Static method to clear user details upon logout
        public static void Logout()
        {
            IsLoggedIn = false;
            Username = null;
            RoleId = 0; // Reset RoleId
            RoleName = null;
            IsAdmin = false;
            IsRegularUser = false;
        }


    }
}