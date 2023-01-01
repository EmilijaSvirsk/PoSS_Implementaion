using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PSP_Komanda32_API.Models;
using PSP_Komanda32_API.Services;
using PSP_Komanda32_API.Services.Database;
using PSP_Komanda32_API.Services.Interfaces;

namespace PSP_Komanda32_API.Controllers.PriceManagement
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Manage prices")]
    public class TaxesController : ControllerBase
    {
        private readonly PoSSContext _context;
        public TaxesController(PoSSContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all taxes from Tax table
        /// </summary>
        /// <returns>list of discounts</returns>
        /// <response code="200">Returns the list of discounts</response>
        // GET: api/<TaxesController>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Tax>))]
        public async Task<ActionResult> GetAll()
        {
            var tax = await _context.Tax.ToListAsync();
            return Ok(tax);
        }

        /// <summary>
        /// Gets specific tax from Tax table
        /// </summary>
        /// <param name="id">id of tax</param>
        /// <returns>one tax by id</returns>
        /// <response code="200">Returns found item</response>
        /// <response code="404">If the item is null</response>
        // GET api/<TaxesController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Tax))]
        public async Task<ActionResult> Get(int id)
        {
            var tax = await _context.Tax
                .FirstOrDefaultAsync(o => o.id == id);
            if (tax == null)
            {
                return NotFound();
            }
            return Ok(tax);
        }

        /// <summary>
        /// Posts specific tax to Tax table
        /// </summary>
        /// <param name="value">new created tax</param>
        /// <returns>one tax by id</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="404">If the item is null</response>
        // POST api/<TaxesController>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Tax))]
        public async Task<ActionResult> Post([FromBody] Tax value)
        {
            var tax = new Tax
            {
                Percentage = value.Percentage,
                Name = value.Name,
                Description = value.Description
            };

            await _context.Tax.AddAsync(tax);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = tax.id }, value);
        }

        /// <summary>
        /// Updates specific tax by id from Tax table
        /// </summary>
        /// <param name="id">tax id</param>
        /// <param name="value">changed tax</param>
        /// <returns>one tax by id</returns>
        /// <response code="204">if the change is successful</response>
        /// <response code="404">if bad request</response>
        // PUT api/<TaxesController>/5
        [HttpPut]
        [ProducesResponseType(204)]
        public async Task<ActionResult> Put(int id, [FromBody] Tax value)
        {
            var current = await _context.Tax
                .FirstOrDefaultAsync(o => o.id == id);
            if (current == null)
            {
                return NotFound("Item does not exist");
            }

            var tax = new Tax
            {
                Percentage = value.Percentage,
                Name = value.Name,
                Description = value.Description
            };

            _context.Entry(current).CurrentValues.SetValues(tax);
            _context.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Deletes specific tax by id from Tax table
        /// </summary>
        /// <param name="id">tax id</param>
        /// <response code="204">if delete is successful</response>
        // DELETE api/<TaxesController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<ActionResult> Delete(int id)
        {
            var tax = await _context.Tax.FindAsync(id);
            if (tax == null)
            {
                return NotFound();
            }
            _context.Tax.Remove(tax);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}