namespace PSP_Komanda32_API.Models
{
    public class Address
    {
        public int id { get; set; }
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public int HouseNr { get; set; }
        public int FlatNr { get; set; }
        public int CustomerId { get; set; }

    }
}
