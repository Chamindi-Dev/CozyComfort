using System.ComponentModel.DataAnnotations;

namespace CozyComfort.Domain.DTOs.User
{
    public class CreateUserDto
    {
        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public string? Phone { get; set; }

        public string? Address { get; set; }

        [Required]
        public int RoleId { get; set; }
    }
}