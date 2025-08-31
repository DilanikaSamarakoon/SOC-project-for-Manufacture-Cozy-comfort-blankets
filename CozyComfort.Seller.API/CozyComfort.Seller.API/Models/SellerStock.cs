using System.ComponentModel.DataAnnotations;

namespace CozyComfort.Seller.API.Models
{
    public class SellerStock
    {
        [Key]
        public int Id { get; set; }

        public int SellerId { get; set; } // Foreign key to the Seller

        public int ProductId { get; set; }

        public int QuantityInStock { get; set; }
    }
}