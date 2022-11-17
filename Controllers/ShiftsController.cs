using Microsoft.AspNetCore.Mvc;
using PSP_Komanda32_API.Models;
using PSP_Komanda32_API.Services;
using PSP_Komanda32_API.Services.Interfaces;

namespace PSP_Komanda32_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftsController : ControllerBase
    {
        readonly IRandomizer _randomizer = new Randomizer();

        // GET: api/<ShiftsController>
        [HttpGet]
        public IEnumerable<Shift> GetAll()
        {
            var list = new List<Shift>();
            var index = 50;

            for (int i = 0; i < index; i++)
            {
                list.Add(_randomizer.GenerateRandomData<Shift>());
            }

            return list;
        }

        // GET api/<ShiftsController>/5
        [HttpGet("{id}")]
        public Shift Get(int id)
        {
            return _randomizer.GenerateRandomData<Shift>(id);
        }

        // POST api/<ShiftsController>
        [HttpPost]
        public void Post([FromBody] Shift value)
        {
        }

        // PUT api/<ShiftsController>/5
        [HttpPut]
        public void Put([FromBody] Shift value)
        {
        }

        // DELETE api/<ShiftsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}