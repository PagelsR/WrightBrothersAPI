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
            new Airfield("Kitty Hawk", "North Carolina", "1900-1903", "First successful flight"),
            new Airfield("Huffman Prairie", "Ohio", "1904-1905", "First practical airplane"),
            new Airfield("Le Mans", "France", "1908", "First public flights")
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
        public ActionResult Put(string name, Airfield newAirfield)
        {
            var airfield = Airfields.Find(a => a.Name == name);
            if (airfield == null)
            {
                return NotFound();
            }
            airfield.Location = newAirfield.Location;
            airfield.DatesOfUse = newAirfield.DatesOfUse;
            airfield.Significance = newAirfield.Significance;
            return NoContent();
        }

        [HttpDelete("{name}")]
        public ActionResult Delete(string name)
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