using Microsoft.AspNetCore.Mvc;
using WrightBrothersApi.Models;
using System.Collections.Generic;

namespace WrightBrothersApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirfieldsController : ControllerBase
    {
        private static List<Airfield> _airfields = new List<Airfield>
        {
            new Airfield { Id = 1, Name = "Huffman Prairie", Location = "Dayton, Ohio" },
            new Airfield { Id = 2, Name = "Kill Devil Hills", Location = "North Carolina" },
            new Airfield { Id = 3, Name = "Simms Station", Location = "Dayton, Ohio" }
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
            var airfield = _airfields.Find(a => a.Id == id);
            if (airfield == null)
            {
                return NotFound();
            }
            return airfield;
        }

        // POST: api/Airfields
        [HttpPost]
        public ActionResult<Airfield> PostAirfield(Airfield airfield)
        {
            _airfields.Add(airfield);
            return CreatedAtAction(nameof(GetAirfield), new { id = airfield.Id }, airfield);
        }

        // PUT: api/Airfields/5
        [HttpPut("{id}")]
        public IActionResult PutAirfield(int id, Airfield airfield)
        {
            if (id != airfield.Id)
            {
                return BadRequest();
            }

            var existingAirfield = _airfields.Find(a => a.Id == id);
            if (existingAirfield == null)
            {
                return NotFound();
            }

            existingAirfield.Name = airfield.Name;
            existingAirfield.Location = airfield.Location;

            return NoContent();
        }

        // DELETE: api/Airfields/5
        [HttpDelete("{id}")]
        public IActionResult DeleteAirfield(int id)
        {
            var airfield = _airfields.Find(a => a.Id == id);
            if (airfield == null)
            {
                return NotFound();
            }

            _airfields.Remove(airfield);
            return NoContent();
        }
    }
}