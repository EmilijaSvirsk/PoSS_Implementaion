using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PSP_Komanda32_API.Models
{
    public class BusinessManager
    {
        public int id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string Password { get; set; } = string.Empty;
        public int BusinessId { get; set; }
        public int CreatedBy { get; set; }
    }
}
