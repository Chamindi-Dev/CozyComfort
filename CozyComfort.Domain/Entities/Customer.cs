using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CozyComfort.Domain.Entities
{
    [Table("Customers")]
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string CustomerName { get; set; } = string.Empty;

        [StringLength(150)]
        public string? Email { get; set; }

        [StringLength(30)]
        public string? Phone { get; set; }

        [StringLength(255)]
        public string? Address { get; set; }

        [JsonIgnore]
        public ICollection<CustomerOrder> CustomerOrders { get; set; }
            = new List<CustomerOrder>();
    }
}