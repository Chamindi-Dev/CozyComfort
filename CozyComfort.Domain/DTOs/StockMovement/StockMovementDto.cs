namespace CozyComfort.Domain.DTOs.StockMovement
{
    public class StockMovementDto
    {
        public int Id { get; set; }

        public int BlanketModelId { get; set; }

        public int? FactoryInventoryId { get; set; }

        public int? DistributorInventoryId { get; set; }

        public int? SellerInventoryId { get; set; }

        public string MovementType { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public int? TransferOrderId { get; set; }

        public int? CustomerOrderId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}