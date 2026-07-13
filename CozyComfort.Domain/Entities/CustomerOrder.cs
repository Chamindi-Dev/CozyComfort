using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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

        [JsonIgnore]
        public Customer Customer { get; set; } = null!;

        [JsonIgnore]
        public Seller Seller { get; set; } = null!;

        [JsonIgnore]
        public ICollection<CustomerOrderItem> CustomerOrderItems { get; set; }
           = new List<CustomerOrderItem>();

        [JsonIgnore]
        public ICollection<AvailabilityRequest> AvailabilityRequests { get; set; }
          = new List<AvailabilityRequest>();

        [JsonIgnore]
        public ICollection<StockMovement> StockMovements { get; set; }
          = new List<StockMovement>();
    }
}