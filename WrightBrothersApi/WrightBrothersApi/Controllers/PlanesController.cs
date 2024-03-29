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
                ImageUrl = "https://example.com/wright_flyer.jpg"
            },
            new Plane
            {
                Id = 2,
                Name = "Wright Flyer II",
                Year = 1904,
                Description = "A refinement of the original Flyer with better performance.",
                RangeInKm = 24,
                ImageUrl = "https://example.com/wright_flyer_ii.jpg"
            },
            new Plane
            {
                Id = 3,
                Name = "Wright Model A",
                Year = 1908,
                Description = "The first commercially successful airplane.",
                RangeInKm = 40,
                ImageUrl = "https://example.com/wright_model_a.jpg"
            },
            new Plane
            {
                Id = 4,
                Name = "Wright Model B",
                Year = 1910,
                Description = "The first mass-produced airplane.",
                RangeInKm = 60,
                ImageUrl = "https://example.com/wright_model_b.jpg"
            },
            new Plane
            {
                Id = 5,
                Name = "Wright Model C",
                Year = 1912,
                Description = "An improved version of Model B with better stability.",
                RangeInKm = 80,
                ImageUrl = "https://example.com/wright_model_c.jpg"
            }
        };

        [HttpGet]
        public ActionResult<List<Plane>> GetAll()
        {
            _logger.LogInformation("GET all ✈✈✈ NO PARAMS ✈✈✈");

            return Ok(Planes);
        }

        /// <summary>
        /// This is a HTTP GET method in the PlanesController class.
        /// </summary>
        /// <param name="id">The ID of the plane to retrieve.</param>
        /// <returns>
        /// An ActionResult of type Plane. This method will return HTTP 200 (OK) along with the plane data if the plane is found.
        /// If the plane is not found, it will return HTTP 404 (Not Found).
        /// </returns>
        [HttpGet("{id}")]
        public ActionResult<Plane> GetById(int id)
        {
            // Log the information that a GET request by id has been made.
            // The id of the requested plane is also logged.
            _logger.LogInformation($"GET by id ✈✈✈ ID: {id} ✈✈✈");

            // Find the plane with the given id in the Planes collection.
            // The Find method will return the first plane that matches the provided predicate.
            // In this case, the predicate is a lambda expression that checks if the plane's Id is equal to the provided id.
            var plane = Planes.Find(p => p.Id == id);

            // If the plane is not found (i.e., if plane is null), return HTTP 404 (Not Found).
            if (plane == null)
            {
                return NotFound();
            }

            // If the plane is found, return HTTP 200 (OK) along with the plane data.
            return Ok(plane);
        }

        [HttpPost]
        public ActionResult<Plane> Post(Plane plane)
        {

            _logger.LogInformation($"POST ✈✈✈ {plane.Name} ✈✈✈");

            // Return BadRequest if plane already exists by id
            if (Planes.Any(p => p.Id == plane.Id))
            {
                return BadRequest();
            }

            Planes.Add(plane);

            return CreatedAtAction(nameof(GetById), new { id = plane.Id }, plane);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, Plane plane)
        {

            _logger.LogInformation($"PUT ✈✈✈ ID: {id} ✈✈✈");

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
            return Ok(Planes.Take(count).ToList());
        }

        // Search planes by name
        [HttpGet("search")]
        public ActionResult<List<Plane>> SearchByName([FromQuery] string name)
        {
            return Ok(Planes.Where(p => p.Name.Contains(name)).ToList());
        }

    }
}
