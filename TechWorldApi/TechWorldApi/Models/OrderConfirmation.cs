namespace TechWorldApi.Models
{
    // Represents an order confirmation from a distributor back to The GadgetHub
    public class OrderConfirmation
    {
        public required string GadgetHubOrderId { get; set; } // The original order ID from GadgetHub
        public required string ProductId { get; set; }
        public required string DistributorOrderId { get; set; } // The distributor's internal order ID
        public int ConfirmedQuantity { get; set; }
        public decimal ConfirmedPricePerUnit { get; set; }
        public int EstimatedDeliveryDays { get; set; }
        public bool Success { get; set; }
        public required string Message { get; set; }
    }
}