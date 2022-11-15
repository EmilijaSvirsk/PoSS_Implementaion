namespace PSP_Komanda32_API.Models
{
    public class BusinessAdministrator
    {
        public int id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string Password { get; set; } = string.Empty;
        public int CreatedBy { get; set; }
        public int BusinessId { get; set; }
    }
}
