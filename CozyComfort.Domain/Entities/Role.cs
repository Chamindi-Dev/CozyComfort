using System.Text.Json.Serialization;

namespace CozyComfort.Domain.Entities
{
    public class Role
    {
        public int Id { get; set; }

        public string RoleName { get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<User> Users { get; set; }
            = new List<User>();
    }
}