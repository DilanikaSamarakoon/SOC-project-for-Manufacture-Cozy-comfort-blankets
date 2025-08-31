// In your DTOs folder, create a new file CreateSellerDto.cs

namespace CozyComfort.Seller.API.DTOs
{
    public class CreateSellerDto
    {
        // No Id property here!
        public string Name { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}