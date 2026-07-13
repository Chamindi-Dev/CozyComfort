using System.ComponentModel.DataAnnotations;

namespace CozyComfort.Domain.DTOs.Seller
{
    public class CreateSellerDto
    {

        [Required]
        public int DistributorId { get; set; }


        [Required]
        [MaxLength(150)]
        public string SellerName { get; set; } = string.Empty;


        [Required]
        [MaxLength(50)]
        public string StoreType { get; set; } = string.Empty;


        [MaxLength(150)]
        public string? Email { get; set; }


        [MaxLength(30)]
        public string? Phone { get; set; }


        [MaxLength(255)]
        public string? Address { get; set; }

    }
}