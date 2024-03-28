using Microsoft.AspNetCore.Mvc;
using WrightBrothersApi.Models;
using System.Collections.Generic;

namespace WrightBrothersApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AirfieldsController : ControllerBase
    {
        private static List<Airfield> _airfields = new List<Airfield>
        {
            new Airfield("Huffman Prairie", "Dayton, Ohio", "1904-1905", "First practical airplane flights"),
            new Airfield("Simms Station", "Dayton, Ohio", "1904-1905", "Further development and flights"),
            new Airfield("Kill Devil Hills", "North Carolina", "1900-1903", "First powered flight")
        };

        [HttpGet]
        public ActionResult<List<Airfield>> GetAll() => _airfields;

        [HttpGet("{name}")]
        public ActionResult<Airfield> Get(string name)
        {
            var airfield = _airfields.Find(a => a.Name == name);
            if (airfield == null)
                return NotFound();
            return airfield;
        }

        [HttpPost]
        public ActionResult<Airfield> Create(Airfield airfield)
        {
            _airfields.Add(airfield);
            return CreatedAtAction(nameof(Get), new { name = airfield.Name }, airfield);
        }

        [HttpPut("{name}")]
        public ActionResult Update(string name, Airfield updatedAirfield)
        {
            var airfield = _airfields.Find(a => a.Name == name);
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
            var airfield = _airfields.Find(a => a.Name == name);
            if (airfield == null)
                return NotFound();
            _airfields.Remove(airfield);
            return NoContent();
        }
    }
}