using System.ComponentModel.DataAnnotations;

namespace CozyComfort.Data.DTOs
{
    public class CreateSellerInventoryDto
    {
        [Required]
        public int SellerId { get; set; }

        [Required]
        public int BlanketModelId { get; set; }

        [Required]
        public int QuantityOnHand { get; set; }

        [Required]
        public int ReservedQuantity { get; set; }
    }
}