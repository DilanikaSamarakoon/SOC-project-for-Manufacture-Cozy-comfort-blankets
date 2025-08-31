using System.Collections.Generic;

namespace TechWorldApi.Models
{
    public class QuotationRequest
    {
        public required string OrderId { get; set; } // Unique ID for the order from GadgetHub
        public List<ProductRequest> Products { get; set; } = new List<ProductRequest>();
    }
}