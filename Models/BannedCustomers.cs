namespace PSP_Komanda32_API.Models
{
    public class BannedCustomers
    {
        public int CustomerId { get; set; }
        public int BannedBy { get; set; }
        public string Reason { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}
