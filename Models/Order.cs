namespace PSP_Komanda32_API.Models
{
    public class Order
    {
        public int id { get; set; }
        public OrderStatus Status { get; set; }
        public int CourierId { get; set; }
        public TimeOnly EstimatedTime { get; set; }
    }
}
