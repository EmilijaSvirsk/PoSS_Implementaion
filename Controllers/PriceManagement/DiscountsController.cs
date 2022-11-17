using Microsoft.AspNetCore.Mvc;
using PSP_Komanda32_API.Models;
using PSP_Komanda32_API.Services;
using PSP_Komanda32_API.Services.Interfaces;

namespace PSP_Komanda32_API.Controllers.PriceManagement
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Manage prices")]
    public class DiscountsController : ControllerBase
    {
        readonly IRandomizer _randomizer;

        public DiscountsController(IRandomizer randomizer)
        {
            _randomizer = randomizer;
        }

        // GET: api/<DiscountsController>
        [HttpGet]
        public IEnumerable<Discount> GetAll()
        {
            var list = new List<Discount>();
            var index = 50;

            for (int i = 0; i < index; i++)
            {
                list.Add(_randomizer.GenerateRandomData<Discount>());
            }

            return list;
        }

        // GET api/<DiscountsController>/5
        [HttpGet("{id}")]
        public ActionResult<Discount> Get(int id)
        {
            var value = _randomizer.GenerateRandomData<Discount>(id);

            if (value == null)
            {
                return NotFound();
            }

            return value;
        }

        // POST api/<DiscountsController>
        [HttpPost]
        public ActionResult<Discount> Post([FromBody] Discount value)
        {
            if (value != null)
                return CreatedAtAction("Get", new { id = value.id }, value);

            return new StatusCodeResult(StatusCodes.Status404NotFound);
        }

        // PUT api/<DiscountsController>/5
        [HttpPut]
        public ActionResult<Discount> Put(int id, [FromBody] Discount value)
        {
            if (id != value.id)
            {
                return BadRequest();
            }

            return NoContent();
        }

        // DELETE api/<DiscountsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok();
        }
    }
}