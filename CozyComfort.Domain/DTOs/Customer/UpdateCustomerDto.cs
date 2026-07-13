using System.ComponentModel.DataAnnotations;

namespace CozyComfort.Domain.DTOs.Customer
{
    public class UpdateCustomerDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string CustomerName { get; set; } = string.Empty;

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }
    }
}