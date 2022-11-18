using Microsoft.AspNetCore.Mvc;
using PSP_Komanda32_API.Models;
using PSP_Komanda32_API.Services;
using PSP_Komanda32_API.Services.Interfaces;

namespace PSP_Komanda32_API.Controllers.EmployeeManagement
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Manage employees")]
    public class CouriersController : ControllerBase
    {
        readonly IRandomizer _randomizer;

        public CouriersController(IRandomizer randomizer)
        {
            _randomizer = randomizer;
        }

        /// <summary>
        /// Gets all data from the couriers table
        /// </summary>
        /// <returns>list of orders</returns>
        // GET: api/<CouriersController>
        [HttpGet]
        public IEnumerable<Courier> GetAll()
        {
            var list = new List<Courier>();
            var index = 50;

            for (int i = 0; i < index; i++)
            {
                list.Add(_randomizer.GenerateRandomData<Courier>());
            }

            return list;
        }

        /// <summary>
        /// Gets specific courier by id from the couriers table
        /// </summary>
        /// <param name="id">id of courier</param>
        /// <returns>one courier by id</returns>
        /// <response code="201">Returns found item</response>
        /// <response code="404">If the item is null</response>
        // GET api/<CouriersController>/5
        [HttpGet("{id}")]
        public ActionResult<Courier> Get(int id)
        {
            var value = _randomizer.GenerateRandomData<Courier>(id);

            if (value == null)
            {
                return NotFound();
            }

            return value;
        }

        /// <summary>
        /// Posts specific courier by id to the couriers table
        /// </summary>
        /// <param name="value">new created courier</param>
        /// <returns>one courier by id</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="404">If the item is null</response>
        // POST api/<CouriersController>
        [HttpPost]
        public ActionResult<Courier> Post([FromBody] Courier value)
        {
            if (value != null)
                return CreatedAtAction("Get", new { id = value.id }, value);

            return new StatusCodeResult(StatusCodes.Status404NotFound);
        }

        /// <summary>
        /// Update specific courier by id to the couriers table
        /// </summary>
        /// <param name="id">courier id</param>
        /// <param name="value">changed courier</param>
        /// <returns>one courier by id</returns>
        /// <response code="204">if the change is successful</response>
        /// <response code="404">if bad request</response>
        // PUT api/<CouriersController>/5
        [HttpPut]
        public ActionResult<Courier> Put(int id, [FromBody] Courier value)
        {
            if (id != value.id)
            {
                return BadRequest();
            }

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
            return Ok();
        }
    }
}