using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PSP_Komanda32_API.Models;
using PSP_Komanda32_API.Services.Database;

namespace PSP_Komanda32_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Manage product services")]
    public class ProductServicesController : ControllerBase
    {
        private readonly PoSSContext _context;
        public ProductServicesController(PoSSContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all product services of a business from ProductService table
        /// </summary>
        /// <param name="businessId">id of business</param>
        /// <returns>list of product services</returns>
        /// <response code="200">Returns found item</response>
        /// <response code="404">If business does not exist</response>
        // GET: api/<ProductServicesController>/5
        [HttpGet("GetAllByBusiness/{businessId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductService>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetAll(int businessId)
        {
            var businessPlace = await _context.BusinessPlaces.FindAsync(businessId);
            if (businessPlace == null) {
                return NotFound();
            }
            var productServices = await _context.ProductServices.Where(x => x.BusinessId == businessId).ToListAsync();
            return Ok(productServices);
        }

        /// <summary>
        /// Gets specific product service from ProductService table
        /// </summary>
        /// <param name="id">id of product service</param>
        /// <returns>one tax by product service</returns>
        /// <response code="200">Returns found item</response>
        /// <response code="404">If the item is null</response>
        // GET api/<ProductServicesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductService>> Get(int id)
        {
            var productService = await _context.ProductServices.FindAsync(id);
            if (productService != null)
                return Ok(productService);
            return NotFound();
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
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProductService))]
        public async Task<ActionResult> Post([FromBody] ProductService value)
        {
            if (value == null)
            {
                return BadRequest();
            }

            await _context.ProductServices.AddAsync(value);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = value.id }, value);
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
        public async Task<ActionResult> Put(int id, [FromBody] ProductService value)
        {
            if (value == null)
            {
                return BadRequest();
            }
            var current = await _context.ProductServices.FindAsync(id);
            if (current == null)
            {
                return NotFound();
            }
            value.id = id;
            _context.Entry(current).CurrentValues.SetValues(value);
            _context.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Deletes specific product service by id from ProductService table
        /// </summary>
        /// <param name="id">product service id</param>
        /// <response code="204">if delete is successful</response>
        // DELETE api/<ProductServicesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var productService = await _context.ProductServices.FindAsync(id);
            if (productService == null)
            {
                return NotFound();
            }
            _context.ProductServices.Remove(productService);
            _context.SaveChanges();
            return NoContent();
        }
    }
}