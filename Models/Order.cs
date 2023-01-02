using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PSP_Komanda32_API.Models
{
    public class Order
    {
        [Required]
        [Key, ForeignKey("Orders")]
        public int id { get; set; }
        public OrderStatus Status { get; set; }
        [Required]
        public int CourierId { get; set; }
        public TimeSpan EstimatedTime { get; set; }
    }
}
