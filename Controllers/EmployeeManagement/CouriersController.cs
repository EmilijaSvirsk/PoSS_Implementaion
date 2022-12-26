using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PSP_Komanda32_API.Models;
using PSP_Komanda32_API.Services;
using PSP_Komanda32_API.Services.Database;
using PSP_Komanda32_API.Services.Interfaces;
using PSP_Komanda32_API.Services.Utils;

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

            var checkResponse = Courier.CheckIfValid(value);

            if (checkResponse == 1)
                return BadRequest("Invalid body: values of Courier cannot be null");
            else if (checkResponse == 2)
                return BadRequest("Invalid body: invalid Courier Id");
            else if (checkResponse == 3)
                return BadRequest("Invalid body: invalid Courier Name");
            else if (checkResponse == 4)
                return BadRequest("Invalid body: invalid Courier Surname");
            else if (checkResponse == 5)
                return BadRequest("Invalid body: invalid Courier Email");
            else if (checkResponse == 6)
                return BadRequest("Invalid body: invalid Courier Username");
            else if (checkResponse == 7)
                return BadRequest("Invalid body: invalid Courier Password");
            else if (checkResponse == 8)
                return BadRequest("Invalid body: invalid Courier CreatedBy");
            else if (checkResponse == 9)
                return BadRequest("Invalid body: invalid Courier PhoneNurmber");
            else if (checkResponse == 10)
                return BadRequest("Invalid body: invalid Courier Transportation");

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

            var checkResponse = Courier.CheckIfValid(value);

            if (checkResponse == 1)
                return BadRequest("Invalid body: values of Courier cannot be null");
            else if (checkResponse == 2)
                return BadRequest("Invalid body: invalid Courier Id");
            else if (checkResponse == 3)
                return BadRequest("Invalid body: invalid Courier Name");
            else if (checkResponse == 4)
                return BadRequest("Invalid body: invalid Courier Surname");
            else if (checkResponse == 5)
                return BadRequest("Invalid body: invalid Courier Email");
            else if (checkResponse == 6)
                return BadRequest("Invalid body: invalid Courier Username");
            else if (checkResponse == 7)
                return BadRequest("Invalid body: invalid Courier Password");
            else if (checkResponse == 8)
                return BadRequest("Invalid body: invalid Courier CreatedBy");
            else if (checkResponse == 9)
                return BadRequest("Invalid body: invalid Courier PhoneNurmber");
            else if (checkResponse == 10)
                return BadRequest("Invalid body: invalid Courier Transportation");

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