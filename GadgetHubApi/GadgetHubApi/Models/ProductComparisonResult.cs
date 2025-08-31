using System.Collections.Generic;

namespace GadgetHubApi.Models
{
    public class ProductComparisonResult
    {
        public required string ProductId { get; set; }
        public int RequestedQuantity { get; set; }
        public decimal BestPricePerUnit { get; set; }
        public required string BestDistributor { get; set; }
        public int AvailableUnitsFromBest { get; set; }
        public int EstimatedDeliveryDaysFromBest { get; set; }
        public bool FullyFulfilled { get; set; }
        public string StatusMessage { get; set; } = "Quoted successfully.";
        public List<string> OtherDistributorOptions { get; set; } = new List<string>();
    }
}
