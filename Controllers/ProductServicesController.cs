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

        /// <summary>
        /// Gets all product services from ProductService table
        /// </summary>
        /// <returns>list of product services</returns>
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

        /// <summary>
        /// Gets specific product service from ProductService table
        /// </summary>
        /// <param name="id">id of product service</param>
        /// <returns>one tax by product service</returns>
        /// <response code="201">Returns found item</response>
        /// <response code="404">If the item is null</response>
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

        /// <summary>
        /// Posts specific product service to ProductService table
        /// </summary>
        /// <param name="value">new created product service</param>
        /// <returns>one product service by id</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="404">If the item is null</response>
        // POST api/<ProductServicesController>
        [HttpPost]
        public ActionResult<ProductService> Post([FromBody] ProductService value)
        {
            if (value != null)
                return CreatedAtAction("Get", new { id = value.id }, value);

            return new StatusCodeResult(StatusCodes.Status404NotFound);
        }

        /// <summary>
        /// Updates specific product service by id from ProductService table
        /// </summary>
        /// <param name="id">product service id</param>
        /// <param name="value">changed product service</param>
        /// <returns>one product service by id</returns>
        /// <response code="204">if the change is successful</response>
        /// <response code="404">if bad request</response>
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

        /// <summary>
        /// Deletes specific product service by id from ProductService table
        /// </summary>
        /// <param name="id">product service id</param>
        /// <response code="204">if delete is successful</response>
        // DELETE api/<ProductServicesController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok();
        }
    }
}