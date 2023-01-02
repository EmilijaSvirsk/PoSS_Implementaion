using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PSP_Komanda32_API.DTOs;
using PSP_Komanda32_API.Models;
using PSP_Komanda32_API.Services.Database;

namespace PSP_Komanda32_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Manage shifts")]
    public class ShiftsController : ControllerBase
    {
        private PoSSContext _context;

        public ShiftsController(PoSSContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all shifts from Shift table
        /// </summary>
        /// <returns>list of discounts</returns>
        // GET: api/<ShiftsController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            return Ok(await _context.Shifts.ToListAsync());
        }

        /// <summary>
        /// Gets specific shift from Shift table
        /// </summary>
        /// <param name="id">id of shift</param>
        /// <returns>one shift by id</returns>
        /// <response code="201">Returns found item</response>
        /// <response code="404">If the item is null</response>
        // GET api/<ShiftsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var shift = await _context.Shifts.SingleOrDefaultAsync(shift => shift.id == id);
            return shift == null ? NotFound() : Ok(shift);
        }

        /// <summary>
        /// Posts specific shift to Shift table
        /// </summary>
        /// <param name="shift">new created shift</param>
        /// <returns>one shift by id</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="404">If the item is null</response>
        // POST api/<ShiftsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateShiftDTO shift)
        {
            if (!_context.Employees.Any(employee => employee.id == shift.EmployeeId)) return BadRequest($"Employee {shift.EmployeeId} does not exist.");
            if (!_context.BusinessManagers.Any(bm => bm.id == shift.CreatedBy)) return BadRequest($"Business manager {shift.CreatedBy} does not exist.");

            var newShift = new Shift
            {
                EmployeeId = shift.EmployeeId,
                Start = shift.Start,
                End = shift.End,
                Location = shift.Location,
                Desription = shift.Desription,
                CheckedIn = shift.CheckedIn,
                CheckedOut = shift.CheckedOut,
                CreatedBy = shift.CreatedBy,
                EmergencyOut = shift.EmergencyOut
            };

            _context.Shifts.Add(newShift);
            await _context.SaveChangesAsync();
            return Ok(newShift);
        }

        /// <summary>
        /// Updates specific shift by id from Shift table
        /// </summary>
        /// <param name="id">shift id</param>
        /// <param name="shift">changed shift</param>
        /// <returns>one shift by id</returns>
        /// <response code="204">if the change is successful</response>
        /// <response code="404">if bad request</response>
        // PUT api/<ShiftsController>/5
        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromBody] Shift shift)
        {
            if (id != shift.EmployeeId)
            {
                return BadRequest("Ids don't match.");
            }

            var oldShift = await _context.Shifts.SingleOrDefaultAsync(s => s.id == shift.id);
            if (oldShift == null) return BadRequest($"Shift {id} does not exist.");

            _context.Entry(oldShift).CurrentValues.SetValues(shift);
            await _context.SaveChangesAsync();

            return Ok(shift);
        }

        /// <summary>
        /// Deletes specific shift by id from Shift table
        /// </summary>
        /// <param name="id">Shift id</param>
        /// <response code="204">if delete is successful</response>
        // DELETE api/<ShiftsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var shift = await _context.Shifts.FirstOrDefaultAsync(s => s.id == id);
            if (shift == null) throw new ArgumentException($"Shift {id} doesn't exist");
            _context.Shifts.Remove(shift);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}