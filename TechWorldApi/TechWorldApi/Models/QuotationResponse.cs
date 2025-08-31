using System.Collections.Generic;

namespace TechWorldApi.Models
{
    public class QuotationResponse
    {
        public required string OrderId { get; set; } // Original Order ID from GadgetHub
        public string DistributorName { get; set; } = "TechWorld"; // Identifier for this distributor
        public List<ProductQuote> ProductQuotes { get; set; } = new List<ProductQuote>();
        public bool Success { get; set; } // Indicates if the request was processed successfully
        public required string Message { get; set; } // Provides additional information or error messages
    }
}