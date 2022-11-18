using Microsoft.AspNetCore.Mvc;
using PSP_Komanda32_API.Models;
using PSP_Komanda32_API.Services;
using PSP_Komanda32_API.Services.Interfaces;

namespace PSP_Komanda32_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Manage product services")]
    public class ProductServicesController : ControllerBase
    {
        readonly IRandomizer _randomizer;

        public ProductServicesController(IRandomizer randomizer)
        {
            _randomizer = randomizer;
        }

        // GET: api/<ProductServicesController>
        [HttpGet]
        public IEnumerable<ProductService> GetAll()
        {
            var list = new List<ProductService>();
            var index = 50;

            for (int i = 0; i < index; i++)
            {
                list.Add(_randomizer.GenerateRandomData<ProductService>());
            }

            return list;
        }

        // GET api/<ProductServicesController>/5
        [HttpGet("{id}")]
        public ActionResult<ProductService> Get(int id)
        {

            var value = _randomizer.GenerateRandomData<ProductService>(id);

            if (value == null)
            {
                return NotFound();
            }

            return value;
        }

        // POST api/<ProductServicesController>
        [HttpPost]
        public ActionResult<ProductService> Post([FromBody] ProductService value)
        {
            if (value != null)
                return CreatedAtAction("Get", new { id = value.id }, value);

            return new StatusCodeResult(StatusCodes.Status404NotFound);
        }

        // PUT api/<ProductServicesController>/5
        [HttpPut]
        public ActionResult<ProductService> Put(int id, [FromBody] ProductService value)
        {

            if (id != value.id)
            {
                return BadRequest();
            }

            return NoContent();
        }

        // DELETE api/<ProductServicesController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok();
        }
    }
}