using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PSP_Komanda32_API.Models;
using PSP_Komanda32_API.Services.Database;

namespace PSP_Komanda32_API.Controllers.OrdersManagement
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Manage orders")]
    public class OrderController : ControllerBase
    {
        private readonly PoSSContext _context;
        public OrderController(PoSSContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all orders from table order
        /// </summary>
        /// <returns>list of orders</returns>
        // GET: api/<OrderController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Order>))]
        public async Task<ActionResult> GetAll()
        {
            var orders = await _context.Order.ToListAsync();
            return Ok(orders);
        }

        /// <summary>
        /// Gets specific order by id from table order
        /// </summary>
        /// <param name="id">id of order</param>
        /// <returns>one order by id</returns>
        /// <response code="200">Returns found item</response>
        /// <response code="404">If the item does not exist</response>
        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Order))]
        public async Task<ActionResult> Get(int id)
        {
            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        /// <summary>
        /// Posts specific order by id to table order
        /// </summary>
        /// <param name="value">new created order</param>
        /// <returns>one order by id</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If courier does not exist</response>
        // POST api/<OrdersController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Order))]
        public async Task<ActionResult> Post([FromBody] Order value)
        {
            if (await _context.Couriers.FindAsync(value.CourierId) == null)
            {
                return BadRequest();
            }
            await _context.Order.AddAsync(value);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = value.id }, value);
        }

        /// <summary>
        /// Update specific order by id to table order
        /// </summary>
        /// <param name="id">orders id</param>
        /// <param name="value">changed order</param>
        /// <returns>one order by id</returns>
        /// <response code="204">if the change is successful</response>
        /// <response code="400">if courier does not exist</response>
        /// <response code="404">if item does not exist</response>
        // PUT api/<OrdersController>/5
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Put(int id, [FromBody] Order value)
        {
            var current = await _context.Order.FindAsync(id);
            if (current == null)
            {
                return NotFound();
            }
            if (await _context.Couriers.FindAsync(value.CourierId) == null)
            {
                 return BadRequest("Courier does not exist");
            }
            _context.Entry(current).CurrentValues.SetValues(value);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Deletes specific order by id from table order
        /// </summary>
        /// <param name="id">order id</param>
        /// <response code="204">if delete is successful</response>
        /// <response code="404">if item does not exist</response>
        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete(int id)
        {
            var current = await _context.Order.FindAsync(id);
            if (current == null)
            {
                return NotFound();
            }
            _context.Order.Remove(current);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
