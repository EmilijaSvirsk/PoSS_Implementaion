using Microsoft.EntityFrameworkCore;
using PSP_Komanda32_API.Models;
using PSP_Komanda32_API.Services.Database;
using PSP_Komanda32_API.Services.Interfaces;

namespace PSP_Komanda32_API.Services
{
    public class TimeSlotService : ITimeSlotService
    {
        private PoSSContext _context;
        public TimeSlotService(PoSSContext context)
        {
            _context = context;
        }

        public async Task<List<DateTime>> GetTimeSlots(DateTime from, DateTime to, int interval)
        {
            var employeesOnShift = await _context.Shifts.Where(s => s.Start < to && s.End > from).ToListAsync();
            var reservations = await _context.Reservations.Where(r => r.Status == ReservationStatus.Awaiting).ToListAsync();

            var awailableTimes = new List<DateTime>();

            for (var timeSlot = from; timeSlot <= to; timeSlot = timeSlot.AddMinutes(interval))
            {
                var numberOfAwailableEmployees = employeesOnShift.Where(s => s.Start <= timeSlot && s.End >= timeSlot.AddMinutes(interval)).Count();
                var numberOfOngoingReservations = reservations
                    .Where(r => (r.Date.AddMinutes(r.Duration) > timeSlot && r.Date <= timeSlot) ||
                                (r.Date < timeSlot.AddMinutes(interval) && r.Date > timeSlot))
                    .Count();
                if (numberOfAwailableEmployees - numberOfOngoingReservations > 0) awailableTimes.Add(timeSlot);
            }
            return awailableTimes;
        }

        public async Task<bool> IsTimeSlotAwailable(DateTime date, int duration)
        {
            var numberOfAwailableEmployees = await _context.Shifts.Where(s => s.Start <= date && s.End >= date.AddMinutes(duration)).CountAsync();
            var numberOfOngoingReservations = await _context.Reservations
                .Where(r => r.Status == ReservationStatus.Awaiting &&
                            ((r.Date.AddMinutes(r.Duration) > date && r.Date <= date) ||
                            (r.Date < date.AddMinutes(duration) && r.Date > date)))
                .CountAsync();
            return numberOfAwailableEmployees - numberOfOngoingReservations > 0;
        }
    }
}
