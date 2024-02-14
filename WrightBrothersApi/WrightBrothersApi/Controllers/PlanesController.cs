using Microsoft.AspNetCore.Mvc;
using WrightBrothersApi.Models;

namespace WrightBrothersApi.Controllers
{
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
                ImageUrl = "https://example.com/wright-flyer.jpg"
            },
            new Plane
            {
                Id = 2,
                Name = "Wright Flyer II",
                Year = 1904,
                Description = "A refinement of the original Flyer with better performance.",
                RangeInKm = 24,
                ImageUrl = "https://example.com/wright-flyer-ii.jpg"
            },
            new Plane
            {
                Id = 3,
                Name = "Wright Model A",
                Year = 1908,
                Description = "The first commercially successful airplane.",
                RangeInKm = 40,
                ImageUrl = "https://example.com/wright-model-a.jpg"
            },
            // Add more planes here
        };

        [HttpGet]
        public ActionResult<List<Plane>> GetAll()
        {
            _logger.LogInformation("GET all ✈✈✈ NO PARAMS ✈✈✈");

            return Ok(Planes);
        }

        [HttpGet("{id}")]
        public ActionResult<Plane> GetById(int id)
        {
            var plane = Planes.Find(p => p.Id == id);

            _logger.LogInformation($"GET by ID ✈✈✈ ID: {id} ✈✈✈");

            if (plane == null)
            {
                return NotFound();
            }

            return Ok(plane);
        }

        // This attribute indicates that this method should be invoked for HTTP POST requests.
        [HttpPost]
        public ActionResult<Plane> Post(Plane plane)
        {
            // Check if a plane with the same name already exists in the Planes collection.
            // If it does, return a BadRequest response. This is a way to prevent duplicate entries.
            if (Planes.Any(p => p.Name == plane.Name))
            {
                return BadRequest();
            }

            // If no plane with the same name exists, add the new plane to the Planes collection.
            Planes.Add(plane);

            // Log the information about the new plane that was added. This is useful for debugging and tracking purposes.
            _logger.LogInformation($"POST ✈✈✈ ID: {plane.Id} ✈✈✈");

            // Return a CreatedAtAction result. This returns a 201 status code, which means a new resource was successfully created.
            // The URI of the new resource is also sent in the Location header of the response.
            // The GetById method will be used to generate the URI.
            // The new { id = plane.Id } creates an anonymous object with a property id, which will be used as the route values to generate the URI.
            // The plane object will be included in the body of the response.
            return CreatedAtAction(nameof(GetById), new { id = plane.Id }, plane);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Plane plane)
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

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var plane = Planes.Find(p => p.Id == id);

            if (plane == null)
            {
                return NotFound();
            }

            Planes.Remove(plane);

            return NoContent();
        }

        [HttpGet("count/{count}")]
        public ActionResult<List<Plane>> GetByCount(int count)
        {
            var planes = Planes.Take(count).ToList();

            return Ok(planes);
        }

        // Get method for year 
        [HttpGet("year/{year}")]
        public ActionResult<List<Plane>> GetByYear(int year)
        {
            var planes = Planes.Where(p => p.Year == year).ToList();

            return Ok(planes);
        }

        // Get method for name
        [HttpGet("name/{name}")]
        public ActionResult<List<Plane>> GetByName(string name)
        {
            var planes = Planes.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();

            return Ok(planes);
        }
    }
}
