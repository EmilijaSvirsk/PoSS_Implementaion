using System.ComponentModel.DataAnnotations;

namespace PSP_Komanda32_API.DTOs
{
    public class CreateReservationDTO
    {
        [Required]
        public int OrderId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int Duration { get; set; } //In minutes
        [Required]
        public int CustomerCount { get; set; }
    }
}
