using System.ComponentModel.DataAnnotations;

namespace CozyComfort.Domain.DTOs.TransferOrder
{
    public class UpdateTransferOrderDto
    {
        [Required]
        public string TransferNumber { get; set; } = string.Empty;

        public int? CustomerOrderId { get; set; }

        [Required]
        public string FromLocationType { get; set; } = string.Empty;

        [Required]
        public string ToLocationType { get; set; } = string.Empty;

        public int? FromDistributorId { get; set; }

        public int? ToDistributorId { get; set; }

        public int? FromSellerId { get; set; }

        public int? ToSellerId { get; set; }

        public string Status { get; set; } = "Pending";

        public DateTime RequestedDate { get; set; }

        public DateTime? ApprovedDate { get; set; }

        public DateTime? CompletedDate { get; set; }

        public string? Remarks { get; set; }
    }
}