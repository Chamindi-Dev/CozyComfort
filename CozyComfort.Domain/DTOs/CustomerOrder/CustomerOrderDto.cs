namespace CozyComfort.Application.DTOs
{
    public class CustomerOrderDto
    {
        public int Id { get; set; }

        public string OrderNumber { get; set; } = string.Empty;

        public int CustomerId { get; set; }

        public int SellerId { get; set; }

        public string Status { get; set; } = string.Empty;

        public string? FinalSource { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? ExpectedDeliveryDate { get; set; }

        public decimal TotalAmount { get; set; }

        public string? Remarks { get; set; }
    }
}