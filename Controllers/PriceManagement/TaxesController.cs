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
        readonly IRandomizer _randomizer = new Randomizer();

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

        // GET api/<TaxesController>/5
        [HttpGet("{id}")]
        public Tax Get(int id)
        {
            return _randomizer.GenerateRandomData<Tax>(id);
        }

        // POST api/<TaxesController>
        [HttpPost]
        public void Post([FromBody] Tax value)
        {
        }

        // PUT api/<TaxesController>/5
        [HttpPut]
        public void Put([FromBody] Tax value)
        {
        }

        // DELETE api/<TaxesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}