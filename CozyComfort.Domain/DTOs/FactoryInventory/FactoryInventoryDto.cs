namespace CozyComfort.Domain.DTOs
{
    public class FactoryInventoryDto
    {
        public int Id { get; set; }

        public int BlanketModelId { get; set; }

        public int QuantityOnHand { get; set; }

        public int ReservedQuantity { get; set; }

        public int AvailableQuantity { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}