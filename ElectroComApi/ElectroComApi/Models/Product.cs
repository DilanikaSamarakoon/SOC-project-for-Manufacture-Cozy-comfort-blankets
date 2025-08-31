using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectroComApi.Models
{
    [Table("Products")] // Maps this class to a table named "Products" in the database
    public class Product
    {
        [Key]
        public required string ProductId { get; set; }

        [Required]
        public required string ProductName { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; } // Changed from BasePrice to Price

        public int Stock { get; set; }

        [Column(TypeName = "decimal(5, 2)")] // Represents a percentage, e.g., 0.10 for 10%
        public decimal DiscountPercentage { get; set; } = 0.00m; // Added for consistency

        public int DeliveryDays { get; set; } = 2; // Changed from BaseDeliveryDays to DeliveryDays
    }
}
