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

            if (plane == null)
            {
                return NotFound();
            }

            return Ok(plane);
        }

        [HttpPost]
        public ActionResult<Plane> Post(Plane plane)
        {
            Planes.Add(plane);

            return CreatedAtAction(nameof(GetById), new { id = plane.Id }, plane);
        }

        [HttpGet("search")]
        public ActionResult<List<Plane>> SearchByName([FromQuery] string name)
        {
            _logger.LogInformation($"GET ✈✈✈ {name} ✈✈✈");

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

        [HttpGet("range")]
        public ActionResult<List<Plane>> GetByRange([FromQuery] int min, [FromQuery] int max)
        {
            var planes = Planes.FindAll(p => p.RangeInKm >= min && p.RangeInKm <= max);

            if (planes == null)
            {
                return NotFound();
            }

            return Ok(planes);
        }

        // Get method for the year
        [HttpGet("year")]
        public ActionResult<List<Plane>> GetByYear([FromQuery] int year)
        {
            var planes = Planes.FindAll(p => p.Year == year);

            if (planes == null)
            {
                return NotFound();
            }

            return Ok(planes);
        }



    }
}
