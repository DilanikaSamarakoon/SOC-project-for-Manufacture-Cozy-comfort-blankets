using System.Collections.Generic;

namespace GadgetHubApi.Models
{
    public class QuotationResponse
    {
        public required string OrderId { get; set; }
        public required string DistributorName { get; set; }
        public List<ProductQuote> ProductQuotes { get; set; } = new List<ProductQuote>();
        public bool Success { get; set; }
        public required string Message { get; set; }
    }
}
