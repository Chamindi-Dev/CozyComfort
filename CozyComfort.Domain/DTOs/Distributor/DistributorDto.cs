namespace CozyComfort.Domain.DTOs.Distributor
{
    public class DistributorDto
    {
        public int Id { get; set; }

        public string DistributorName { get; set; } = string.Empty;

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public string? ServiceArea { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}