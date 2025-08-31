using System.Collections.Generic;

namespace GadgetCentralApi.Models
{
    public class QuotationResponse
    {
        public required string OrderId { get; set; }
        public string DistributorName { get; set; } = "Gadget Central"; // Changed for Gadget Central
        public List<ProductQuote> ProductQuotes { get; set; } = new List<ProductQuote>();
        public bool Success { get; set; }
        public required string Message { get; set; }
    }
}