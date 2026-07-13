using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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


        [JsonIgnore]
        public BlanketModel BlanketModel { get; set; } = null!;

        [JsonIgnore]
        public FactoryInventory? FactoryInventory { get; set; }

        [JsonIgnore]
        public DistributorInventory? DistributorInventory { get; set; }

        [JsonIgnore]
        public SellerInventory? SellerInventory { get; set; }

        [JsonIgnore]
        public TransferOrder? TransferOrder { get; set; }

        [JsonIgnore]
        public CustomerOrder? CustomerOrder { get; set; }
    }
}