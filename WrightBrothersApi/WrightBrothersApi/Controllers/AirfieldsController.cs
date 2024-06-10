// WrightBrothersApi/WrightBrothersApi/Controllers/AirfieldsController.cs
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
            new Airfield("Kitty Hawk", "Kitty Hawk, North Carolina, USA", "1900-1903", "First successful powered flight"),
            new Airfield("Huffman Prairie", "Dayton, Ohio, USA", "1904-1905", "Development of the first practical airplane"),
            new Airfield("Le Mans", "Le Mans, France", "1908", "First public flight demonstrations")
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
        public ActionResult Put(string name, Airfield updatedAirfield)
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