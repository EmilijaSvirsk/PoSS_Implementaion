using Microsoft.AspNetCore.Mvc;
using PSP_Komanda32_API.Models;
using PSP_Komanda32_API.Services;
using PSP_Komanda32_API.Services.Interfaces;

namespace PSP_Komanda32_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        readonly IRandomizer _randomizer = new Randomizer();

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
        public Employee Get(int id)
        {
            return _randomizer.GenerateRandomData<Employee>(id);
        }

        // POST api/<EmployeesController>
        [HttpPost]
        public void Post([FromBody] Employee value)
        {
        }

        // PUT api/<EmployeesController>/5
        [HttpPut]
        public void Put([FromBody] Employee value)
        {
        }

        // DELETE api/<EmployeesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
