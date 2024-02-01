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
            ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/7/7a/Wright_Flyer.jpg"
        },
        new Plane
        {
            Id = 2,
            Name = "Wright Flyer II",
            Year = 1904,
            Description = "A refinement of the original Flyer with better performance.",
            RangeInKm = 24,
            ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/7/7a/Wright_Flyer.jpg"
        },
        new Plane
        {
            Id = 3,
            Name = "Wright Flyer III",
            Year = 1905,
            Description = "The first practical airplane.",
            RangeInKm = 38,
            ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/7/7a/Wright_Flyer.jpg"
        },
        new Plane
        {
            Id = 4,
            Name = "Wright Model A",
            Year = 1908,
            Description = "The first mass-produced airplane.",
            RangeInKm = 56,
            ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/7/7a/Wright_Flyer.jpg"
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

        _logger.LogInformation($"GET by id ✈✈✈ {id} ✈✈✈");

        if (plane == null)
        {
            return NotFound();
        }

        return Ok(plane);
    }

    [HttpPost]
    public ActionResult<Plane> Post(Plane plane)
    {
        // Return BadRequest if plane already exists by name
        if (Planes.Any(p => p.Name == plane.Name))
        {
            return BadRequest();
        }

        // Add the new plane to the collection
        Planes.Add(plane);

        // Log the creation of the new plane
        _logger.LogInformation($"POST ✈✈✈ {plane.Id} ✈✈✈");

        // Return a CreatedAtAction result. This returns a 201 status code, 
        // indicating that the request has been fulfilled and has resulted in 
        // one new resource being created. The URI of the new resource is 
        // returned in the Location header.
        return CreatedAtAction(nameof(GetById), new { id = plane.Id }, plane);
    }

    // search by name
    [HttpGet("search")]
    public ActionResult<List<Plane>> GetByName([FromQuery] string name)
    {
        var planes = Planes.FindAll(p => p.Name.Contains(name));

        if (planes == null)
        {
            return NotFound();
        }

        return Ok(planes);
    }

    [HttpPut("{id}")]
    public ActionResult<Plane> Put(int id, Plane plane)
    {
        var index = Planes.FindIndex(p => p.Id == id);

        if (index < 0)
        {
            return NotFound();
        }

        Planes[index] = plane;

        return Ok(plane);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var index = Planes.FindIndex(p => p.Id == id);

        if (index < 0)
        {
            return NotFound();
        }

        Planes.RemoveAt(index);

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
