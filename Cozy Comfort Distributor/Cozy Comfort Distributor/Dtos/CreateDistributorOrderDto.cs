using System.ComponentModel.DataAnnotations;

namespace Cozy_Comfort_Distributor.Dtos
{
    public class CreateDistributorOrderDto
    {
        [Required]
        public int DistributorId { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        [StringLength(50)]
        public string Status { get; set; } = string.Empty;

        [Required]
        [MinLength(1, ErrorMessage = "Order must contain at least one item.")]
        public ICollection<CreateDistributorOrderItemDto> Items { get; set; } = new List<CreateDistributorOrderItemDto>();
    }

    public class CreateDistributorOrderItemDto
    {
        [Required]
        public int BlanketId { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Unit Price must be greater than zero.")]
        public decimal UnitPrice { get; set; }
    }
}