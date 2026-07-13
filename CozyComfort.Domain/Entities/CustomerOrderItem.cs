using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CozyComfort.Domain.Entities
{
    public class CustomerOrderItem
    {
        public int Id { get; set; }


        public int CustomerOrderId { get; set; }


        public int BlanketModelId { get; set; }


        public int Quantity { get; set; }


        public decimal UnitPrice { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal Subtotal { get; private set; }

        [JsonIgnore]
        public CustomerOrder CustomerOrder { get; set; } = null!;

        [JsonIgnore]
        public BlanketModel BlanketModel { get; set; } = null!;
    }
}