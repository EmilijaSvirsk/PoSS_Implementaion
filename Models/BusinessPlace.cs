namespace PSP_Komanda32_API.Models
{
    public class BusinessPlace
    {
        public int id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNr { get; set; } = string.Empty;
        public int CreatedBy { get; set; }
    }
}
