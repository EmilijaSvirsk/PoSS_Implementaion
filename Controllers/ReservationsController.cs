using Microsoft.AspNetCore.Mvc;
using PSP_Komanda32_API.Models;
using PSP_Komanda32_API.Services;
using PSP_Komanda32_API.Services.Interfaces;

namespace PSP_Komanda32_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Manage reservations")]
    public class ReservationsController : ControllerBase
    {
        readonly IRandomizer _randomizer = new Randomizer();

        // GET: api/<ReservationsController>
        [HttpGet]
        public IEnumerable<Reservation> GetAll()
        {
            var list = new List<Reservation>();
            var index = 50;

            for (int i = 0; i < index; i++)
            {
                list.Add(_randomizer.GenerateRandomData<Reservation>());
            }

            return list;
        }

        // GET api/<ReservationsController>/5
        [HttpGet("{id}")]
        public Reservation Get(int id)
        {
            return _randomizer.GenerateRandomData<Reservation>(id);
        }

        // POST api/<ReservationsController>
        [HttpPost]
        public void Post([FromBody] Reservation value)
        {
        }

        // PUT api/<ReservationsController>/5
        [HttpPut]
        public void Put([FromBody] Reservation value)
        {
        }

        // DELETE api/<ReservationsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}