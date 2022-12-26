using Microsoft.AspNetCore.Mvc;
using PSP_Komanda32_API.Models;
using PSP_Komanda32_API.Services;
using PSP_Komanda32_API.Services.Interfaces;
using PSP_Komanda32_API.Services.Database;
using Microsoft.EntityFrameworkCore;
using PSP_Komanda32_API.Services.Utils;

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
        public async Task<List<Employee>> GetAll()
        {
            return await _context.Employees.ToListAsync();
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
        public async Task<ActionResult<Employee>> Get(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
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
        public ActionResult<Employee> Post([FromBody] Employee value)
        {
            _context.Employees.AddAsync(value);

            var checkResponse = Employee.CheckIfValid(value);

            if (checkResponse == 1)
                return BadRequest("Invalid body: values of Employee cannot be null");
            else if (checkResponse == 2)
                return BadRequest("Invalid body: invalid Employee Id");
            else if (checkResponse == 3)
                return BadRequest("Invalid body: invalid Employee Name");
            else if (checkResponse == 4)
                return BadRequest("Invalid body: invalid Employee Surname");
            else if (checkResponse == 5)
                return BadRequest("Invalid body: invalid Employee Email");
            else if (checkResponse == 6)
                return BadRequest("Invalid body: invalid Employee Username");
            else if (checkResponse == 7)
                return BadRequest("Invalid body: invalid Employee Password");
            else if (checkResponse == 8)
                return BadRequest("Invalid body: invalid Employee CreatedBy");

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
        public ActionResult<Employee> Put(int id, [FromBody] Employee value)
        {
            var employee = _context.Employees.Find(id);

            if (employee == null)
            {
                return BadRequest("No such employee with the given id");
            }

            value.id = id;

            var checkResponse = Employee.CheckIfValid(value);

            if (checkResponse == 1)
                return BadRequest("Invalid body: values of Employee cannot be null");
            else if (checkResponse == 2)
                return BadRequest("Invalid body: invalid Employee Id");
            else if (checkResponse == 3)
                return BadRequest("Invalid body: invalid Employee Name");
            else if (checkResponse == 4)
                return BadRequest("Invalid body: invalid Employee Surname");
            else if (checkResponse == 5)
                return BadRequest("Invalid body: invalid Employee Email");
            else if (checkResponse == 6)
                return BadRequest("Invalid body: invalid Employee Username");
            else if (checkResponse == 7)
                return BadRequest("Invalid body: invalid Employee Password");
            else if (checkResponse == 8)
                return BadRequest("Invalid body: invalid Employee CreatedBy");

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
