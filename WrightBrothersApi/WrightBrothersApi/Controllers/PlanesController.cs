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
                ImageUrl = "https://example.com/images/wright-flyer.jpg"
            },
            new Plane
            {
                Id = 2,
                Name = "Wright Flyer II",
                Year = 1904,
                Description = "A refinement of the original Flyer with better performance.",
                RangeInKm = 24,
                ImageUrl = "https://example.com/images/wright-flyer-ii.jpg"
            },
            new Plane
            {
                Id = 3,
                Name = "Wright Model A",
                Year = 1908,
                Description = "The first commercially successful airplane.",
                RangeInKm = 40,
                ImageUrl = "https://example.com/images/wright-model-a.jpg"
            },
            new Plane
            {
                Id = 4,
                Name = "Wright Model B",
                Year = 1910,
                Description = "The first mass-produced airplane.",
                RangeInKm = 60,
                ImageUrl = "https://example.com/images/wright-model-b.jpg"
            },
            new Plane
            {
                Id = 5,
                Name = "Wright Model C",
                Year = 1912,
                Description = "An improved version of the Model B with a more powerful engine.",
                RangeInKm = 80,
                ImageUrl = "https://example.com/images/wright-model-c.jpg"
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

        /// <summary>
        /// This method is used to add a new plane to the list of planes.
        /// </summary>
        /// <param name="plane">The plane object that needs to be added.</param>
        /// <returns>
        /// Returns a BadRequest if the plane already exists in the list.
        /// If the plane is successfully added, it returns the plane object with a CreatedAtAction status.
        /// </returns>
        [HttpPost]
        public ActionResult<Plane> Post(Plane plane)
        {
            // Check if the plane already exists in the list of planes
            // If it does, return a BadRequest
            if (Planes.Any(p => p.Name == plane.Name))
            {
                return BadRequest();
            }

            // If the plane does not exist in the list, add it
            Planes.Add(plane);

            // Return the plane object with a CreatedAtAction status
            return CreatedAtAction(nameof(GetById), new { id = plane.Id }, plane);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, Plane plane)
        {

            _logger.LogInformation("PUT ✈✈✈ ID: {id} ✈✈✈", id);

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

            _logger.LogInformation("DELETE ✈✈✈ ID: {id} ✈✈✈", id);

            var existingPlane = Planes.Find(p => p.Id == id);

            if (existingPlane == null)
            {
                return NotFound();
            }

            Planes.Remove(existingPlane);

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
    }
}
