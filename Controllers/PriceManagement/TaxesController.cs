using Microsoft.AspNetCore.Mvc;
using PSP_Komanda32_API.Models;
using PSP_Komanda32_API.Services;
using PSP_Komanda32_API.Services.Interfaces;

namespace PSP_Komanda32_API.Controllers.PriceManagement
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Manage prices")]
    public class TaxesController : ControllerBase
    {
        readonly IRandomizer _randomizer;

        public TaxesController(IRandomizer randomizer)
        {
            _randomizer = randomizer;
        }

        /// <summary>
        /// Gets all taxes from Tax table
        /// </summary>
        /// <returns>list of discounts</returns>
        // GET: api/<TaxesController>
        [HttpGet]
        public IEnumerable<Tax> GetAll()
        {
            var list = new List<Tax>();
            var index = 50;

            for (int i = 0; i < index; i++)
            {
                list.Add(_randomizer.GenerateRandomData<Tax>());
            }

            return list;
        }

        /// <summary>
        /// Gets specific tax from Tax table
        /// </summary>
        /// <param name="id">id of tax</param>
        /// <returns>one tax by id</returns>
        /// <response code="201">Returns found item</response>
        /// <response code="404">If the item is null</response>
        // GET api/<TaxesController>/5
        [HttpGet("{id}")]
        public ActionResult<Tax> Get(int id)
        {
            var value = _randomizer.GenerateRandomData<Tax>(id);

            if (value == null)
            {
                return NotFound();
            }

            return value;
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
        public ActionResult<Tax> Post([FromBody] Tax value)
        {
            if (value != null)
                return CreatedAtAction("Get", new { id = value.id }, value);

            return new StatusCodeResult(StatusCodes.Status404NotFound);
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
        public ActionResult<Tax> Put(int id, [FromBody] Tax value)
        {
            if (id != value.id)
            {
                return BadRequest();
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes specific tax by id from Tax table
        /// </summary>
        /// <param name="id">tax id</param>
        /// <response code="204">if delete is successful</response>
        // DELETE api/<TaxesController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok();
        }
    }
}