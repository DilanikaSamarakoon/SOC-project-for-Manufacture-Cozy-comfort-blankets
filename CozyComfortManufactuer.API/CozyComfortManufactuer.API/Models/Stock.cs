namespace CozyComfort.Manufacturer.API.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public int BlanketId { get; set; } // Foreign Key to Blanket
        public int Quantity { get; set; }

        // This is the missing property
        public int ProductionCapacityPerWeek { get; set; }
    }
}