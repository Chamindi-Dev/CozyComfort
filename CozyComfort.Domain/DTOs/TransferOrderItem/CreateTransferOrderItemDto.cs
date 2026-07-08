using System.ComponentModel.DataAnnotations;

namespace CozyComfort.Application.DTOs.TransferOrderItem
{
    public class CreateTransferOrderItemDto
    {
        [Required]
        public int TransferOrderId { get; set; }

        [Required]
        public int BlanketModelId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}