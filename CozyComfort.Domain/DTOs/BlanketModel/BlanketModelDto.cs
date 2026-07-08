namespace CozyComfort.Application.DTOs.BlanketModel
{
    public class BlanketModelDto
    {
        public int Id { get; set; }

        public string SKU { get; set; } = string.Empty;

        public string ModelName { get; set; } = string.Empty;

        public int MaterialId { get; set; }

        public string? MaterialName { get; set; }

        public string? Size { get; set; }

        public string? Color { get; set; }

        public decimal UnitPrice { get; set; }

        public bool IsActive { get; set; }
    }
}