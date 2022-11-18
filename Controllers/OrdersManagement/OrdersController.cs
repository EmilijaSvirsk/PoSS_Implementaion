using Microsoft.AspNetCore.Mvc;
using PSP_Komanda32_API.Models;
using PSP_Komanda32_API.Services;
using PSP_Komanda32_API.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PSP_Komanda32_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Manage orders")]
    public class OrdersController : ControllerBase
    {
        readonly IRandomizer _randomizer;

        public OrdersController(IRandomizer randomizer)
        {
            _randomizer = randomizer;
        }

        /// <summary>
        /// Gets all data from table orders
        /// </summary>
        /// <returns>list of orders</returns>
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

        /// <summary>
        /// Gets specific order by id from table orders
        /// </summary>
        /// <param name="id">id of order</param>
        /// <returns>one order by id</returns>
        /// <response code="201">Returns found item</response>
        /// <response code="404">If the item is null</response>
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

        /// <summary>
        /// Get orders by employeeId
        /// </summary>
        /// <param name="id">id of employee</param>
        /// <returns>list of orders based on employee id</returns>
        // GET api/<OrdersController>/GetByEmployee/5
        [HttpGet]
        [Route("GetByEmployee/{id}")]
        public IEnumerable<Orders> GetByEmployeeId(int id)
        {
            var list = new List<Orders>();
            var index = 10;

            for (int i = 0; i < index; i++)
            {
                list.Add(_randomizer.GenerateRandomData<Orders>());
                list.Last().EmployeeId = id;
            }

            return list;
        }

        /// <summary>
        /// Get orders by customer id
        /// </summary>
        /// <param name="id">id of customer id</param>
        /// <returns>list of orders based on customer id</returns>
        // GET api/<OrdersController>/GetByCustomer/5
        [HttpGet]
        [Route("GetByCustomer/{id}")]
        public IEnumerable<Orders> GetByClientId(int id)
        {
            var list = new List<Orders>();
            var index = 10;

            for (int i = 0; i < index; i++)
            {
                list.Add(_randomizer.GenerateRandomData<Orders>());
                list.Last().CustomerId = id;
            }

            return list;
        }


        /// <summary>
        /// Posts specific order by id to table orders
        /// </summary>
        /// <param name="value">new created order</param>
        /// <returns>one order by id</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="404">If the item is null</response>
        // POST api/<OrdersController>
        [HttpPost]
        public ActionResult<Orders> Post([FromBody] Orders value)
        {
            if (value != null)
                return CreatedAtAction("Get", new { id = value.id }, value);

            return new StatusCodeResult(StatusCodes.Status404NotFound);
        }

        /// <summary>
        /// Update specific order by id to table orders
        /// </summary>
        /// <param name="id">orders id</param>
        /// <param name="value">changed order</param>
        /// <returns>one order by id</returns>
        /// <response code="204">if the change is successful</response>
        /// <response code="404">if bad request</response>
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

        /// <summary>
        /// Deletes specific order by id from table orders
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
