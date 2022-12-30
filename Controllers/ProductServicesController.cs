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
        [HttpGet("GetAllByBusiness/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductService>))]
        public async Task<ActionResult> GetAll(int businessId)
        {
            var businessPlace = await _context.BusinessPlaces.FindAsync(businessId);
            if (businessPlace == null)
            {
                return NotFound();
            }
            var productServices = await _context.ProductServices
                .Where(x => x.BusinessId == businessId).ToListAsync();
            return Ok(productServices);
        }

        /// <summary>
        /// Gets specific product service from ProductService table
        /// </summary>
        /// <param name="id">id of product service</param>
        /// <returns>one tax by product service</returns>
        /// <response code="200">Returns found item</response>
        /// <response code="404">If item does not exist</response>
        // GET api/<ProductServicesController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductService))]
        public async Task<ActionResult> Get(int id)
        {
            var productService = await _context.ProductServices.FindAsync(id);
            if (productService == null)
            {
                return NotFound();
            }
            return Ok(productService);
        }

        /// <summary>
        /// Posts specific product service to ProductService table
        /// </summary>
        /// <param name="value">new created product service</param>
        /// <returns>one product service by id</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If item is invalid</response>
        /// <response code="404">If business does not exist</response>
        // POST api/<ProductServicesController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProductService))]
        public async Task<ActionResult> Post([FromBody] ProductService value)
        {
            if (await _context.BusinessPlaces.FindAsync(value.BusinessId) == null)
            {
                return NotFound("Business does not exist");
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
        /// <response code="400">if item is invalid</response>
        /// <response code="404">if item does not exist</response>
        // PUT api/<ProductServicesController>/5
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Put(int id, [FromBody] ProductService value)
        {
            var current = await _context.ProductServices.FindAsync(id);
            if (current == null)
            {
                return NotFound();
            }
            if (value.BusinessId != current.BusinessId)
            {
                return BadRequest("BusinessId cannot be changed");
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
        /// <response code="404">if item does not exist</response>
        // DELETE api/<ProductServicesController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete(int id)
        {
            var productService = await _context.ProductServices.FindAsync(id);
            if (productService == null)
            {
                return NotFound();
            }
            _context.ProductServices.Remove(productService);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}