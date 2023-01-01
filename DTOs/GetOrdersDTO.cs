using PSP_Komanda32_API.Models;

namespace PSP_Komanda32_API.DTOs
{
    public class GetOrdersDTO
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int CustomerId { get; set; }
        public DateTime Date { get; set; }
        public Payment Payment { get; set; }
        public bool IsPaid { get; set; }
        public string Comment { get; set; } = string.Empty;
        public bool IsAccepted { get; set; }
        public string DeclineReason { get; set; } = string.Empty;
        public int DeliveryAddressId { get; set; }
        public List<ProductService> ProductServices { get; set; } = new List<ProductService>();
    }
}