using System.ComponentModel.DataAnnotations;

namespace CozyComfort.Seller.API.Models
{
    public class Seller
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}