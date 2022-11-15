namespace PSP_Komanda32_API.Models
{
    public enum OrderStatus
    {
        NonResponded,
        Received,
        InProcess,
        ProcessLate,
        ProcessDone,
        Delivering,
        DeliveringLate,
        DeliveringDone,
        NotTaken
    }
}
