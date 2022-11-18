using Microsoft.AspNetCore.Mvc;
using PSP_Komanda32_API.Models;
using PSP_Komanda32_API.Services;
using PSP_Komanda32_API.Services.Interfaces;

namespace PSP_Komanda32_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftsController : ControllerBase
    {
        readonly IRandomizer _randomizer;

        public ShiftsController(IRandomizer randomizer)
        {
            _randomizer = randomizer;
        }

        /// <summary>
        /// Gets all shifts from Shift table
        /// </summary>
        /// <returns>list of discounts</returns>
        // GET: api/<ShiftsController>
        [HttpGet]
        public IEnumerable<Shift> GetAll()
        {
            var list = new List<Shift>();
            var index = 50;

            for (int i = 0; i < index; i++)
            {
                list.Add(_randomizer.GenerateRandomData<Shift>());
            }

            return list;
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
        public ActionResult<Shift> Get(int id)
        {
            var value = _randomizer.GenerateRandomData<Shift>(id);

            if (value == null)
            {
                return NotFound();
            }

            return value;
        }

        /// <summary>
        /// Posts specific shift to Shift table
        /// </summary>
        /// <param name="value">new created shift</param>
        /// <returns>one shift by id</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="404">If the item is null</response>
        // POST api/<ShiftsController>
        [HttpPost]
        public ActionResult<Shift> Post([FromBody] Shift value)
        {
            if (value != null)
                return CreatedAtAction("Get", new { id = value.EmployeeId }, value);

            return new StatusCodeResult(StatusCodes.Status404NotFound);
        }

        /// <summary>
        /// Updates specific shift by id from Shift table
        /// </summary>
        /// <param name="id">shift id</param>
        /// <param name="value">changed shift</param>
        /// <returns>one shift by id</returns>
        /// <response code="204">if the change is successful</response>
        /// <response code="404">if bad request</response>
        // PUT api/<ShiftsController>/5
        [HttpPut]
        public ActionResult<Shift> Put(int id, [FromBody] Shift value)
        {
            if (id != value.EmployeeId)
            {
                return BadRequest();
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes specific shift by id from Shift table
        /// </summary>
        /// <param name="id">Shift id</param>
        /// <response code="204">if delete is successful</response>
        // DELETE api/<ShiftsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok();
        }
    }
}