using Microsoft.AspNetCore.Mvc;
using PSP_Komanda32_API.Models;
using PSP_Komanda32_API.Services;
using PSP_Komanda32_API.Services.Interfaces;

namespace PSP_Komanda32_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Manage reservations")]
    public class ReservationsController : ControllerBase
    {
        readonly IRandomizer _randomizer;

        public ReservationsController(IRandomizer randomizer)
        {
            _randomizer = randomizer;
        }

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

        // POST api/<ReservationsController>
        [HttpPost]
        public ActionResult<Reservation> Post([FromBody] Reservation value)
        {
            if (value != null)
                return CreatedAtAction("Get", new { id = value.id }, value);

            return new StatusCodeResult(StatusCodes.Status404NotFound);
        }

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

        // DELETE api/<ReservationsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok();
        }
    }
}