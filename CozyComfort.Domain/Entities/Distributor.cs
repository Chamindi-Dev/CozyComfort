using System.ComponentModel.DataAnnotations;

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

        // Navigation Properties
        public ICollection<Seller> Sellers { get; set; }
            = new List<Seller>();

        public ICollection<DistributorInventory> DistributorInventories { get; set; }
            = new List<DistributorInventory>();
    }
}