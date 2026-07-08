using System.ComponentModel.DataAnnotations;

namespace CozyComfort.Domain.Entities
{
    public class ProductionCapacity
    {
        public int Id { get; set; }

        public int BlanketModelId { get; set; }

        public int DailyCapacity { get; set; }

        public int WeeklyCapacity { get; set; }

        public int CurrentPendingQuantity { get; set; }

        public int LeadTimeDays { get; set; }

        public DateTime LastUpdated { get; set; }

        // Navigation
        public BlanketModel BlanketModel { get; set; } = null!;
    }
}