using System.ComponentModel.DataAnnotations;

namespace CozyComfort.Domain.DTOs
{
    public class CreateCustomerOrderDto
    {

        [Required]
        [MaxLength(100)]
        public string OrderNumber { get; set; } = string.Empty;


        [Required]
        public int CustomerId { get; set; }


        [Required]
        public int SellerId { get; set; }


        public string Status { get; set; } = "Pending";


        public string? FinalSource { get; set; }


        public DateTime? ExpectedDeliveryDate { get; set; }


        public decimal TotalAmount { get; set; }


        [MaxLength(500)]
        public string? Remarks { get; set; }
    }
}