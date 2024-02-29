using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WrightBrothersApi.Models;

namespace WrightBrothersApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirfieldsController : ControllerBase
    {
        private static List<Airfield> _airfields = new List<Airfield>
        {
            new Airfield("Huffman Prairie", "Dayton, Ohio", "1904-1905", "First practical airplane flights"),
            new Airfield("Kill Devil Hills", "North Carolina", "1900-1903", "First powered flight")
        };

        // GET: api/Airfields
        [HttpGet]
        public ActionResult<IEnumerable<Airfield>> GetAirfields()
        {
            return _airfields;
        }

        // GET: api/Airfields/5
        [HttpGet("{id}")]
        public ActionResult<Airfield> GetAirfield(int id)
        {
            if (id < 0 || id >= _airfields.Count)
            {
                return NotFound();
            }

            return _airfields[id];
        }

        // POST: api/Airfields
        [HttpPost]
        public ActionResult<Airfield> PostAirfield(Airfield airfield)
        {
            _airfields.Add(airfield);
            return CreatedAtAction(nameof(GetAirfield), new { id = _airfields.Count - 1 }, airfield);
        }

        // PUT: api/Airfields/5
        [HttpPut("{id}")]
        public IActionResult PutAirfield(int id, Airfield airfield)
        {
            if (id < 0 || id >= _airfields.Count)
            {
                return NotFound();
            }

            _airfields[id] = airfield;
            return NoContent();
        }

        // DELETE: api/Airfields/5
        [HttpDelete("{id}")]
        public IActionResult DeleteAirfield(int id)
        {
            if (id < 0 || id >= _airfields.Count)
            {
                return NotFound();
            }

            _airfields.RemoveAt(id);
            return NoContent();
        }
    }
}