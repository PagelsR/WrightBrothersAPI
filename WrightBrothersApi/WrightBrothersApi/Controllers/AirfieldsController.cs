using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WrightBrothersApi.Models;

namespace WrightBrothersApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AirfieldsController : ControllerBase
    {
        private static List<Airfield> _airfields = new List<Airfield>
        {
            new Airfield("Huffman Prairie", "Dayton, Ohio", "1904-1905", "First practical airplane flights"),
            new Airfield("Kill Devil Hills", "North Carolina", "1900-1903", "First powered flight"),
            new Airfield("Simms Station", "Dayton, Ohio", "1904-1905", "Improved the design and control of the Flyer")
        };

        [HttpGet]
        public ActionResult<List<Airfield>> GetAll() => _airfields;

        [HttpGet("{id}")]
        public ActionResult<Airfield> Get(int id)
        {
            if (id < 0 || id >= _airfields.Count)
                return NotFound();
            return _airfields[id];
        }

        [HttpPost]
        public ActionResult<Airfield> Create(Airfield airfield)
        {
            _airfields.Add(airfield);
            return airfield;
        }

        [HttpPut("{id}")]
        public ActionResult<Airfield> Update(int id, Airfield airfield)
        {
            if (id < 0 || id >= _airfields.Count)
                return NotFound();
            _airfields[id] = airfield;
            return airfield;
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id < 0 || id >= _airfields.Count)
                return NotFound();
            _airfields.RemoveAt(id);
            return NoContent();
        }
    }
}