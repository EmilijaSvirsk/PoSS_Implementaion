using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PSP_Komanda32_API.DTOs;
using PSP_Komanda32_API.Models;
using PSP_Komanda32_API.Services.Database;
using PSP_Komanda32_API.Services.Interfaces;

namespace PSP_Komanda32_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Manage orders")]
    public class ReservationsController : ControllerBase
    {
        private PoSSContext _context;
        private ITimeSlotService _timeSlotService;

        public ReservationsController(PoSSContext context, ITimeSlotService timeSlotService)
        {
            _context = context;
            _timeSlotService = timeSlotService;
        }

        /// <summary>
        /// Gets all data from the reservations table
        /// </summary>
        /// <returns>list of reservations</returns>
        // GET: api/<ReservationsController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Reservations.ToListAsync());
        }

        /// <summary>
        /// Gets specific reservation by id from the reservations table
        /// </summary>
        /// <param name="id">id of reservation</param>
        /// <returns>one reservation by id</returns>
        /// <response code="201">Returns found item</response>
        /// <response code="404">If the item is null</response>
        // GET api/<ReservationsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var reservation = await _context.Reservations.Where(r => r.id == id).FirstOrDefaultAsync();
            return reservation == null ? NotFound() : Ok(reservation);
        }

        /// <summary>
        /// Get awailable time slots.
        /// </summary>
        /// <param name="from">Start of time interval</param>
        /// <param name="to">End of time interval</param>
        /// <param name="interval">Splits time interval into intervals</param>
        /// <returns></returns>
        [HttpGet("timeslots")]
        public async Task<IActionResult> GetTimeSlots(DateTime from, DateTime to, int interval)
        {
            if(from > to) return BadRequest("Time interval is not valid.");

            var awailableTimes = await _timeSlotService.GetTimeSlots(from, to, interval);

            return Ok(awailableTimes);
        }

        /// <summary>
        /// Create new resrvation
        /// </summary>
        /// <param name="reservation">new created reservation</param>
        /// <returns>one reservation by id</returns>
        /// <response code="200">Returns the newly created item</response>
        // POST api/<ReservationsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateReservationDTO reservation)
        {
            if (!await _timeSlotService.IsTimeSlotAwailable(reservation.Date, reservation.Duration)) return BadRequest("Selected time is not awailable.");

            var newReservation = new Reservation
            {
                Date = reservation.Date,
                Duration = reservation.Duration,
                CustomerCount = reservation.CustomerCount,
                InappropriateBehaviour = false,
                Status = ReservationStatus.Awaiting
            };
            _context.Add(newReservation);
            await _context.SaveChangesAsync();
            return Ok(newReservation);
        }

        /// <summary>
        /// Update specific reservation by id to the reservations table
        /// </summary>
        /// <param name="id">reservation id</param>
        /// <param name="reservation">changed reservation</param>
        /// <returns>one reservation by id</returns>
        /// <response code="200">if the change is successful</response>
        /// <response code="400">if bad request</response>
        // PUT api/<ReservationsController>/5
        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromBody] Reservation reservation)
        {
            if (id != reservation.id) return BadRequest("Ids do not match.");

            var oldReservation = await _context.Reservations.FirstOrDefaultAsync(r => r.id == id);
            if (oldReservation == null) return NotFound();

            if (oldReservation.Date != reservation.Date && !await _timeSlotService.IsTimeSlotAwailable(reservation.Date, reservation.Duration))
                return BadRequest("Selected time is not awailable.");

            _context.Entry(oldReservation).CurrentValues.SetValues(reservation);
            await _context.SaveChangesAsync();
            return Ok(reservation);
        }

        /// <summary>
        /// Deletes specific reservation by id from the reservations table
        /// </summary>
        /// <param name="id">reservation id</param>
        /// <response code="204">if delete is successful</response>
        // DELETE api/<ReservationsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var reservation = await _context.Reservations.FirstOrDefaultAsync(r => r.id == id);
            if(reservation == null) return NotFound();

            _context.Remove(reservation);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}