using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PSP_Komanda32_API.Models
{
    public class ProductService
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string Name { get; set; } = string.Empty;    
        public string Description { get; set; } = string.Empty;
        public int CostInCents { get; set; }
        public int BusinessId { get; set; }
    }
}
