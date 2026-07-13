using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CozyComfort.Domain.Entities
{
    public class TransferOrder
    {
        public int Id { get; set; }


        [Required]
        [MaxLength(100)]
        public string TransferNumber { get; set; } = string.Empty;


        public int? CustomerOrderId { get; set; }


        [Required]
        [MaxLength(50)]
        public string FromLocationType { get; set; } = string.Empty;


        [Required]
        [MaxLength(50)]
        public string ToLocationType { get; set; } = string.Empty;


        public int? FromDistributorId { get; set; }

        public int? ToDistributorId { get; set; }


        public int? FromSellerId { get; set; }

        public int? ToSellerId { get; set; }



        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Pending";


        public DateTime RequestedDate { get; set; }


        public DateTime? ApprovedDate { get; set; }


        public DateTime? CompletedDate { get; set; }


        [MaxLength(500)]
        public string? Remarks { get; set; }



        // Navigation
        [JsonIgnore]
        public CustomerOrder? CustomerOrder { get; set; }

        [JsonIgnore]
        public Distributor? FromDistributor { get; set; }

        [JsonIgnore]
        public Distributor? ToDistributor { get; set; }

        [JsonIgnore]
        public Seller? FromSeller { get; set; }

        [JsonIgnore]
        public Seller? ToSeller { get; set; }

        [JsonIgnore]
        public ICollection<TransferOrderItem> TransferOrderItems { get; set; }
         = new List<TransferOrderItem>();

        [JsonIgnore]
        public ICollection<StockMovement> StockMovements { get; set; }
         = new List<StockMovement>();
    }
}