using Microsoft.AspNetCore.Mvc;
using WrightBrothersApi.Models;
using System.Collections.Generic;

namespace WrightBrothersApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AirfieldsController : ControllerBase
    {
        private static readonly List<Airfield> Airfields = new List<Airfield>
        {
            new Airfield("Huffman Prairie", "Dayton, Ohio", "1904-1905", "First practical airplane flights"),
            new Airfield("Kitty Hawk", "North Carolina", "1900-1903", "First powered flight")
        };

        [HttpGet]
        public ActionResult<List<Airfield>> GetAll()
        {
            return Airfields;
        }

        [HttpGet("{name}")]
        public ActionResult<Airfield> GetByName(string name)
        {
            var airfield = Airfields.Find(a => a.Name == name);
            if (airfield == null)
            {
                return NotFound();
            }
            return airfield;
        }

        [HttpPost]
        public ActionResult<Airfield> Post(Airfield airfield)
        {
            Airfields.Add(airfield);
            return CreatedAtAction(nameof(GetByName), new { name = airfield.Name }, airfield);
        }

        [HttpPut("{name}")]
        public IActionResult Put(string name, Airfield updatedAirfield)
        {
            var airfield = Airfields.Find(a => a.Name == name);
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
        public IActionResult Delete(string name)
        {
            var airfield = Airfields.Find(a => a.Name == name);
            if (airfield == null)
            {
                return NotFound();
            }
            Airfields.Remove(airfield);
            return NoContent();
        }
    }
}