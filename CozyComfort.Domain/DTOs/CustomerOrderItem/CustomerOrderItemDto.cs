namespace CozyComfort.Application.DTOs
{
    public class CustomerOrderItemDto
    {
        public int Id { get; set; }

        public int CustomerOrderId { get; set; }

        public int BlanketModelId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Subtotal { get; set; }
    }
}