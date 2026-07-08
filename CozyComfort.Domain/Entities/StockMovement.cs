using System.ComponentModel.DataAnnotations;

namespace CozyComfort.Domain.Entities
{
    public class StockMovement
    {
        public int Id { get; set; }

        public int BlanketModelId { get; set; }

        public int? FactoryInventoryId { get; set; }

        public int? DistributorInventoryId { get; set; }

        public int? SellerInventoryId { get; set; }

        [Required]
        [MaxLength(50)]
        public string MovementType { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public int? TransferOrderId { get; set; }

        public int? CustomerOrderId { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation Properties

        public BlanketModel BlanketModel { get; set; } = null!;

        public FactoryInventory? FactoryInventory { get; set; }

        public DistributorInventory? DistributorInventory { get; set; }

        public SellerInventory? SellerInventory { get; set; }

        public TransferOrder? TransferOrder { get; set; }

        public CustomerOrder? CustomerOrder { get; set; }
    }
}