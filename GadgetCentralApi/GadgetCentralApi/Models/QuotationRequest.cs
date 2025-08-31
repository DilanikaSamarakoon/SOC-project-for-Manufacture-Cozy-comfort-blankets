using System.Collections.Generic;

namespace GadgetCentralApi.Models
{
    public class QuotationRequest
    {
        public required string OrderId { get; set; }
        public List<ProductRequest> Products { get; set; } = new List<ProductRequest>();
    }
}