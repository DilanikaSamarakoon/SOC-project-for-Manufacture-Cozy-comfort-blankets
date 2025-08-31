using System.ComponentModel.DataAnnotations;

namespace CozyComfort.Seller.API.Models
{
    public class SellerOrder
    {
        [Key]
        public int Id { get; set; }
        public int SellerId { get; set; } // Foreign Key to the Seller
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } // e.g., "Placed", "Shipped", "Completed"
        public decimal TotalAmount { get; set; }
        public ICollection<SellerOrderItem> OrderItems { get; set; } = new List<SellerOrderItem>();
    }
}