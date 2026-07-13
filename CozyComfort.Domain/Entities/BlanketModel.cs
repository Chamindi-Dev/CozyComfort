using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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


        public int MaterialId { get; set; }


        public Material? Material { get; set; }


        [MaxLength(50)]
        public string? Size { get; set; }


        [MaxLength(50)]
        public string? Color { get; set; }


        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }


        public bool IsActive { get; set; } = true;

        [JsonIgnore]
        public ICollection<FactoryInventory> FactoryInventories { get; set; }
         = new List<FactoryInventory>();

        [JsonIgnore]
        public ICollection<DistributorInventory> DistributorInventories { get; set; }
         = new List<DistributorInventory>();

        [JsonIgnore]
        public ICollection<SellerInventory> SellerInventories { get; set; }
         = new List<SellerInventory>();

        [JsonIgnore]
        public ICollection<ProductionCapacity> ProductionCapacities { get; set; }
         = new List<ProductionCapacity>();

        [JsonIgnore]
        public ICollection<CustomerOrderItem> CustomerOrderItems { get; set; }
         = new List<CustomerOrderItem>();

        [JsonIgnore]
        public ICollection<AvailabilityRequest> AvailabilityRequests { get; set; }
         = new List<AvailabilityRequest>();

        [JsonIgnore]
        public ICollection<TransferOrderItem> TransferOrderItems { get; set; }
         = new List<TransferOrderItem>();

        [JsonIgnore]
        public ICollection<StockMovement> StockMovements { get; set; }
         = new List<StockMovement>();
    }
}