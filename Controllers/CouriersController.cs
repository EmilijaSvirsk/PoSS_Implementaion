using Microsoft.AspNetCore.Mvc;
using PSP_Komanda32_API.Models;
using PSP_Komanda32_API.Services;
using PSP_Komanda32_API.Services.Interfaces;

namespace PSP_Komanda32_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CouriersController : ControllerBase
    {
        readonly IRandomizer _randomizer = new Randomizer();

        // GET: api/<CouriersController>
        [HttpGet]
        public IEnumerable<Courier> GetAll()
        {
            var list = new List<Courier>();
            var index = 50;

            for (int i = 0; i < index; i++)
            {
                list.Add(_randomizer.GenerateRandomData<Courier>());
            }

            return list;
        }

        // GET api/<CouriersController>/5
        [HttpGet("{id}")]
        public Courier Get(int id)
        {
            return _randomizer.GenerateRandomData<Courier>(id);
        }

        // POST api/<CouriersController>
        [HttpPost]
        public void Post([FromBody] Courier value)
        {
        }

        // PUT api/<CouriersController>/5
        [HttpPut]
        public void Put([FromBody] Courier value)
        {
        }

        // DELETE api/<CouriersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}