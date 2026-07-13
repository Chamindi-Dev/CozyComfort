using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CozyComfort.Domain.Entities
{
    public class SellerInventory
    {
        public int Id { get; set; }

        public int SellerId { get; set; }

        public int BlanketModelId { get; set; }

        public int QuantityOnHand { get; set; }

        public int ReservedQuantity { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int AvailableQuantity { get; private set; }

        public DateTime LastUpdated { get; set; }

        [JsonIgnore]
        public Seller Seller { get; set; } = null!;

        [JsonIgnore]
        public BlanketModel BlanketModel { get; set; } = null!;

        [JsonIgnore]
        public ICollection<StockMovement> StockMovements { get; set; }
        = new List<StockMovement>();
    }
}