namespace CozyComfort.Manufacturer.API.DTO
{
    public class OrderCreateDto
    {
        //public int OrderId { get; set; }
        public int BlanketId { get; set; }
        public int Quantity { get; set; }

        public string Status { get; set; }
        public DateTime OrderDate { get; set; }
    }
}