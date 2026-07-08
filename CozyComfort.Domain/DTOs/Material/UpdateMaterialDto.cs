using System.ComponentModel.DataAnnotations;

namespace CozyComfort.Application.DTOs.Material
{
    public class UpdateMaterialDto
    {
        [Required]
        [MaxLength(100)]
        public string MaterialName { get; set; } = string.Empty;


        [MaxLength(255)]
        public string? Description { get; set; }
    }
}