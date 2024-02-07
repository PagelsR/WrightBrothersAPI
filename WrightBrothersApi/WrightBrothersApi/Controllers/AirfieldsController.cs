using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
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
            new Airfield("Simms Station", "Dayton, Ohio", "1904-1905", "First circular flight"),
            new Airfield("Kill Devil Hills", "North Carolina", "1900-1903", "First powered flight")
        };

        [HttpGet]
        public ActionResult<IEnumerable<Airfield>> Get()
        {
            return _airfields;
        }

        [HttpGet("{name}")]
        public ActionResult<Airfield> Get(string name)
        {
            var airfield = _airfields.FirstOrDefault(a => a.Name == name);
            if (airfield == null)
            {
                return NotFound();
            }

            return airfield;
        }

        [HttpPost]
        public ActionResult Post(Airfield airfield)
        {
            _airfields.Add(airfield);
            return CreatedAtAction(nameof(Get), new { name = airfield.Name }, airfield);
        }

        [HttpPut("{name}")]
        public ActionResult Put(string name, Airfield updatedAirfield)
        {
            var airfield = _airfields.FirstOrDefault(a => a.Name == name);
            if (airfield == null)
            {
                return NotFound();
            }

            airfield.Location = updatedAirfield.Location;
            airfield.DatesOfUse = updatedAirfield.DatesOfUse;
            airfield.Significance = updatedAirfield.Significance;

            return NoContent();
        }

        [HttpDelete("{name}")]
        public ActionResult Delete(string name)
        {
            var airfield = _airfields.FirstOrDefault(a => a.Name == name);
            if (airfield == null)
            {
                return NotFound();
            }

            _airfields.Remove(airfield);

            return NoContent();
        }
    }
}