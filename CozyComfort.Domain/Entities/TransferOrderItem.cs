using System.ComponentModel.DataAnnotations;

namespace CozyComfort.Domain.Entities
{
    public class TransferOrderItem
    {
        public int Id { get; set; }

        public int TransferOrderId { get; set; }

        public int BlanketModelId { get; set; }

        public int Quantity { get; set; }

        // Navigation Properties

        public TransferOrder TransferOrder { get; set; } = null!;

        public BlanketModel BlanketModel { get; set; } = null!;
    }
}