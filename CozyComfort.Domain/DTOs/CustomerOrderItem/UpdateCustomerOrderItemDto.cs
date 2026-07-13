using System.ComponentModel.DataAnnotations;

namespace CozyComfort.Domain.DTOs
{
    public class UpdateCustomerOrderItemDto
    {

        [Required]
        public int BlanketModelId { get; set; }


        [Required]
        public int Quantity { get; set; }


        [Required]
        public decimal UnitPrice { get; set; }

    }
}