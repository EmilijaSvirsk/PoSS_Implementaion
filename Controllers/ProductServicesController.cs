using Microsoft.AspNetCore.Mvc;
using PSP_Komanda32_API.Models;
using PSP_Komanda32_API.Services;
using PSP_Komanda32_API.Services.Interfaces;

namespace PSP_Komanda32_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Manage product services")]
    public class ProductServicesController : ControllerBase
    {
        readonly IRandomizer _randomizer = new Randomizer();

        // GET: api/<ProductServicesController>
        [HttpGet]
        public IEnumerable<ProductService> GetAll()
        {
            var list = new List<ProductService>();
            var index = 50;

            for (int i = 0; i < index; i++)
            {
                list.Add(_randomizer.GenerateRandomData<ProductService>());
            }

            return list;
        }

        // GET api/<ProductServicesController>/5
        [HttpGet("{id}")]
        public ProductService Get(int id)
        {
            return _randomizer.GenerateRandomData<ProductService>(id);
        }

        // POST api/<ProductServicesController>
        [HttpPost]
        public void Post([FromBody] ProductService value)
        {
        }

        // PUT api/<ProductServicesController>/5
        [HttpPut]
        public void Put([FromBody] ProductService value)
        {
        }

        // DELETE api/<ProductServicesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}