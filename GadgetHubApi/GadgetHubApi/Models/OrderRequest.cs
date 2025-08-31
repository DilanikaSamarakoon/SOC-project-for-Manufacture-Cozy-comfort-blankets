namespace GadgetHubApi.Models
{
    // Represents an order request from The GadgetHub to a distributor
    public class OrderRequest
    {
        public required string GadgetHubOrderId { get; set; } // The original order ID from GadgetHub
        public required string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal AgreedPricePerUnit { get; set; } // The price GadgetHub expects to pay
    }
}
