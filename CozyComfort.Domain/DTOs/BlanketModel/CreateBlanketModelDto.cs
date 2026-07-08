using System.ComponentModel.DataAnnotations;

namespace CozyComfort.Application.DTOs.BlanketModel
{
    public class CreateBlanketModelDto
    {
        [Required]
        [MaxLength(100)]
        public string SKU { get; set; } = string.Empty;


        [Required]
        [MaxLength(150)]
        public string ModelName { get; set; } = string.Empty;


        [Required]
        public int MaterialId { get; set; }


        [MaxLength(50)]
        public string? Size { get; set; }


        [MaxLength(50)]
        public string? Color { get; set; }


        [Required]
        public decimal UnitPrice { get; set; }


        public bool IsActive { get; set; } = true;
    }
}