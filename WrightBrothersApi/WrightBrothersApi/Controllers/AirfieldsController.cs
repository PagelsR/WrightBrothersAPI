using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WrightBrothersApi.Models;

namespace WrightBrothersApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AirfieldsController : ControllerBase
    {
        private static List<Airfield> Airfields = new List<Airfield>
        {
            new Airfield("Huffman Prairie", "Dayton, Ohio", "1904-1905", "First practical airplane flights"),
            new Airfield("Kill Devil Hills", "North Carolina", "1900-1903", "First powered flight"),
            new Airfield("Simms Station", "Dayton, Ohio", "1904-1905", "Improved the design and control of the Flyer")
        };

        [HttpGet]
        public ActionResult<List<Airfield>> GetAll() => Airfields;

        [HttpGet("{name}")]
        public ActionResult<Airfield> Get(string name)
        {
            var airfield = Airfields.Find(a => a.Name == name);
            if (airfield == null)
                return NotFound();
            return airfield;
        }

        [HttpPost]
        public ActionResult<Airfield> Create(Airfield airfield)
        {
            Airfields.Add(airfield);
            return CreatedAtAction(nameof(Get), new { name = airfield.Name }, airfield);
        }

        [HttpPut("{name}")]
        public ActionResult Update(string name, Airfield updatedAirfield)
        {
            var airfield = Airfields.Find(a => a.Name == name);
            if (airfield == null)
                return NotFound();
            airfield.Location = updatedAirfield.Location;
            airfield.DatesOfUse = updatedAirfield.DatesOfUse;
            airfield.Significance = updatedAirfield.Significance;
            return NoContent();
        }

        [HttpDelete("{name}")]
        public ActionResult Delete(string name)
        {
            var airfield = Airfields.Find(a => a.Name == name);
            if (airfield == null)
                return NotFound();
            Airfields.Remove(airfield);
            return NoContent();
        }
    }
}