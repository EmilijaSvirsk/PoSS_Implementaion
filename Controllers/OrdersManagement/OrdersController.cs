using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PSP_Komanda32_API.Models;
using PSP_Komanda32_API.DTOs;
using PSP_Komanda32_API.Services.Database;
using PSP_Komanda32_API.Extensions;

namespace PSP_Komanda32_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Manage orders")]
    public class OrdersController : ControllerBase
    {
        private readonly PoSSContext _context;

        public OrdersController(PoSSContext context)
        {
            _context = context;
        }

        private IQueryable<OrderProducts> GetOrderProducts(Orders orders)
        {
            return _context
                .Entry(orders)
                .Collection(o => o.OrderProducts)
                .Query();
        }

        /// <summary>
        /// Gets all data from table orders
        /// </summary>
        /// <returns>list of orders</returns>
        /// <response code="200">Returns the list of orders</response>
        // GET: api/<OrdersController>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OrdersSummaryDTO>))]
        public async Task<ActionResult> GetAll()
        {
            var orders = await _context.Orders.ToListAsync();
            var ordersDTO = orders.Select(o => o.ToOrdersSummaryDTO(GetOrderProducts(o)));
            return Ok(ordersDTO);
        }

        /// <summary>
        /// Gets specific order by id from table orders
        /// </summary>
        /// <param name="id">id of order</param>
        /// <returns>one order by id</returns>
        /// <response code="200">Returns found item</response>
        /// <response code="404">If the item is null</response>
        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(GetOrdersDTO))]
        public async Task<ActionResult> Get(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.id == id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order.ToGetOrdersDTO(GetOrderProducts(order)));
        }

        /// <summary>
        /// Get orders by employeeId
        /// </summary>
        /// <param name="id">id of employee</param>
        /// <returns>list of orders based on employee id</returns>
        /// <response code="200">Returns found item</response>
        /// <response code="404">If employee does not exist</response>
        // GET api/<OrdersController>/GetByEmployee/5
        [HttpGet]
        [Route("GetByEmployee/{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Orders>))]
        public async Task<ActionResult> GetByEmployeeId(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            var orders = await _context.Orders.Where(o => o.EmployeeId == id).ToListAsync();
            var ordersDTOs = orders.Select(o => o.ToOrdersSummaryDTO(GetOrderProducts(o)));
            return Ok(ordersDTOs);
        }

        /// <summary>
        /// Get orders by customer id
        /// </summary>
        /// <param name="id">id of customer id</param>
        /// <returns>list of orders based on customer id</returns>
        /// <response code="200">Returns found item</response>
        /// <response code="404">If customer does not exist</response>
        // GET api/<OrdersController>/GetByCustomer/5
        [HttpGet]
        [Route("GetByCustomer/{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OrdersSummaryDTO>))]
        public async Task<ActionResult> GetByCustomerId(int id)
        {
            var client = await _context.Customers.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            var orders = await _context.Orders.Where(o => o.CustomerId == id).ToListAsync();
            var ordersDTOs = orders.Select(o => o.ToOrdersSummaryDTO(GetOrderProducts(o)));
            return Ok(ordersDTOs);
        }

        private async Task<string?> CheckIsAddressValid(int addressId, int customerId)
        {
            var address = await _context.Addresses.FindAsync(addressId);
            if (address == null)
            {
                return "Address does not exist";
            }
            if (address.CustomerId != customerId)
            {
                return "Address does not belong to customer";
            }
            return null;
        }

        private async Task<string?> CheckIsBusinessValid(int employeeId, List<ProductService> productServices)
        {
            var employee = await _context.Employees.FindAsync(employeeId);
            if (employee == null)
            {
                return "Employee does not exist";
            }
            var businessAdministrator = await _context.BusinessAdministrators.FindAsync(employee.CreatedBy);
            if (businessAdministrator == null)
            {
                throw new Exception("Employee has no associated business");
            }
            if (!productServices.All(ps => ps.BusinessId == businessAdministrator.BusinessId))
            {
                return "Product or service does not belong to this business";
            }
            return null;
        }

        /// <summary>
        /// Posts specific order by id to table orders
        /// </summary>
        /// <param name="value">new created order</param>
        /// <returns>one order by id</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is incorrect</response>
        // POST api/<OrdersController>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(GetOrdersDTO))]
        public async Task<ActionResult> Post([FromBody] CreateOrdersDTO value)
        {
            if (await _context.Customers.FindAsync(value.CustomerId) == null)
            {
                return BadRequest("Customer does not exist");
            }
            var productServices = await _context.ProductServices.Where(ps => value.ProductServiceIds.Contains(ps.id)).ToListAsync();
            if (productServices.Count != value.ProductServiceIds.Count)
            {
                return BadRequest("Product or service does not exist");
            }
            try
            {
                var businessError = await CheckIsBusinessValid(value.EmployeeId, productServices);
                if (businessError != null)
                {
                    return BadRequest(businessError);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
            var addressError = await CheckIsAddressValid(value.DeliveryAddressId, value.CustomerId);
            if (addressError != null)
            {
                return BadRequest(addressError);
            }
            var orders = value.ToOrders(productServices);
            await _context.Orders.AddAsync(orders);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = orders.id }, orders.ToGetOrdersDTO(productServices));
        }

        /// <summary>
        /// Update specific order by id to table orders
        /// </summary>
        /// <param name="id">orders id</param>
        /// <param name="value">changed order</param>
        /// <returns>one order by id</returns>
        /// <response code="204">if the change is successful</response>
        /// <response code="400">If the item is incorrect</response>
        /// <response code="404">If item does not exist</response>
        // PUT api/<OrdersController>/5
        [HttpPut]
        [ProducesResponseType(204)]
        public async Task<ActionResult> Put(int id, [FromBody] CreateOrdersDTO value)
        {
            var current = await _context.Orders
                .Include("OrderProducts")
                .FirstOrDefaultAsync(o => o.id == id);
            if (current == null)
            {
                return NotFound("Item does not exist");
            }
            var productServices = await _context.ProductServices
                .Where(ps => !ps.isDeleted && value.ProductServiceIds.Contains(ps.id)).ToListAsync();
            if (productServices.Count != value.ProductServiceIds.Count)
            {
                return BadRequest("One or more product or service do not exist");
            }
            if (value.CustomerId != current.CustomerId)
            {
                return BadRequest("Customer cannot be changed");
            }
            try
            {
                var businessError = await CheckIsBusinessValid(value.EmployeeId, productServices);
                if (businessError != null)
                {
                    return BadRequest(businessError);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
            if (value.DeliveryAddressId != current.DeliveryAddressId)
            {
                var addressError = await CheckIsAddressValid(value.DeliveryAddressId, value.CustomerId);
                if (addressError != null)
                {
                    return BadRequest(addressError);
                }
            }
            var orders = value.ToOrders();
            orders.id = id;
            current.OrderProducts = productServices.Select(ps => ps.ToOrderProducts()).ToList();

            _context.Entry(current).CurrentValues.SetValues(orders);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Deletes specific order by id from table orders
        /// </summary>
        /// <param name="id">order id</param>
        /// <response code="204">if delete is successful</response>
        /// <response code="404">if item does not exist</response>
        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<ActionResult> Delete(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
