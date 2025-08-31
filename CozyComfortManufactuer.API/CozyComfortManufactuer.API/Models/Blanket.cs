// This code goes in your API Project, inside a file named Blanket.cs
// (Usually located in a 'Models' or 'Data' folder)

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CozyComfort.Manufacturer.API.Models // Or whatever your data namespace is
{
    public class Blanket
    {
        [Key]
        public int Id { get; set; }

        // Initialize with a default value to satisfy the non-nullable error
        public string ModelName { get; set; } = string.Empty;

        // Initialize with a default value
        public string Material { get; set; } = string.Empty;

        // Added the missing 'Price' property and specified the column type
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
    }
}
