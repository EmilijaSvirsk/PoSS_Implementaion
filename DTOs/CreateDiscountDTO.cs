using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PSP_Komanda32_API.DTOs
{
    public class CreateDiscountDTO
    {
        [Range(0, int.MaxValue)]
        public decimal Credit { get; set; }
        [Required]
        public int CreatedBy { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int LoaltyCost { get; set; }
    }
}
