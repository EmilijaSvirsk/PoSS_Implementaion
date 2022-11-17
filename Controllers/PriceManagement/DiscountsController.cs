using Microsoft.AspNetCore.Mvc;
using PSP_Komanda32_API.Models;
using PSP_Komanda32_API.Services;
using PSP_Komanda32_API.Services.Interfaces;

namespace PSP_Komanda32_API.Controllers.PriceManagement
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Manage prices")]
    public class DiscountsController : ControllerBase
    {
        readonly IRandomizer _randomizer = new Randomizer();

        // GET: api/<DiscountsController>
        [HttpGet]
        public IEnumerable<Discount> GetAll()
        {
            var list = new List<Discount>();
            var index = 50;

            for (int i = 0; i < index; i++)
            {
                list.Add(_randomizer.GenerateRandomData<Discount>());
            }

            return list;
        }

        // GET api/<DiscountsController>/5
        [HttpGet("{id}")]
        public Discount Get(int id)
        {
            return _randomizer.GenerateRandomData<Discount>(id);
        }

        // POST api/<DiscountsController>
        [HttpPost]
        public void Post([FromBody] Discount value)
        {
        }

        // PUT api/<DiscountsController>/5
        [HttpPut]
        public void Put([FromBody] Discount value)
        {
        }

        // DELETE api/<DiscountsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}