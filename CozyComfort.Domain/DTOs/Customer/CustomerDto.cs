namespace CozyComfort.Domain.DTOs.Customer
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
    }
}