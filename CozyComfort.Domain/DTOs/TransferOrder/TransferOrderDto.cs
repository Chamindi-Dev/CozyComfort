namespace CozyComfort.Application.DTOs.TransferOrder
{
    public class TransferOrderDto
    {
        public int Id { get; set; }

        public string TransferNumber { get; set; } = string.Empty;

        public int? CustomerOrderId { get; set; }

        public string FromLocationType { get; set; } = string.Empty;

        public string ToLocationType { get; set; } = string.Empty;

        public int? FromDistributorId { get; set; }

        public int? ToDistributorId { get; set; }

        public int? FromSellerId { get; set; }

        public int? ToSellerId { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime RequestedDate { get; set; }

        public DateTime? ApprovedDate { get; set; }

        public DateTime? CompletedDate { get; set; }

        public string? Remarks { get; set; }
    }
}