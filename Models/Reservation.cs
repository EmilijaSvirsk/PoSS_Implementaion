namespace PSP_Komanda32_API.Models
{
    public class Reservation
    {
        public int id { get; set; }
        public DateTime Date { get; set; }
        public int CustomerCount { get; set; }
        public ReservationStatus Status { get; set; }   
        public bool InappropriateBehaviour { get; set; }
    }
}
