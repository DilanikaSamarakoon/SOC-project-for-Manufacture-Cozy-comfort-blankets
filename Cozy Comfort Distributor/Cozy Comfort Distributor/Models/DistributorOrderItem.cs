using System.ComponentModel.DataAnnotations;

namespace Cozy_Comfort_Distributor.Models // Use your new project's namespace
{
    public class DistributorOrderItem
    {
        public int Id { get; set; }
        public int DistributorOrderId { get; set; }
        public int BlanketId { get; set; } // This will be a foreign key to a Blanket in the Manufacturer's DB (conceptual)
                                           // For this API, it's just an int. If you communicate with the Manufacturer API,
                                           // you'd fetch Blanket details from there.
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public required DistributorOrder DistributorOrder { get; set; }
        // Note: No 'Blanket' navigation property here, as Blanket model is not in this project.
        // If you want a placeholder or DTO, you'd add it as an ordinary property or link via service.
    }
}