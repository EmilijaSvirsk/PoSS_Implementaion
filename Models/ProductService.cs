using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PSP_Komanda32_API.Models
{
    public class ProductService
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        [MinLength(1)]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Required]
        [Range(0, int.MaxValue)]
        public int CostInCents { get; set; }
        [Required]
        public int BusinessId { get; set; } 
        [JsonIgnore]
        public bool isDeleted { get; set; }
        
        [JsonIgnore]
        public List<OrderProducts> OrderProducts { get; set; } = new List<OrderProducts>();
    }
}
