using System;
using System.Collections.Generic;

namespace DistributorDashboard
{
    // No changes needed in these two DTOs
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
        public string ItemSummary => string.Join(", ", Items.ConvertAll(i => $"{i.Quantity}x {i.ProductName ?? "ID:" + i.BlanketId}"));
    }

    // --- FIXES APPLIED BELOW ---

    public class CreateDistributorOrderDto
    {
        public int DistributorId { get; set; }

        // FIX: Added the Status property, which the API requires.
        public string Status { get; set; }

        public List<CreateDistributorOrderItemDto> Items { get; set; } = new List<CreateDistributorOrderItemDto>();
    }

    public class CreateDistributorOrderItemDto
    {
        public int BlanketId { get; set; }
        public int Quantity { get; set; }


        // FIX: Added the Price property, which the API requires.
        // The API model calls it 'UnitPrice', so we use a JsonProperty to match the name.
        [Newtonsoft.Json.JsonProperty("unitPrice")]
        public decimal Price { get; set; }
    }
}
