using System.ComponentModel.DataAnnotations;

namespace CozyComfort.Application.DTOs
{
    public class UpdateFactoryInventoryDto
    {
        [Required]
        public int BlanketModelId { get; set; }

        [Range(0, int.MaxValue)]
        public int QuantityOnHand { get; set; }

        [Range(0, int.MaxValue)]
        public int ReservedQuantity { get; set; }
    }
}