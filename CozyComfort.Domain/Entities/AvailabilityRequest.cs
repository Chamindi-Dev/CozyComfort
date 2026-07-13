using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CozyComfort.Domain.Entities
{
    public class AvailabilityRequest
    {
        public int Id { get; set; }


        [Required]
        [MaxLength(100)]
        public string RequestNumber { get; set; } = string.Empty;


        public int CustomerOrderId { get; set; }


        public int BlanketModelId { get; set; }


        public int SellerId { get; set; }


        public int DistributorId { get; set; }


        public int RequestedQuantity { get; set; }


        [Required]
        [MaxLength(50)]
        public string RequestLevel { get; set; } = string.Empty;


        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Pending";


        public DateTime RequestedDate { get; set; }


        public DateTime? ResponseDate { get; set; }


        public int? ExpectedLeadTimeDays { get; set; }


        [MaxLength(500)]
        public string? ResponseMessage { get; set; }

        [JsonIgnore]
        public CustomerOrder CustomerOrder { get; set; } = null!;

        [JsonIgnore]
        public BlanketModel BlanketModel { get; set; } = null!;

        [JsonIgnore]
        public Seller Seller { get; set; } = null!;

        [JsonIgnore]
        public Distributor Distributor { get; set; } = null!;
    }
}