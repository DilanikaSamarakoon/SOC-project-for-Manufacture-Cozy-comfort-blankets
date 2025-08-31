using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TharukaPOS.Forms.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        // *** THESE MUST BE STRING ***
        public string Password { get; set; } // This will hold the Base64 HASH from the DB. Renamed for clarity.
        public string Salt { get; set; }     // This will hold the Base64 Salt from the DB
        // *************************
        public int RoleId { get; set; }
        public string RoleName { get; set; }


        


       
    }
}
