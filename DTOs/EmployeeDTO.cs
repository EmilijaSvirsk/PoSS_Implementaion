namespace PSP_Komanda32_API.Models
{
    public class EmployeeDTO
    {
        public int id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string? Email { get; set; }
        public int CreatedBy { get; set; }
    }
}
