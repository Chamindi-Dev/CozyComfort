using System.ComponentModel.DataAnnotations;

namespace CozyComfort.Domain.Entities
{
    public class Material
    {
        public int Id { get; set; }


        [Required]
        [MaxLength(100)]
        public string MaterialName { get; set; } = string.Empty;


        [MaxLength(255)]
        public string? Description { get; set; }


        // Navigation Property
        public ICollection<BlanketModel> BlanketModels { get; set; }
            = new List<BlanketModel>();
    }
}