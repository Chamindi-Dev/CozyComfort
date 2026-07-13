using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CozyComfort.Domain.Entities
{
    public class Distributor
    {
        public int Id { get; set; }

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

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [JsonIgnore]
        public ICollection<Seller> Sellers { get; set; }
            = new List<Seller>();

        [JsonIgnore]
        public ICollection<DistributorInventory> DistributorInventories { get; set; }
            = new List<DistributorInventory>();
    }
}