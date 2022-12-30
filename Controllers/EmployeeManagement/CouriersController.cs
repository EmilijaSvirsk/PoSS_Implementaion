using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PSP_Komanda32_API.Models;
using PSP_Komanda32_API.Services;
using PSP_Komanda32_API.Services.Database;
using PSP_Komanda32_API.Services.Interfaces;

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
        public async Task<List<Courier>> GetAll()
        {
            return await _context.Couriers.ToListAsync();
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
        public async Task<ActionResult<Courier>> Get(int id)
        {
            var courier = await _context.Couriers.FindAsync(id);

            if (courier == null)
            {
                return NotFound();
            }

            return Ok(courier);
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
        public ActionResult<Courier> Post([FromBody] Courier value)
        {
            _context.Couriers.AddAsync(value);

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
        public ActionResult<Courier> Put(int id, [FromBody] Courier value)
        {
            var employee = _context.Couriers.Find(id);

            if (employee == null)
            {
                return BadRequest("No such courier with the given id");
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