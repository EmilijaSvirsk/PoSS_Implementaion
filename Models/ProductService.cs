namespace PSP_Komanda32_API.Models
{
    public class ProductService
    {
        public int id { get; set; }
        public string Name { get; set; } = string.Empty;    
        public string Description { get; set; } = string.Empty;
        public decimal Cost { get; set; }
        public int BusinessId { get; set; }
        //public enum Category { get; set; }

    }
}
