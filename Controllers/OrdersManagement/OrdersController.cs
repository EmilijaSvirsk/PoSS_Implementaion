using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PSP_Komanda32_API.Models;
using PSP_Komanda32_API.DTOs;
using PSP_Komanda32_API.Services.Database;
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
            var ordersDTO = orders.Select(o => new OrdersSummaryDTO
            {
                id = o.id,
                EmployeeId = o.EmployeeId,
                CustomerId = o.CustomerId,
                Date = o.Date,
                Payment = o.Payment,
                IsPaid = o.IsPaid,
                Comment = o.Comment,
                IsAccepted = o.IsAccepted,
                DeclineReason = o.DeclineReason,
                PriceInCents = _context
                    .Entry(o)
                    .Collection(o => o.ProductServices)
                    .Query()
                    .Sum(ps => ps.CostInCents)
            });
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
        [ProducesResponseType(200, Type = typeof(Orders))]
        public async Task<ActionResult> Get(int id)
        {
            var order = await _context.Orders
                .Include(o => o.ProductServices)
                .FirstOrDefaultAsync(o => o.id == id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
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
            var ordersDTOs = orders.Select(o => new OrdersSummaryDTO
            {
                id = o.id,
                EmployeeId = o.EmployeeId,
                CustomerId = o.CustomerId,
                Date = o.Date,
                Payment = o.Payment,
                IsPaid = o.IsPaid,
                Comment = o.Comment,
                IsAccepted = o.IsAccepted,
                DeclineReason = o.DeclineReason,
                PriceInCents = _context
                    .Entry(o)
                    .Collection(o => o.ProductServices)
                    .Query()
                    .Sum(ps => ps.CostInCents)
            });
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
            var ordersDTOs = orders.Select(o => new OrdersSummaryDTO
            {
                id = o.id,
                EmployeeId = o.EmployeeId,
                CustomerId = o.CustomerId,
                Date = o.Date,
                Payment = o.Payment,
                IsPaid = o.IsPaid,
                Comment = o.Comment,
                IsAccepted = o.IsAccepted,
                DeclineReason = o.DeclineReason,
                PriceInCents = _context
                    .Entry(o)
                    .Collection(o => o.ProductServices)
                    .Query()
                    .Sum(ps => ps.CostInCents)
            });
            return Ok(ordersDTOs);
        }

        /// <summary>
        /// Posts specific order by id to table orders
        /// </summary>
        /// <param name="value">new created order</param>
        /// <returns>one order by id</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is incorrect</response>
        /// <response code="404">If customer or employee do not exist</response>
        // POST api/<OrdersController>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(CreateOrdersDTO))]
        public async Task<ActionResult> Post([FromBody] CreateOrdersDTO value)
        {
            var employee = await _context.Employees.FindAsync(value.EmployeeId);
            if (employee == null)
            {
                return NotFound("Employee does not exist");
            }
            var businessAdministrator = await _context.BusinessAdministrators.FindAsync(employee.CreatedBy);
            if (businessAdministrator == null)
            {
                return StatusCode(500, "Employee has no associated business");
            }
            if (await _context.Customers.FindAsync(value.CustomerId) == null)
            {
                return NotFound("Customer does not exist");
            }
            var productServices = await _context.ProductServices.Where(ps => value.ProductServiceIds.Contains(ps.id)).ToListAsync();
            if (productServices.Count != value.ProductServiceIds.Count)
            {
                return NotFound("Product or service does not exist");
            }
            if (!productServices.All(ps => ps.BusinessId == businessAdministrator.BusinessId))
            {
                return NotFound("Product or service does not belong to this business");
            }
            var address = await _context.Addresses.FindAsync(value.DeliveryAddressId);
            if (address == null)
            {
                return NotFound("Address does not exist");
            }
            if (address.CustomerId != value.CustomerId)
            {
                return NotFound("Address does not belong to this customer");
            }

            var orders = new Orders
            {
                EmployeeId = value.EmployeeId,
                CustomerId = value.CustomerId,
                Date = value.Date,
                Payment = value.Payment,
                IsPaid = value.IsPaid,
                Comment = value.Comment,
                IsAccepted = value.IsAccepted,
                DeclineReason = value.DeclineReason,
                DeliveryAddressId = value.DeliveryAddressId,
                ProductServices = productServices
            };
            await _context.Orders.AddAsync(orders);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = orders.id }, value);
        }

        /// <summary>
        /// Update specific order by id to table orders
        /// </summary>
        /// <param name="id">orders id</param>
        /// <param name="value">changed order</param>
        /// <returns>one order by id</returns>
        /// <response code="204">if the change is successful</response>
        /// <response code="404">If item, customer or employee do not exist</response>
        // PUT api/<OrdersController>/5
        [HttpPut]
        [ProducesResponseType(204)]
        public async Task<ActionResult> Put(int id, [FromBody] CreateOrdersDTO value)
        {
            var current = await _context.Orders
                .Include("ProductServices")
                .FirstOrDefaultAsync(o => o.id == id);
            if (current == null)
            {
                return NotFound("Item does not exist");
            }
            if (value.EmployeeId != current.EmployeeId)
            {
                var employee = await _context.Employees.FindAsync(value.EmployeeId);
                if (employee == null)
                {
                    return NotFound("Employee does not exist");
                }
                var businessAdministrator = await _context.BusinessAdministrators.FindAsync(employee.CreatedBy);
                if (businessAdministrator == null)
                {
                    return StatusCode(500, "Employee has no associated business");
                }
            }
            if (value.CustomerId != current.CustomerId)
            {
                return BadRequest("Customer cannot be changed");
            }
            if (value.DeliveryAddressId != current.DeliveryAddressId)
            {
                var address = await _context.Addresses.FindAsync(value.DeliveryAddressId);
                if (address == null)
                {
                    return NotFound("Address does not exist");
                }
                if (address.CustomerId != value.CustomerId)
                {
                    return NotFound("Address does not belong to this customer");
                }
            }
            var productServices = await _context.ProductServices.Where(ps => value.ProductServiceIds.Contains(ps.id)).ToListAsync();
            if (productServices.Count != value.ProductServiceIds.Count)
            {
                return NotFound("Product or service does not exist");
            }
            var orders = new Orders
            {
                id = id,
                EmployeeId = value.EmployeeId,
                CustomerId = value.CustomerId,
                Date = value.Date,
                Payment = value.Payment,
                IsPaid = value.IsPaid,
                Comment = value.Comment,
                IsAccepted = value.IsAccepted,
                DeclineReason = value.DeclineReason,
                DeliveryAddressId = value.DeliveryAddressId,
            };
            current.ProductServices = productServices;
            _context.Entry(current).CurrentValues.SetValues(orders);
            _context.SaveChanges();
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
