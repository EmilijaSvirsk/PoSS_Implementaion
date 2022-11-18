using Microsoft.AspNetCore.Mvc;
using PSP_Komanda32_API.Models;
using PSP_Komanda32_API.Services;
using PSP_Komanda32_API.Services.Interfaces;

namespace PSP_Komanda32_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Manage orders")]
    public class ReservationsController : ControllerBase
    {
        readonly IRandomizer _randomizer;

        public ReservationsController(IRandomizer randomizer)
        {
            _randomizer = randomizer;
        }

        /// <summary>
        /// Gets all data from the reservations table
        /// </summary>
        /// <returns>list of reservations</returns>
        // GET: api/<ReservationsController>
        [HttpGet]
        public IEnumerable<Reservation> GetAll()
        {
            var list = new List<Reservation>();
            var index = 50;

            for (int i = 0; i < index; i++)
            {
                list.Add(_randomizer.GenerateRandomData<Reservation>());
            }

            return list;
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
        public ActionResult<Reservation> Get(int id)
        {
            var value = _randomizer.GenerateRandomData<Reservation>(id);

            if (value == null)
            {
                return NotFound();
            }

            return value;
        }

        /// <summary>
        /// Posts specific order by id to table orders
        /// </summary>
        /// <param name="value">new created reservation</param>
        /// <returns>one reservation by id</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="404">If the item is null</response>
        // POST api/<ReservationsController>
        [HttpPost]
        public ActionResult<Reservation> Post([FromBody] Reservation value)
        {
            if (value != null)
                return CreatedAtAction("Get", new { id = value.id }, value);

            return new StatusCodeResult(StatusCodes.Status404NotFound);
        }

        /// <summary>
        /// Update specific reservation by id to the reservations table
        /// </summary>
        /// <param name="id">reservation id</param>
        /// <param name="value">changed reservation</param>
        /// <returns>one reservation by id</returns>
        /// <response code="204">if the change is successful</response>
        /// <response code="404">if bad request</response>
        // PUT api/<ReservationsController>/5
        [HttpPut]
        public ActionResult<Reservation> Put(int id, [FromBody] Reservation value)
        {
            if (id != value.id)
            {
                return BadRequest();
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes specific reservation by id from the reservations table
        /// </summary>
        /// <param name="id">reservation id</param>
        /// <response code="204">if delete is successful</response>
        // DELETE api/<ReservationsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok();
        }
    }
}