using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CozyComfort.Domain.Entities
{
    public class TransferOrderItem
    {
        public int Id { get; set; }

        public int TransferOrderId { get; set; }

        public int BlanketModelId { get; set; }

        public int Quantity { get; set; }

        [JsonIgnore]
        public TransferOrder TransferOrder { get; set; } = null!;

        [JsonIgnore]
        public BlanketModel BlanketModel { get; set; } = null!;
    }
}