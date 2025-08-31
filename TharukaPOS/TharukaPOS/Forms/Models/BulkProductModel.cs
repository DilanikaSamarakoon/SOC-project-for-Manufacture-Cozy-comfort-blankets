using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TharukaPOS.Forms.Models
{
    public class BulkProductModel
    {
        public int BulkProductId { get; set; }
        public string BulkProductName { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public int BulkQuantity { get; set; }
        public decimal Price { get; set; }
    }
}
