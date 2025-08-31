using System.ComponentModel.DataAnnotations;

namespace Cozy_Comfort_Distributor.Dtos
{
    public class CreateDistributorDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [StringLength(100)]
        public string ContactPerson { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;
        [Required]
        [Phone]
        [StringLength(20)]
        public string Phone { get; set; } = string.Empty;
        [StringLength(200)]
        public string? Address { get; set; }
    }
}