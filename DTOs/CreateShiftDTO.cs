using System.ComponentModel.DataAnnotations;

namespace PSP_Komanda32_API.DTOs
{
    public class CreateShiftDTO : IValidatableObject
    {
        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }

        [Required]
        public string Location { get; set; }
        public string Desription { get; set; }
        public bool CheckedIn { get; set; }
        public bool CheckedOut { get; set; }
        public bool EmergencyOut { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Start > End)
            {
                yield return new ValidationResult(
                    $"Shift can't start after end.",
                    new[] { nameof(Start), nameof(End) });
            }
        }
    }
}
