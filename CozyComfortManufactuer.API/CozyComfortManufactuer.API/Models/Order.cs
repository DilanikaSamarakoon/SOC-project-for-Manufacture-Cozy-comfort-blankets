namespace CozyComfort.Manufacturer.API.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        //public int SellerId { get; set; }
        public int BlanketId { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public DateTime OrderDate { get; set; }
       // public object OrderId { get; internal set; }
    }
}