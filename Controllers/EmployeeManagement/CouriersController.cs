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

        // POST api/<CouriersController>
        [HttpPost]
        public ActionResult<Courier> Post([FromBody] Courier value)
        {
            if (value != null)
                return CreatedAtAction("Get", new { id = value.id }, value);

            return new StatusCodeResult(StatusCodes.Status404NotFound);
        }

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

        // DELETE api/<CouriersController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok();
        }
    }
}