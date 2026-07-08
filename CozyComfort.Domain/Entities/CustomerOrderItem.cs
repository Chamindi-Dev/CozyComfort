using System.ComponentModel.DataAnnotations.Schema;

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



        // Navigation Properties

        public CustomerOrder CustomerOrder { get; set; } = null!;


        public BlanketModel BlanketModel { get; set; } = null!;
    }
}