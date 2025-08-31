using System.ComponentModel.DataAnnotations;

namespace CozyComfort.Manufacturer.API.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; } // Made nullable to fix warnings
        public decimal Cost { get; set; }
        public int InventoryCount { get; set; }
    }
}