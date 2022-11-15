namespace PSP_Komanda32_API.Models
{
    public class NonWork
    {
        public int EmployeeId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }   
        public string Reason { get; set; } = string.Empty;
        public string DocumentProof { get; set; } = string.Empty;
    }
}
