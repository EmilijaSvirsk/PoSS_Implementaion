using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PSP_Komanda32_API.Models
{
    public class Orders
    {
        public int id { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public DateTime Date { get; set; }

        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Payment Payment { get; set; }
        public bool IsPaid { get; set; }
        public string Comment { get; set; } = string.Empty;
        public bool IsAccepted { get; set; }
        public string DeclineReason { get; set; } = string.Empty;
        [Required]
        public int DeliveryAddressId { get; set; }       

        public virtual ICollection<OrderProducts> OrderProducts { get; set; } = new List<OrderProducts>();
    }
}
