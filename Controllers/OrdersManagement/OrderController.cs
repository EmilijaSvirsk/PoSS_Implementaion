using Microsoft.AspNetCore.Mvc;
using PSP_Komanda32_API.Models;
using PSP_Komanda32_API.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PSP_Komanda32_API.Controllers.OrdersManagement
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Manage orders")]
    public class OrderController : ControllerBase
    {
        readonly IRandomizer _randomizer;

        public OrderController(IRandomizer randomizer)
        {
            _randomizer = randomizer;
        }

        /// <summary>
        /// Gets all orders from table order
        /// </summary>
        /// <returns>list of orders</returns>
        // GET: api/<OrderController>
        [HttpGet]
        public IEnumerable<Order> GetAll()
        {
            var list = new List<Order>();
            var index = 50;

            for (int i = 0; i < index; i++)
            {
                list.Add(_randomizer.GenerateRandomData<Order>());
            }

            return list;
        }

        /// <summary>
        /// Gets specific order by id from table order
        /// </summary>
        /// <param name="id">id of order</param>
        /// <returns>one order by id</returns>
        /// <response code="201">Returns found item</response>
        /// <response code="404">If the item is null</response>
        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public ActionResult<Order> Get(int id)
        {
            var value = _randomizer.GenerateRandomData<Order>(id);

            if (value == null)
            {
                return NotFound();
            }

            return value;
        }

        /// <summary>
        /// Posts specific order by id to table order
        /// </summary>
        /// <param name="value">new created order</param>
        /// <returns>one order by id</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="404">If the item is null</response>
        // POST api/<OrdersController>
        [HttpPost]
        public ActionResult<Order> Post([FromBody] Order value)
        {
            if (value != null)
                return CreatedAtAction("Get", new { id = value.id }, value);

            return new StatusCodeResult(StatusCodes.Status404NotFound);
        }

        /// <summary>
        /// Update specific order by id to table order
        /// </summary>
        /// <param name="id">orders id</param>
        /// <param name="value">changed order</param>
        /// <returns>one order by id</returns>
        /// <response code="204">if the change is successful</response>
        /// <response code="404">if bad request</response>
        // PUT api/<OrdersController>/5
        [HttpPut]
        public ActionResult<Order> Put(int id, [FromBody] Order value)
        {
            if (id != value.id)
            {
                return BadRequest();
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes specific order by id from table order
        /// </summary>
        /// <param name="id">order id</param>
        /// <response code="204">if delete is successful</response>
        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok();
        }
    }
}
