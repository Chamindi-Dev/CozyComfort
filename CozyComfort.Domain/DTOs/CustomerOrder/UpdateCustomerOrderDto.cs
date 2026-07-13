using System.ComponentModel.DataAnnotations;

namespace CozyComfort.Domain.DTOs
{
    public class UpdateCustomerOrderDto
    {

        [Required]
        public int CustomerId { get; set; }


        [Required]
        public int SellerId { get; set; }


        [Required]
        public string Status { get; set; } = "Pending";


        public string? FinalSource { get; set; }


        public DateTime? ExpectedDeliveryDate { get; set; }


        public decimal TotalAmount { get; set; }


        public string? Remarks { get; set; }

    }
}