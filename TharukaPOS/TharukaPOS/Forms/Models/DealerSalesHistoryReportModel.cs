using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TharukaPOS.Forms.Models
{
    public class DealerSalesHistoryReportModel
    {
        public int SaleId { get; set; }
        public DateTime SaleDate { get; set; }
        public string DealerName { get; set; } // From Dealer table
        public string ProductName { get; set; } // From Products table
        public int QuantitySold { get; set; }
        public decimal SellingPriceAtSale { get; set; } // Price at the time of sale (from SaleDetails)
        public decimal BuyingPriceAtSale { get; set; } // Buying Price at the time of sale (from SaleDetails)
        public decimal LineTotal { get; set; } // QuantitySold * SellingPriceAtSale
        public decimal SaleTotalAmount { get; set; } // Total for the entire sale (from Sales)
        // Removed: SaleDiscount - as per your schema
    }
}