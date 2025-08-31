using System.ComponentModel.DataAnnotations;

namespace CozyComfort.Seller.API.Models
{
    public class SellerOrderItem
    {
        [Key]
        public int Id { get; set; }
        public int SellerOrderId { get; set; } // Foreign Key to the SellerOrder
        public int ProductId { get; set; } // This ID will correspond to a product from the Distributor/Manufacturer
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}