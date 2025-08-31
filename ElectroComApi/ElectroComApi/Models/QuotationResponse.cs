using System.Collections.Generic;

namespace ElectroComApi.Models
{
    public class QuotationResponse
    {
        public required string OrderId { get; set; }
        public string DistributorName { get; set; } = "ElectroCom"; // Changed for ElectroCom
        public List<ProductQuote> ProductQuotes { get; set; } = new List<ProductQuote>();
        public bool Success { get; set; }
        public required string Message { get; set; }
    }
}