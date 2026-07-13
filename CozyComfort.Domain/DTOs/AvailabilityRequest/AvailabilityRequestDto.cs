namespace CozyComfort.Domain.DTOs.AvailabilityRequest
{
    public class AvailabilityRequestDto
    {
        public int Id { get; set; }

        public string RequestNumber { get; set; } = string.Empty;

        public int CustomerOrderId { get; set; }

        public int BlanketModelId { get; set; }

        public int SellerId { get; set; }

        public int DistributorId { get; set; }

        public int RequestedQuantity { get; set; }

        public string RequestLevel { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public DateTime RequestedDate { get; set; }

        public DateTime? ResponseDate { get; set; }

        public int? ExpectedLeadTimeDays { get; set; }

        public string? ResponseMessage { get; set; }
    }
}