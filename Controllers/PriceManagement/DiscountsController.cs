using Microsoft.AspNetCore.Mvc;
using PSP_Komanda32_API.Models;
using PSP_Komanda32_API.Services;
using PSP_Komanda32_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
//using PSP_Komanda32_API.DTOs;
using PSP_Komanda32_API.Services.Database;

namespace PSP_Komanda32_API.Controllers.PriceManagement
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Manage prices")]
    public class DiscountsController : ControllerBase
    {
        private readonly PoSSContext _context;

        public DiscountsController(PoSSContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all discounts from Discount table
        /// </summary>
        /// <returns>list of discounts</returns>
        /// <response code="200">Returns the list of discounts</response>
        // GET: api/<DiscountsController>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Discount>))]
        public async Task<ActionResult> GetAll()
        {
            var discounts = await _context.Discount.ToListAsync();
            return Ok(discounts);
        }

        /// <summary>
        /// Gets specific discount from Discount table
        /// </summary>
        /// <param name="id">id of discount</param>
        /// <returns>one discount by id</returns>
        /// <response code="200">Returns found item</response>
        /// <response code="404">If the item is null</response>
        // GET api/<DiscountsController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Discount))]
        public async Task<ActionResult> Get(int id)
        {
            var discount = await _context.Discount
                .FirstOrDefaultAsync(o => o.id == id);
            if (discount == null)
            {
                return NotFound();
            }
            return Ok(discount);
        }

        /// <summary>
        /// Posts specific discount to Discount table
        /// </summary>
        /// <param name="value">new created discount</param>
        /// <returns>one discount by id</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="404">If the item is null</response>
        // POST api/<DiscountsController>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Discount))]
        public async Task<ActionResult> Post([FromBody] Discount value)
        {
            var createdBy = await _context.BusinessManager.FindAsync(value.CreatedBy);
            if (createdBy == null)
            {
                return NotFound("Business manager does not exist");
            }

            var discount = new Discount
            {
                Credit = value.Credit,
                CreatedBy = value.CreatedBy,
                LoaltyCost = value.LoaltyCost
            };

            await _context.Discount.AddAsync(discount);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = discount.id }, value);
        }

        /// <summary>
        /// Updates specific discount by id from Discount table
        /// </summary>
        /// <param name="id">discount id</param>
        /// <param name="value">changed discount</param>
        /// <returns>one discount by id</returns>
        /// <response code="204">if the change is successful</response>
        /// <response code="404">if bad request</response>
        // PUT api/<DiscountsController>/5
        [HttpPut]
        [ProducesResponseType(204)]
        public async Task<ActionResult> Put(int id, [FromBody] Discount value)
        {
            var current = await _context.Discount
                .FirstOrDefaultAsync(o => o.id == id);
            if (current == null)
            {
                return NotFound("Item does not exist");
            }

            if (value.CreatedBy != current.CreatedBy)
            {
                var employee = await _context.BusinessManager.FindAsync(value.CreatedBy);
                if (employee == null)
                {
                    return NotFound("Business manager does not exist");
                }
            }

            var discount = new Discount
            {
                Credit = value.Credit,
                CreatedBy = value.CreatedBy,
                LoaltyCost = value.LoaltyCost
            };

            _context.Entry(current).CurrentValues.SetValues(discount);
            _context.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Deletes specific discount by id from Discount table
        /// </summary>
        /// <param name="id">discount id</param>
        /// <response code="204">if delete is successful</response>
        // DELETE api/<DiscountsController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<ActionResult> Delete(int id)
        {
            var discount = await _context.Discount.FindAsync(id);
            if (discount == null)
            {
                return NotFound();
            }
            _context.Discount.Remove(discount);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}