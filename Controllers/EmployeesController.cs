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

        // GET: api/<OrdersController>
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

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public Employee Get(int id)
        {
            return _randomizer.GenerateRandomData<Employee>(id);
        }

        // POST api/<OrdersController>
        [HttpPost]
        public void Post([FromBody] Employee value)
        {
        }

        // PUT api/<OrdersController>/5
        [HttpPut]
        public void Put([FromBody] Employee value)
        {
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
