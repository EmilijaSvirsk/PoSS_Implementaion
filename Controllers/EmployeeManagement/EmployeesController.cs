using Microsoft.AspNetCore.Mvc;
using PSP_Komanda32_API.Models;
using PSP_Komanda32_API.Services.Database;
using Microsoft.EntityFrameworkCore;

namespace PSP_Komanda32_API.Controllers.EmployeeManagement
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Manage employees")]
    public class EmployeesController : ControllerBase
    {

        private readonly PoSSContext _context;

        public EmployeesController(PoSSContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all data from the employees table
        /// </summary>
        /// <returns>list of employees</returns>
        // GET: api/<EmployeesController>
        [HttpGet]
        public async Task<List<EmployeeDTO>> GetAll()
        {
            var employee = await _context.Employees.ToListAsync();

            return employee.Select(x => new EmployeeDTO
            {
                id = x.id,
                Name = x.Name,
                Surname = x.Surname,
                Email = x.Email,
                CreatedBy = x.CreatedBy
            }).ToList();
        }

        /// <summary>
        /// Gets specific employee by id from the employees table
        /// </summary>
        /// <param name="id">id of employee</param>
        /// <returns>one employee by id</returns>
        /// <response code="200">Returns found item</response>
        /// <response code="404">If the item is null</response>
        // GET api/<EmployeesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDTO>> Get(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(new EmployeeDTO
            {
                id = employee.id,
                Name = employee.Name,
                Surname = employee.Surname,
                Email = employee.Email,
                CreatedBy = employee.CreatedBy
            });
        }

        /// <summary>
        /// Posts specific employee by id to the employees table
        /// </summary>
        /// <param name="value">new created employee</param>
        /// <returns>one employee by id</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        // POST api/<EmployeesController>
        [HttpPost]
        public async Task<ActionResult<Employee>> Post([FromBody] Employee value)
        {
            var businessAdmin = await _context.BusinessAdministrators.FindAsync(value.CreatedBy);
            await _context.Employees.AddAsync(value);

            if (businessAdmin == null)
            {
                return BadRequest("No such business admin with the given CreatedBy value");
            }

            var response = Employee.CheckIfValid(value);

            if (!response.Equals("Ok"))
            {
                return BadRequest("Invalid body: " + response);
            }

            _context.SaveChanges();

            return Created("api/Employees", value);
        }

        /// <summary>
        /// Update specific employee by id to the employees table
        /// </summary>
        /// <param name="id">employee id</param>
        /// <param name="value">changed employee</param>
        /// <returns>one employee by id</returns>
        /// <response code="204">if the change is successful</response>
        /// <response code="400">if bad request</response>
        // PUT api/<EmployeesController>/5
        [HttpPut]
        public async Task<ActionResult<Employee>> Put(int id, [FromBody] Employee value)
        {
            var employee = await _context.Employees.FindAsync(id);
            var businessAdmin = await _context.BusinessAdministrators.FindAsync(value.CreatedBy);

            if (employee == null)
            {
                return BadRequest("No such employee with the given id");
            }

            if (businessAdmin == null)
            {
                return BadRequest("No such business admin with the given CreatedBy value");
            }

            value.id = id;

            var response = Employee.CheckIfValid(value);

            if (!response.Equals("Ok"))
            {
                return BadRequest("Invalid body: " + response);
            }

            _context.ChangeTracker.Clear();
            _context.Employees.Update(value);
            _context.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Deletes specific employee by id from the employees table
        /// </summary>
        /// 
        /// <param name="id">employee id</param>
        /// <response code="204">if delete is successful</response>
        // DELETE api/<EmployeesController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var employee = _context.Employees.Find(id);

            if (employee == null)
            {
                return BadRequest("No such employee with the given id");
            }

            _context.Employees.Remove(employee);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
