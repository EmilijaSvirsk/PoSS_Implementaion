using Microsoft.AspNetCore.Mvc;
using PSP_Komanda32_API.Models;
using PSP_Komanda32_API.Services;
using PSP_Komanda32_API.Services.Interfaces;

namespace PSP_Komanda32_API.Controllers.PriceManagement
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Manage prices")]
    public class DiscountsController : ControllerBase
    {
        readonly IRandomizer _randomizer;

        public DiscountsController(IRandomizer randomizer)
        {
            _randomizer = randomizer;
        }

        /// <summary>
        /// Gets all discounts from Discount table
        /// </summary>
        /// <returns>list of discounts</returns>
        // GET: api/<DiscountsController>
        [HttpGet]
        public IEnumerable<Discount> GetAll()
        {
            var list = new List<Discount>();
            var index = 50;

            for (int i = 0; i < index; i++)
            {
                list.Add(_randomizer.GenerateRandomData<Discount>());
            }

            return list;
        }

        /// <summary>
        /// Gets specific discount from Discount table
        /// </summary>
        /// <param name="id">id of discount</param>
        /// <returns>one discount by id</returns>
        /// <response code="201">Returns found item</response>
        /// <response code="404">If the item is null</response>
        // GET api/<DiscountsController>/5
        [HttpGet("{id}")]
        public ActionResult<Discount> Get(int id)
        {
            var value = _randomizer.GenerateRandomData<Discount>(id);

            if (value == null)
            {
                return NotFound();
            }

            return value;
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
        public ActionResult<Discount> Post([FromBody] Discount value)
        {
            if (value != null)
                return CreatedAtAction("Get", new { id = value.id }, value);

            return new StatusCodeResult(StatusCodes.Status404NotFound);
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
        public ActionResult<Discount> Put(int id, [FromBody] Discount value)
        {
            if (id != value.id)
            {
                return BadRequest();
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes specific discount by id from Discount table
        /// </summary>
        /// <param name="id">discount id</param>
        /// <response code="204">if delete is successful</response>
        // DELETE api/<DiscountsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok();
        }
    }
}