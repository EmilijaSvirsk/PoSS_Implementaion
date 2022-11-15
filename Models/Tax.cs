namespace PSP_Komanda32_API.Models
{
    public class Tax
    {
        public int Id { get; set; }
        public decimal Percentage { get; set; }
        public string Name { get; set; } = string.Empty;    
        public string Description { get; set; } = string.Empty;
    }
}
