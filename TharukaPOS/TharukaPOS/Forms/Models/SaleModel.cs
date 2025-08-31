using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TharukaPOS.Forms.Models
{
    public class SaleModel
    {
        public int SaleId { get; set; }
        public int DealerId { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }

        // Collection to hold the details of the sale (products sold)
        public List<SaleDetailModel> SaleDetails { get; set; }

        public SaleModel()
        {
            SaleDetails = new List<SaleDetailModel>();
            SaleDate = DateTime.Now; // Default to current date/time
        }
    }

    public class SaleDetailModel
    {
        public int SaleDetailId { get; set; }
        public int SaleId { get; set; } // Foreign key to Sales
        public int ProductId { get; set; }
        public string ProductName { get; set; } // For display in DataGridView, not saved directly in DB table
        public int Quantity { get; set; }
        public decimal SellingPrice { get; set; }      // Selling Price
        public decimal BuyingPriceAtSale { get; set; } // Buying Price
        public decimal LineTotal { get; set; }
    }
}
