using System.ComponentModel.DataAnnotations;

namespace Cozy_Comfort_Distributor.Models // Use your new project's namespace
{
    public class Distributor
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string ContactPerson { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public string? Address { get; set; }

        public ICollection<DistributorOrder>? DistributorOrders { get; set; }
    }
}