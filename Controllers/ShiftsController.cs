using Microsoft.AspNetCore.Mvc;
using PSP_Komanda32_API.Models;
using PSP_Komanda32_API.Services;
using PSP_Komanda32_API.Services.Interfaces;

namespace PSP_Komanda32_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Manage shifts")]
    public class ShiftsController : ControllerBase
    {
        readonly IRandomizer _randomizer;

        public ShiftsController(IRandomizer randomizer)
        {
            _randomizer = randomizer;
        }

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

        // POST api/<ShiftsController>
        [HttpPost]
        public ActionResult<Shift> Post([FromBody] Shift value)
        {
            if (value != null)
                return CreatedAtAction("Get", new { id = value.EmployeeId }, value);

            return new StatusCodeResult(StatusCodes.Status404NotFound);
        }

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

        // DELETE api/<ShiftsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok();
        }
    }
}