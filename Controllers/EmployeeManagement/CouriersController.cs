using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PSP_Komanda32_API.Models;
using PSP_Komanda32_API.Services.Database;

namespace PSP_Komanda32_API.Controllers.EmployeeManagement
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Manage employees")]
    public class CouriersController : ControllerBase
    {
        private readonly PoSSContext _context;

        public CouriersController(PoSSContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all data from the couriers table
        /// </summary>
        /// <returns>list of orders</returns>
        // GET: api/<CouriersController>
        [HttpGet]
        public async Task<List<CourierDTO>> GetAll()
        {
            var courier = await _context.Couriers.ToListAsync();

            return courier.Select(x => new CourierDTO
            {
                id = x.id,
                Name = x.Name,
                Surname = x.Surname,
                Email = x.Email,
                CreatedBy = x.CreatedBy,
                PhoneNumber = x.PhoneNumber,
                Transportation = x.Transportation
            }).ToList();
        }

        /// <summary>
        /// Gets specific courier by id from the couriers table
        /// </summary>
        /// <param name="id">id of courier</param>
        /// <returns>one courier by id</returns>
        /// <response code="200">Returns found item</response>
        /// <response code="404">If the item is null</response>
        // GET api/<CouriersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourierDTO>> Get(int id)
        {
            var courier = await _context.Couriers.FindAsync(id);

            if (courier == null)
            {
                return NotFound();
            }

            return Ok(new CourierDTO
            {
                id = courier.id,
                Name = courier.Name,
                Surname = courier.Surname,
                Email = courier.Email,
                CreatedBy = courier.CreatedBy,
                PhoneNumber = courier.PhoneNumber,
                Transportation = courier.Transportation
            });
        }

        /// <summary>
        /// Posts specific courier by id to the couriers table
        /// </summary>
        /// <param name="value">new created courier</param>
        /// <returns>one courier by id</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        // POST api/<CouriersController>
        [HttpPost]
        public async Task<ActionResult<Courier>> Post([FromBody] Courier value)
        {
            var businessAdmin = await _context.BusinessAdministrators.FindAsync(value.CreatedBy);
            await _context.Couriers.AddAsync(value);

            if (businessAdmin == null)
            {
                return BadRequest("No such business admin with the given CreatedBy value");
            }

            var response = Courier.CheckIfValid(value);

            if (!response.Equals("Ok"))
            {
                return BadRequest("Invalid body: " + response);
            }

            _context.SaveChanges();

            return Created("api/Couriers", value);
        }

        /// <summary>
        /// Update specific courier by id to the couriers table
        /// </summary>
        /// <param name="id">courier id</param>
        /// <param name="value">changed courier</param>
        /// <returns>one courier by id</returns>
        /// <response code="204">if the change is successful</response>
        /// <response code="400">if bad request</response>
        // PUT api/<CouriersController>/5
        [HttpPut]
        public async Task<ActionResult<Courier>> Put(int id, [FromBody] Courier value)
        {
            var employee = _context.Couriers.Find(id);
            var businessAdmin = await _context.BusinessAdministrators.FindAsync(value.CreatedBy);

            if (employee == null)
            {
                return BadRequest("No such courier with the given id");
            }

            if (businessAdmin == null)
            {
                return BadRequest("No such business admin with the given CreatedBy value");
            }

            value.id = id;

            var response = Courier.CheckIfValid(value);

            if (!response.Equals("Ok"))
            {
                return BadRequest("Invalid body: " + response);
            }

            _context.ChangeTracker.Clear();
            _context.Couriers.Update(value);
            _context.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Deletes specific courier by id from the couriers table
        /// </summary>
        /// <param name="id">courier id</param>
        /// <response code="204">if delete is successful</response>
        // DELETE api/<CouriersController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var employee = _context.Couriers.Find(id);

            if (employee == null)
            {
                return BadRequest("No such courier with the given id");
            }

            _context.Couriers.Remove(employee);
            _context.SaveChanges();

            return NoContent();
        }
    }
}