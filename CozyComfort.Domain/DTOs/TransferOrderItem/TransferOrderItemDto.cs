namespace CozyComfort.Application.DTOs.TransferOrderItem
{
    public class TransferOrderItemDto
    {
        public int Id { get; set; }

        public int TransferOrderId { get; set; }

        public int BlanketModelId { get; set; }

        public int Quantity { get; set; }
    }
}