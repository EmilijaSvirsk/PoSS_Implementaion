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

        // POST api/<EmployeesController>
        [HttpPost]
        public ActionResult<Employee> Post([FromBody] Employee value)
        {
            if (value != null)
                return CreatedAtAction("Get", new { id = value.id }, value);

            return new StatusCodeResult(StatusCodes.Status404NotFound);
        }

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

        // DELETE api/<EmployeesController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok();
        }
    }
}
