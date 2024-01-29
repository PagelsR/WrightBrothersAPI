using Microsoft.AspNetCore.Mvc;
using WrightBrothersApi.Models;

namespace WrightBrothersApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PlanesController : ControllerBase
{
    private readonly ILogger<PlanesController> _logger;

    public PlanesController(ILogger<PlanesController> logger)
    {
        _logger = logger;
    }

    private static readonly List<Plane> Planes = new List<Plane>
    {
        new Plane
        {
            Id = 1,
            Name = "Wright Flyer",
            Year = 1903,
            Description = "The first successful heavier-than-air powered aircraft.",
            RangeInKm = 12,
            imageUrl = "https://upload.wikimedia.org/wikipedia/commons/8/8d/Wright_Flyer.jpg"
        },
        new Plane
        {
            Id = 2,
            Name = "Wright Flyer II",
            Year = 1904,
            Description = "A refinement of the original Flyer with better performance.",
            RangeInKm = 24,
            imageUrl = "https://upload.wikimedia.org/wikipedia/commons/8/8d/Wright_Flyer.jpg"
        },
        new Plane
        {
            Id = 3,
            Name = "Wright Flyer III",
            Year = 1905,
            Description = "The third powered aircraft by the Wright Brothers.",
            RangeInKm = 39,
            imageUrl = "https://upload.wikimedia.org/wikipedia/commons/8/8d/Wright_Flyer.jpg"
        },
        new Plane
        {
            Id = 4,
            Name = "Wright Model A",
            Year = 1908,
            Description = "The first Model A was one of the earliest aircraft with a fuselage.",
            RangeInKm = 56,
            imageUrl = "https://upload.wikimedia.org/wikipedia/commons/8/8d/Wright_Flyer.jpg"
        }
    };

    [HttpGet]
    public ActionResult<List<Plane>> GetAll()
    {
        _logger.LogInformation("GET all ✈✈✈ NO PARAMS ✈✈✈");

        return Planes;
    }

    [HttpGet("{id}")]
    public ActionResult<Plane> GetById(int id)
    {
        var plane = Planes.Find(p => p.Id == id);

        _logger.LogInformation($"GET ✈✈✈ {id} ✈✈✈");

        if (plane == null)
        {
            return NotFound();
        }

        return Ok(plane);
    }

    // search by name
    [HttpGet("search")]
    public ActionResult<List<Plane>> GetByName([FromQuery] string name)
    {
        var planes = Planes.FindAll(p => p.Name.Contains(name));
        _logger.LogInformation($"GET ✈✈✈ {name} ✈✈✈");

        if (planes == null || !planes.Any())
        {
            return NotFound();
        }

        return Ok(planes);
    }

    [HttpPost]
    public ActionResult<Plane> Post(Plane plane)
    {
        // Return BadRequest if plane already exists by name
        if (Planes.Any(p => p.Name == plane.Name))
        {
            return BadRequest();
        }

        Planes.Add(plane);
        _logger.LogInformation($"POST ✈✈✈ {plane.Id} ✈✈✈");

        return CreatedAtAction(nameof(GetById), new { id = plane.Id }, plane);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Plane plane)
    {
        if (id != plane.Id)
        {
            return BadRequest();
        }

        var planeToUpdate = Planes.Find(p => p.Id == id);

        if (planeToUpdate == null)
        {
            return NotFound();
        }

        planeToUpdate.Name = plane.Name;
        planeToUpdate.Year = plane.Year;
        planeToUpdate.Description = plane.Description;
        planeToUpdate.RangeInKm = plane.RangeInKm;
        planeToUpdate.imageUrl = plane.imageUrl;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var planeToDelete = Planes.Find(p => p.Id == id);

        if (planeToDelete == null)
        {
            return NotFound();
        }

        Planes.Remove(planeToDelete);

        return NoContent();
    }

    [HttpGet("count/{count}")]
    public ActionResult<List<Plane>> GetByCount(int count)
    {
        var planes = Planes.Take(count).ToList();

        if (planes == null)
        {
            return NotFound();
        }

        return Ok(planes);
    }

    // Get method for year
    [HttpGet("year/{year}")]
    public ActionResult<List<Plane>> GetByYear(int year)
    {
        var planes = Planes.FindAll(p => p.Year == year);

        if (planes == null)
        {
            return NotFound();
        }

        return Ok(planes);
    }


}
