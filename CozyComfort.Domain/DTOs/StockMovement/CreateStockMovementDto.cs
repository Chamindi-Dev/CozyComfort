using System.ComponentModel.DataAnnotations;

namespace CozyComfort.Domain.DTOs.StockMovement
{
    public class CreateStockMovementDto
    {
        [Required]
        public int BlanketModelId { get; set; }

        public int? FactoryInventoryId { get; set; }

        public int? DistributorInventoryId { get; set; }

        public int? SellerInventoryId { get; set; }

        [Required]
        [MaxLength(50)]
        public string MovementType { get; set; } = string.Empty;

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        public int? TransferOrderId { get; set; }

        public int? CustomerOrderId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}