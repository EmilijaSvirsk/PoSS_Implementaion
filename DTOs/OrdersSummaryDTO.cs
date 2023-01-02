using PSP_Komanda32_API.Models;

namespace PSP_Komanda32_API.DTOs
{
    public class OrdersSummaryDTO
    {
        public int id { get; set; }
        public int EmployeeId { get; set; }
        public int CustomerId { get; set; }
        public DateTime Date { get; set; }
        public Payment Payment { get; set; }
        public bool IsPaid { get; set; }
        public string Comment { get; set; } = string.Empty;
        public bool IsAccepted { get; set; }
        public string DeclineReason { get; set; } = string.Empty;
        public int TotalCostInCents { get; set; }
    }
}