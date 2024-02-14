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
            new Airfield
            {
                Id = 1,
                Name = "Kill Devil Hills",
                Location = "North Carolina, USA"
            },
            new Airfield
            {
                Id = 2,
                Name = "Huffman Prairie",
                Location = "Ohio, USA"
            },
            new Airfield
            {
                Id = 3,
                Name = "Simms Station",
                Location = "Ohio, USA"
            },
            // Add more airfields here
        };

        [HttpGet]
        public ActionResult<List<Airfield>> GetAll()
        {
            return Ok(Airfields);
        }

        [HttpGet("{id}")]
        public ActionResult<Airfield> GetById(int id)
        {
            var airfield = Airfields.Find(a => a.Id == id);

            if (airfield == null)
            {
                return NotFound();
            }

            return Ok(airfield);
        }

        [HttpPost]
        public ActionResult<Airfield> Post(Airfield airfield)
        {
            airfield.Id = Airfields.Count + 1;
            Airfields.Add(airfield);
            return CreatedAtAction(nameof(GetById), new { id = airfield.Id }, airfield);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Airfield airfield)
        {
            var existingAirfield = Airfields.Find(a => a.Id == id);

            if (existingAirfield == null)
            {
                return NotFound();
            }

            existingAirfield.Name = airfield.Name;
            existingAirfield.Location = airfield.Location;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var airfield = Airfields.Find(a => a.Id == id);

            if (airfield == null)
            {
                return NotFound();
            }

            Airfields.Remove(airfield);

            return NoContent();
        }
    }
}