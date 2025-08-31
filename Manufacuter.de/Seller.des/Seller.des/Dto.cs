using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SellerDashboard
{
    public class DistributorOrderItemDto
    {
        public int Id { get; set; }
        public int BlanketId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    public class DistributorOrderDto
    {
        public int Id { get; set; }
        public int DistributorId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public List<DistributorOrderItemDto> Items { get; set; } = new List<DistributorOrderItemDto>();
    }

    public class CreateDistributorOrderDto
    {
        public int DistributorId { get; set; }
        public string Status { get; set; }
        public List<CreateDistributorOrderItemDto> Items { get; set; } = new List<CreateDistributorOrderItemDto>();
    }

    public class CreateDistributorOrderItemDto
    {
        
        public int Quantity { get; set; }

        // This attribute ensures the property is named 'unitPrice' in the JSON, matching the API
        [JsonProperty("unitPrice")]
        public decimal Price { get; set; }
        public int BlanketId { get; internal set; }
    }
    namespace Seller.des
    {
        // Represents a stock item for a seller from GET /api/Sellers/{id}/stock
        public class SellerStockItem
        {
            public int BlanketId { get; set; }
            public string BlanketName { get; set; } // Assuming you have a name property
            public int Quantity { get; set; }
        }

        // Represents an order from GET /api/orders
        public class SellerOrder
        {
            public int OrderId { get; set; }
            public string Status { get; set; }
            // Add other properties like OrderDate if your API provides them
        }
    }
}
