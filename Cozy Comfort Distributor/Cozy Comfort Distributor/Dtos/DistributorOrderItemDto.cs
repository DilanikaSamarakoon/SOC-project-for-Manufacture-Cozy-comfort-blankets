namespace Cozy_Comfort_Distributor.Dtos
{
    public class DistributorOrderItemDto
    {
        public int Id { get; set; }
        public int BlanketId { get; set; } // We don't have Blanket model, so just the ID from Manufacturer API
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        
    }
}