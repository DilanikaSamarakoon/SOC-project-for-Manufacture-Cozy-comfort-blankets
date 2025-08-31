namespace Cozy_Comfort_Distributor.Dtos
{
    public class DistributorOrderDto
    {
        public int Id { get; set; }
        public int DistributorId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public string? DistributorName { get; set; } // To show related distributor name
        public ICollection<DistributorOrderItemDto>? Items { get; set; } // Nested items
    }
}