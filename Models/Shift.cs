namespace PSP_Komanda32_API.Models
{
    public class Shift
    {
        public int EmployeeId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Desription { get; set; } = string.Empty;
        public bool CheckedIn { get; set; }
        public bool CheckedOut { get; set; }
        public bool EmergencyOut { get; set; }
        public int CreatedBy { get; set; }
    }
}
