namespace CozyComfort.Manufacturer.API.DTO.Product
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public int InventoryCount { get; set; }
    }
}