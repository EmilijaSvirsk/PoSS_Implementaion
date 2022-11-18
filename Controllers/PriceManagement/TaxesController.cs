using Microsoft.AspNetCore.Mvc;
using PSP_Komanda32_API.Models;
using PSP_Komanda32_API.Services;
using PSP_Komanda32_API.Services.Interfaces;

namespace PSP_Komanda32_API.Controllers.PriceManagement
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Manage prices")]
    public class TaxesController : ControllerBase
    {
        readonly IRandomizer _randomizer;

        public TaxesController(IRandomizer randomizer)
        {
            _randomizer = randomizer;
        }

        // GET: api/<TaxesController>
        [HttpGet]
        public IEnumerable<Tax> GetAll()
        {
            var list = new List<Tax>();
            var index = 50;

            for (int i = 0; i < index; i++)
            {
                list.Add(_randomizer.GenerateRandomData<Tax>());
            }

            return list;
        }

        // GET api/<TaxesController>/5
        [HttpGet("{id}")]
        public ActionResult<Tax> Get(int id)
        {
            var value = _randomizer.GenerateRandomData<Tax>(id);

            if (value == null)
            {
                return NotFound();
            }

            return value;
        }

        // POST api/<TaxesController>
        [HttpPost]
        public ActionResult<Tax> Post([FromBody] Tax value)
        {
            if (value != null)
                return CreatedAtAction("Get", new { id = value.Id }, value);

            return new StatusCodeResult(StatusCodes.Status404NotFound);
        }

        // PUT api/<TaxesController>/5
        [HttpPut]
        public ActionResult<Tax> Put(int id, [FromBody] Tax value)
        {
            if (id != value.Id)
            {
                return BadRequest();
            }

            return NoContent();
        }

        // DELETE api/<TaxesController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok();
        }
    }
}