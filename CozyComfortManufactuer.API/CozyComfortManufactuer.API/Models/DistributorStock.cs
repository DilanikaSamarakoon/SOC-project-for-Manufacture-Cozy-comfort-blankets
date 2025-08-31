namespace CozyComfort.Distributor.API.Models
{
    public class DistributorStock
    {
        public int Id { get; set; }
        public int BlanketId { get; set; }
        public string BlanketModelName { get; set; }
        public int Quantity { get; set; }
    }
}