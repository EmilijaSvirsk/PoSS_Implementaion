using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PSP_Komanda32_API.Models
{
    public class Discount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        [Range(0, 100)]
        public decimal Credit { get; set; }
        [Required]
        public int CreatedBy { get; set; }
        [Required]
        [Range(0, 100)]
        public int LoaltyCost { get; set; }
    }
}
