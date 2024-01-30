using Microsoft.AspNetCore.Mvc;
using WrightBrothersApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace WrightBrothersApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AirfieldsController : ControllerBase
    {
        private static List<Airfield> Airfields = new List<Airfield>
        {
            new Airfield("Huffman Prairie", "Dayton, Ohio", "1904-1905", "First practical airplane flights"),
            new Airfield("Simms Station", "Dayton, Ohio", "1904-1905", "First circular flight"),
            new Airfield("Kill Devil Hills", "Kitty Hawk, North Carolina", "1900-1903", "First powered flight"),
            new Airfield("Pau-Orthez", "France", "1908-1909", "First public passenger flights")
        };

        [HttpGet]
        public ActionResult<List<Airfield>> GetAll()
        {
            return Airfields;
        }

        [HttpGet("{name}")]
        public ActionResult<Airfield> GetByName(string name)
        {
            var airfield = Airfields.FirstOrDefault(a => a.Name == name);
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
        public ActionResult Put(string name, Airfield updatedAirfield)
        {
            var airfield = Airfields.FirstOrDefault(a => a.Name == name);
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
            var airfield = Airfields.FirstOrDefault(a => a.Name == name);
            if (airfield == null)
            {
                return NotFound();
            }
            Airfields.Remove(airfield);
            return NoContent();
        }
    }
}