using System.ComponentModel.DataAnnotations;

namespace CozyComfort.Domain.DTOs.Distributor
{
    public class CreateDistributorDto
    {
        [Required]
        [MaxLength(150)]
        public string DistributorName { get; set; } = string.Empty;


        [MaxLength(150)]
        public string? Email { get; set; }


        [MaxLength(30)]
        public string? Phone { get; set; }


        [MaxLength(255)]
        public string? Address { get; set; }


        [MaxLength(150)]
        public string? ServiceArea { get; set; }
    }
}