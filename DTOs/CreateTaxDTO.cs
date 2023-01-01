using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PSP_Komanda32_API.DTOs
{
    public class CreateTaxDTO
    {
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
