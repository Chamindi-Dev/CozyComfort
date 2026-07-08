namespace CozyComfort.Application.DTOs.Material
{
    public class MaterialDto
    {
        public int Id { get; set; }

        public string MaterialName { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}