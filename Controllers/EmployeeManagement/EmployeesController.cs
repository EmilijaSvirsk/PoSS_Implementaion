using Microsoft.AspNetCore.Mvc;
using PSP_Komanda32_API.Models;
using PSP_Komanda32_API.Services;
using PSP_Komanda32_API.Services.Interfaces;

namespace PSP_Komanda32_API.Controllers.EmployeeManagement
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Manage employees")]
    public class EmployeesController : ControllerBase
    {
        readonly IRandomizer _randomizer;

        public EmployeesController(IRandomizer randomizer)
        {
            _randomizer = randomizer;
        }

        /// <summary>
        /// Gets all data from the employees table
        /// </summary>
        /// <returns>list of employees</returns>
        // GET: api/<EmployeesController>
        [HttpGet]
        public IEnumerable<Employee> GetAll()
        {
            var list = new List<Employee>();
            var index = 50;

            for (int i = 0; i < index; i++)
            {
                list.Add(_randomizer.GenerateRandomData<Employee>());
            }

            return list;
        }

        /// <summary>
        /// Gets specific employee by id from the employees table
        /// </summary>
        /// <param name="id">id of employee</param>
        /// <returns>one employee by id</returns>
        /// <response code="201">Returns found item</response>
        /// <response code="404">If the item is null</response>
        // GET api/<EmployeesController>/5
        [HttpGet("{id}")]
        public ActionResult<Employee> Get(int id)
        {
            var value = _randomizer.GenerateRandomData<Employee>(id);

            if (value == null)
            {
                return NotFound();
            }

            return value;
        }

        /// <summary>
        /// Posts specific employee by id to the employees table
        /// </summary>
        /// <param name="value">new created employee</param>
        /// <returns>one employee by id</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="404">If the item is null</response>
        // POST api/<EmployeesController>
        [HttpPost]
        public ActionResult<Employee> Post([FromBody] Employee value)
        {
            if (value != null)
                return CreatedAtAction("Get", new { id = value.id }, value);

            return new StatusCodeResult(StatusCodes.Status404NotFound);
        }

        /// <summary>
        /// Update specific employee by id to the employees table
        /// </summary>
        /// <param name="id">employee id</param>
        /// <param name="value">changed employee</param>
        /// <returns>one employee by id</returns>
        /// <response code="204">if the change is successful</response>
        /// <response code="404">if bad request</response>
        // PUT api/<EmployeesController>/5
        [HttpPut]
        public ActionResult<Employee> Put(int id, [FromBody] Employee value)
        {
            if (id != value.id)
            {
                return BadRequest();
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes specific employee by id from the employees table
        /// </summary>
        /// <param name="id">employee id</param>
        /// <response code="204">if delete is successful</response>
        // DELETE api/<EmployeesController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok();
        }
    }
}
