namespace GadgetCentralApi.Models
{
    public class ProductRequest
    {
        public required string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}