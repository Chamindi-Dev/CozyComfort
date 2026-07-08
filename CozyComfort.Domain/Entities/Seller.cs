using System.ComponentModel.DataAnnotations;

namespace CozyComfort.Domain.Entities
{
    public class Seller
    {
        public int Id { get; set; }


        // Foreign Key
        public int DistributorId { get; set; }


        // Navigation Property
        public Distributor? Distributor { get; set; }


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


        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<SellerInventory> SellerInventories { get; set; }
           = new List<SellerInventory>();
        public ICollection<CustomerOrder> CustomerOrders { get; set; }
           = new List<CustomerOrder>();
    }
}