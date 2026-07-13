using System.ComponentModel.DataAnnotations;

namespace CozyComfort.Domain.DTOs
{
    public class UpdateProductionCapacityDto
    {
        [Required]
        public int BlanketModelId { get; set; }

        [Required]
        public int DailyCapacity { get; set; }

        [Required]
        public int WeeklyCapacity { get; set; }

        public int CurrentPendingQuantity { get; set; }

        [Required]
        public int LeadTimeDays { get; set; }
    }
}