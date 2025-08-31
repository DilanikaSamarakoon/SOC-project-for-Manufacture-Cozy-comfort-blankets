using System.Collections.Generic;

namespace GadgetHubApi.Models
{
    public class CustomerOrderRequest
    {
        public required string OrderId { get; set; }
        public List<CustomerOrderItem> Items { get; set; } = new List<CustomerOrderItem>();
    }
}