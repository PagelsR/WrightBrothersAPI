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
            new Plane
            {
                Id = 4,
                Name = "Wright Model B",
                Year = 1910,
                Description = "A two-seat biplane used for training and exhibition flights.",
                RangeInKm = 80,
                ImageUrl = "https://example.com/wright-model-b.jpg"
            },
            new Plane
            {
                Id = 5,
                Name = "Wright Model EX",
                Year = 1911,
                Description = "A single-seat biplane used for exhibition flights.",
                RangeInKm = 100,
                ImageUrl = "https://example.com/wright-model-ex.jpg"
            }
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

            if (plane == null)
            {
                return NotFound();
            }

            return Ok(plane);
        }

        [HttpPost]
        public ActionResult<Plane> Post(Plane plane)
        {
            // Log the information that a POST request has been made
            _logger.LogInformation("POST ✈✈✈ PLANE ✈✈✈");
        
            // Check if the plane object is null
            if(plane == null)
            {
                // If the plane object is null, return a BadRequest
                return BadRequest("Plane object cannot be null.");
            }
        
            // Check if a plane with the same name already exists
            if (Planes.Any(p => p.Name == plane.Name))
            {
                // If a plane with the same name exists, return a BadRequest
                return BadRequest("Plane with the same name already exists.");
            }
            
            // If the plane object is not null and a plane with the same name does not exist, add the plane to the list of planes
            Planes.Add(plane);
        
            // Return a CreatedAtAction result. This returns a 201 status code, which means the request has been fulfilled and has resulted in a new resource being created
            return CreatedAtAction(nameof(GetById), new { id = plane.Id }, plane);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, Plane plane)
        {

            _logger.LogInformation("PUT ✈✈✈ PLANE ✈✈✈");

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

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {

            _logger.LogInformation("DELETE ✈✈✈ PLANE ✈✈✈");
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

        // Search planes by name
        [HttpGet("search")]
        public ActionResult<List<Plane>> SearchByName([FromQuery] string name)
        {
            var planes = Planes.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();

            return Ok(planes);
        }

        [HttpPost("setup")]
        public ActionResult SetupPlanesData(List<Plane> planes)
        {
            Planes.Clear();
            Planes.AddRange(planes);

            return Ok();
        }
        
    }
}
