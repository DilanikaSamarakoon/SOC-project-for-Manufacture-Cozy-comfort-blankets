using System.Collections.Generic;

namespace GadgetHubClient.Models
{
    public class GadgetHubOrderSummary
    {
        public required string OrderId { get; set; }
        public decimal TotalEstimatedCost { get; set; }
        public List<ProductComparisonResult> ProductResults { get; set; } = new List<ProductComparisonResult>();
        public required string OverallStatus { get; set; }
        public required string OverallMessage { get; set; }
    }
}