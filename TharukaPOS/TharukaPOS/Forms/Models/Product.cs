using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TharukaPOS.Forms.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Weight { get; set; } // Assuming weight is decimal
        public int Quantity { get; set; } // This might be used for stock quantity
        public decimal BuyingPrice { get; set; }
        public decimal SellingPrice { get; set; }
    }

}
