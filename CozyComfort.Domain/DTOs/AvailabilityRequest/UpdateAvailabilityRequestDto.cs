namespace CozyComfort.Application.DTOs.AvailabilityRequest
{
    public class UpdateAvailabilityRequestDto
    {
        public int Id { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime? ResponseDate { get; set; }

        public int? ExpectedLeadTimeDays { get; set; }

        public string? ResponseMessage { get; set; }
    }
}