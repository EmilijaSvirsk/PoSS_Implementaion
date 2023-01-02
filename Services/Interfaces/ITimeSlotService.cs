namespace PSP_Komanda32_API.Services.Interfaces
{
    public interface ITimeSlotService
    {
        public Task<List<DateTime>> GetTimeSlots(DateTime from, DateTime to, int interval);
        public Task<bool> IsTimeSlotAwailable(DateTime date, int duration);
    }
}
