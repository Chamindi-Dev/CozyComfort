using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CozyComfort.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? Phone { get; set; }

        public string? Address { get; set; }

        [Required]
        public int RoleId { get; set; }

        [JsonIgnore]
        public Role? Role { get; set; }
    }
}