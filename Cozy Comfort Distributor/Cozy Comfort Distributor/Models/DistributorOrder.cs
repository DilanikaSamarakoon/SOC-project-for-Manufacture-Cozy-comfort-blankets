using System.ComponentModel.DataAnnotations;
using Cozy_Comfort_Distributor.Models;

namespace Cozy_Comfort_Distributor.Models // Use your new project's namespace
{
    public class DistributorOrder
    {
        public int Id { get; set; }
        public int DistributorId { get; set; }
        public DateTime OrderDate { get; set; }
        public required string Status { get; set; } // e.g., "Pending", "Processing", "Shipped", "Delivered"
        public decimal TotalAmount { get; set; } // This will be calculated

        public required Distributor Distributor { get; set; }
        public ICollection<DistributorOrderItem>? Items { get; set; }
    }
}