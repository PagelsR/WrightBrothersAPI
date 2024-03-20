// WrightBrothersApi/WrightBrothersApi/Controllers/AirfieldsController.cs
using Microsoft.AspNetCore.Mvc;
using WrightBrothersApi.Models;
using System.Collections.Generic;

[ApiController]
[Route("[controller]")]
public class AirfieldsController : ControllerBase
{
    private static List<Airfield> Airfields = new List<Airfield>
    {
        new Airfield("Kitty Hawk", "North Carolina", "1900-1903", "First successful powered flight"),
        new Airfield("Huffman Prairie", "Ohio", "1904-1905", "Development and refinement of the Wright Flyer"),
        new Airfield("Le Mans", "France", "1908", "Public demonstrations of flight")
    };

    [HttpGet]
    public ActionResult<List<Airfield>> Get() => Airfields;

    [HttpGet("{id}")]
    public ActionResult<Airfield> Get(int id) => Airfields.Find(a => a.Name == id.ToString());

    [HttpPost]
    public ActionResult Post([FromBody] Airfield airfield)
    {
        Airfields.Add(airfield);
        return Ok();
    }

    [HttpPut("{id}")]
    public ActionResult Put(int id, [FromBody] Airfield updatedAirfield)
    {
        var airfield = Airfields.Find(a => a.Name == id.ToString());
        if (airfield == null) return NotFound();
        airfield.Name = updatedAirfield.Name;
        airfield.Location = updatedAirfield.Location;
        airfield.DatesOfUse = updatedAirfield.DatesOfUse;
        airfield.Significance = updatedAirfield.Significance;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var airfield = Airfields.Find(a => a.Name == id.ToString());
        if (airfield == null) return NotFound();
        Airfields.Remove(airfield);
        return NoContent();
    }
}