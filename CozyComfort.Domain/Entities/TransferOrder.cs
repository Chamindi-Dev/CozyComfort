using System.ComponentModel.DataAnnotations;

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

        public CustomerOrder? CustomerOrder { get; set; }

        public Distributor? FromDistributor { get; set; }

        public Distributor? ToDistributor { get; set; }

        public Seller? FromSeller { get; set; }

        public Seller? ToSeller { get; set; }

        public ICollection<TransferOrderItem> TransferOrderItems { get; set; }
         = new List<TransferOrderItem>();
        public ICollection<StockMovement> StockMovements { get; set; }
         = new List<StockMovement>();
    }
}