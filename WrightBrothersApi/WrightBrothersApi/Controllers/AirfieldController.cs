using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WrightBrothersApi.Models;

namespace WrightBrothersApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AirfieldsController : ControllerBase
    {
        private static List<Airfield> airfields = new List<Airfield>
        {
            new Airfield("Huffman Prairie", "Dayton, Ohio", "1904-1905", "First practical airplane flights"),
            new Airfield("Kill Devil Hills", "North Carolina", "1900-1903", "First powered flight"),
            new Airfield("Simms Station", "Dayton, Ohio", "1904-1905", "Improved control system")
        };

        [HttpGet]
        public IEnumerable<Airfield> Get()
        {
            return airfields;
        }

        [HttpGet("{id}")]
        public Airfield Get(int id)
        {
            return airfields[id];
        }

        [HttpPost]
        public void Post([FromBody] Airfield airfield)
        {
            airfields.Add(airfield);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Airfield airfield)
        {
            airfields[id] = airfield;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            airfields.RemoveAt(id);
        }
    }
}