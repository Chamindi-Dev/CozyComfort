namespace CozyComfort.Domain.DTOs.Seller
{
    public class SellerDto
    {
        public int Id { get; set; }


        public int DistributorId { get; set; }


        public string? DistributorName { get; set; }


        public string SellerName { get; set; } = string.Empty;


        public string StoreType { get; set; } = string.Empty;


        public string? Email { get; set; }


        public string? Phone { get; set; }


        public string? Address { get; set; }


        public DateTime CreatedAt { get; set; }
    }
}