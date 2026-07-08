using System.ComponentModel.DataAnnotations;

namespace CozyComfort.Domain.Entities
{
    public class CustomerOrder
    {
        public int Id { get; set; }


        [Required]
        [MaxLength(100)]
        public string OrderNumber { get; set; } = string.Empty;


        public int CustomerId { get; set; }


        public int SellerId { get; set; }


        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Pending";


        [MaxLength(50)]
        public string? FinalSource { get; set; }


        public DateTime OrderDate { get; set; }


        public DateTime? ExpectedDeliveryDate { get; set; }


        public decimal TotalAmount { get; set; }


        [MaxLength(500)]
        public string? Remarks { get; set; }



        // Navigation Properties

        public Customer Customer { get; set; } = null!;


        public Seller Seller { get; set; } = null!;

        public ICollection<CustomerOrderItem> CustomerOrderItems { get; set; }
           = new List<CustomerOrderItem>();

        public ICollection<AvailabilityRequest> AvailabilityRequests { get; set; }
          = new List<AvailabilityRequest>();
        public ICollection<StockMovement> StockMovements { get; set; }
          = new List<StockMovement>();
    }
}