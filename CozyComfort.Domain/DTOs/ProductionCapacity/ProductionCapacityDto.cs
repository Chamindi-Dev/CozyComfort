namespace CozyComfort.Application.DTOs
{
    public class ProductionCapacityDto
    {
        public int Id { get; set; }

        public int BlanketModelId { get; set; }

        public int DailyCapacity { get; set; }

        public int WeeklyCapacity { get; set; }

        public int CurrentPendingQuantity { get; set; }

        public int LeadTimeDays { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}