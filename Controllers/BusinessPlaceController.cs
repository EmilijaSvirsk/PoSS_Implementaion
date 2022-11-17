using Microsoft.AspNetCore.Mvc;
using PSP_Komanda32_API.Models;
using PSP_Komanda32_API.Services.Interfaces;
using PSP_Komanda32_API.Services;

namespace PSP_Komanda32_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BusinessPlaceController : ControllerBase
    {
        readonly IRandomizer _randomizer = new Randomizer();

        // GET: api/<OrdersController>
        [HttpGet]
        public IEnumerable<BusinessPlace> GetAll()
        {
            var list = new List<BusinessPlace>();
            var index = 50;

            for (int i = 0; i < index; i++)
            {
                list.Add(_randomizer.GenerateRandomData<BusinessPlace>());
            }

            return list;
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public BusinessPlace Get(int id)
        {
            return _randomizer.GenerateRandomData<BusinessPlace>(id);
        }

        // POST api/<OrdersController>
        [HttpPost]
        public void Post([FromBody] BusinessPlace value)
        {
        }

        // PUT api/<OrdersController>/5
        [HttpPut]
        public void Put([FromBody] BusinessPlace value)
        {
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}