using Microsoft.AspNetCore.Mvc;
using PSP_Komanda32_API.Models;
using PSP_Komanda32_API.Services;
using PSP_Komanda32_API.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PSP_Komanda32_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        readonly IRandomizer _randomizer;

        public OrdersController(IRandomizer randomizer)
        {
            _randomizer = randomizer;
        }


        // GET: api/<OrdersController>
        [HttpGet]
        public IEnumerable<Orders> GetAll()
        {
            var list = new List<Orders>();
            var index = 50;

            for (int i = 0; i < index; i++)
            {
                list.Add(_randomizer.GenerateRandomData<Orders>());
            }

            return list;
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public ActionResult<Orders> Get(int id)
        {
            var value = _randomizer.GenerateRandomData<Orders>(id);

            if (value == null)
            {
                return NotFound();
            }

            return value;
        }

        // POST api/<OrdersController>
        [HttpPost]
        public ActionResult<Orders> Post([FromBody] Orders value)
        {
            if (value != null)
                return CreatedAtAction("Get", new { id = value.id }, value);

            return new StatusCodeResult(StatusCodes.Status404NotFound);
        }

        // PUT api/<OrdersController>/5
        [HttpPut]
        public ActionResult<Orders> Put(int id, [FromBody] Orders value)
        {
            if(id != value.id)
            {
                return BadRequest();
            }

            return NoContent();
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok();
        }
    }
}
