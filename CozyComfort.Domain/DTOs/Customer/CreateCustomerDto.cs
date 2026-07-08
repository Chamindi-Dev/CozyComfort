using System.ComponentModel.DataAnnotations;

namespace CozyComfort.Application.DTOs.Customer
{
    public class CreateCustomerDto
    {
        [Required]
        [StringLength(150)]
        public string CustomerName { get; set; } = string.Empty;

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }
    }
}