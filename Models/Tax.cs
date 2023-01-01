using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PSP_Komanda32_API.Models
{
    public class Tax
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        [Range(0, 100)]
        public decimal Percentage { get; set; }
        [Required]
        [MinLength(1)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MinLength(1)]
        public string Description { get; set; } = string.Empty;
    }
}
