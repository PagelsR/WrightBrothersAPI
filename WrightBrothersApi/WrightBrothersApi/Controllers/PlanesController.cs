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
            ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/8/8c/Wright_Flyer.jpg"
        },
        new Plane
        {
            Id = 2,
            Name = "Wright Flyer II",
            Year = 1904,
            Description = "A refinement of the original Flyer with better performance.",
            RangeInKm = 24,
            ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/8/8c/Wright_Flyer.jpg"
        },
        new Plane
        {
            Id = 3,
            Name = "Wright Flyer III",
            Year = 1905,
            Description = "The first practical airplane.",
            RangeInKm = 38,
            ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/8/8c/Wright_Flyer.jpg"
        },
        new Plane
        {
            Id = 4,
            Name = "Wright Model A",
            Year = 1908,
            Description = "The first mass-produced airplane.",
            RangeInKm = 56,
            ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/8/8c/Wright_Flyer.jpg"
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

        _logger.LogInformation($"GET by id ✈✈✈ ID: {id} ✈✈✈");

        if (plane == null)
        {
            return NotFound();
        }

        return Ok(plane);
    }

    // This attribute indicates that this method should handle HTTP POST requests.
    [HttpPost]
    // This is a public method named Post that returns an ActionResult of type Plane.
    // It takes a Plane object as a parameter, which is the plane to be added.
    public ActionResult<Plane> Post(Plane plane)
    {
        // This line checks if a plane with the same name already exists in the Planes collection.
        // If it does, it returns a BadRequest (HTTP 400 status code), indicating that the request is invalid.
        if (Planes.Any(p => p.Name == plane.Name))
        {
            return BadRequest();
        }

        // If no plane with the same name exists, the new plane is added to the Planes collection.
        Planes.Add(plane);

        // This line logs an informational message indicating that a new plane has been added.
        _logger.LogInformation($"POST ✈✈✈ ID: {plane.Id} ✈✈✈");

        // This line returns a CreatedAtAction result (HTTP 201 status code), which includes the route to the new plane (via the GetById action) and the new plane itself.
        // This is a common practice in RESTful APIs to return the location of the newly created resource.
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
        if (id != plane.Id)
        {
            return BadRequest();
        }

        var existingPlane = Planes.Find(p => p.Id == id);

        if (existingPlane == null)
        {
            return NotFound();
        }

        existingPlane.Name = plane.Name;
        existingPlane.Year = plane.Year;
        existingPlane.Description = plane.Description;
        existingPlane.RangeInKm = plane.RangeInKm;
        existingPlane.ImageUrl = plane.ImageUrl;

        return Ok(existingPlane);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var plane = Planes.Find(p => p.Id == id);

        if (plane == null)
        {
            return NotFound();
        }

        Planes.Remove(plane);

        return Ok();
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
