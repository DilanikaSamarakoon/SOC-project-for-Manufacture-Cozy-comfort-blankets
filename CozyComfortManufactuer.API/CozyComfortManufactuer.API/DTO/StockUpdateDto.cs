namespace CozyComfort.Manufacturer.API.DTO
{
    public class StockUpdateDto
    {
        public int Id { get; set; }
        public int BlanketId { get; set; }
        public int Quantity { get; set; }
        public int ProductionCapacityPerWeek { get; set; }
    }
}