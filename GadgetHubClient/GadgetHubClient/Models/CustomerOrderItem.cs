namespace GadgetHubClient.Models
{
    public class CustomerOrderItem
    {
        public required string ProductId { get; set; }

        public String ProductName { get; set; }
        public int Quantity { get; set; }
    }
}