using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CozyComfort.Domain.Entities
{
    public class BlanketModel
    {
        public int Id { get; set; }


        [Required]
        [MaxLength(100)]
        public string SKU { get; set; } = string.Empty;


        [Required]
        [MaxLength(150)]
        public string ModelName { get; set; } = string.Empty;


        // Foreign Key
        public int MaterialId { get; set; }


        // Navigation Property
        public Material? Material { get; set; }


        [MaxLength(50)]
        public string? Size { get; set; }


        [MaxLength(50)]
        public string? Color { get; set; }


        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }


        public bool IsActive { get; set; } = true;

        public ICollection<FactoryInventory> FactoryInventories { get; set; }
         = new List<FactoryInventory>();

        public ICollection<DistributorInventory> DistributorInventories { get; set; }
         = new List<DistributorInventory>();

        public ICollection<SellerInventory> SellerInventories { get; set; }
         = new List<SellerInventory>();

        public ICollection<ProductionCapacity> ProductionCapacities { get; set; }
         = new List<ProductionCapacity>();

        public ICollection<CustomerOrderItem> CustomerOrderItems { get; set; }
         = new List<CustomerOrderItem>();

        public ICollection<AvailabilityRequest> AvailabilityRequests { get; set; }
         = new List<AvailabilityRequest>();

        public ICollection<TransferOrderItem> TransferOrderItems { get; set; }
         = new List<TransferOrderItem>();
        public ICollection<StockMovement> StockMovements { get; set; }
         = new List<StockMovement>();
    }
}