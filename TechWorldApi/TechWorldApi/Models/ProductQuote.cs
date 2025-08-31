namespace TechWorldApi.Models
{
    public class ProductQuote
    {
        public required string ProductId { get; set; }
        public decimal PricePerUnit { get; set; }
        public int AvailableUnits { get; set; }
        public int EstimatedDeliveryDays { get; set; }
    }
}