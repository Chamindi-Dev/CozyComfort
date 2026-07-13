using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CozyComfort.Domain.Entities
{
    public class DistributorInventory
    {
        public int Id { get; set; }

        public int DistributorId { get; set; }

        public int BlanketModelId { get; set; }

        public int QuantityOnHand { get; set; }

        public int ReservedQuantity { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int AvailableQuantity { get; private set; }

        public DateTime LastUpdated { get; set; }

        [JsonIgnore]
        public Distributor Distributor { get; set; } = null!;

        [JsonIgnore]
        public BlanketModel BlanketModel { get; set; } = null!;

        [JsonIgnore]
        public ICollection<StockMovement> StockMovements { get; set; }
        = new List<StockMovement>();
    }
}