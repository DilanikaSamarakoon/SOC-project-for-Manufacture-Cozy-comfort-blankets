namespace CozyComfort.Seller.API.DTOs
{
    // FOR CREATING ORDERS (POST)

    public class CreateSellerOrderItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class CreateSellerOrderDto
    {
        public int SellerId { get; set; }
        public List<CreateSellerOrderItemDto> Items { get; set; } = new List<CreateSellerOrderItemDto>();
    }

    // FOR VIEWING ORDERS (GET)

    public class SellerOrderItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class SellerOrderDto
    {
        public int Id { get; set; }
        public int SellerId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public List<SellerOrderItemDto> OrderItems { get; set; } = new List<SellerOrderItemDto>();
    }

    // FOR UPDATING AN ORDER (PUT)

    public class UpdateOrderStatusDto
    {
        public string Status { get; set; }
    }
}