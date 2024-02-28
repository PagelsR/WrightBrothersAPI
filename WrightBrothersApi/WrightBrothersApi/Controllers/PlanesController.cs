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
                Description = "The first airplane to be used for military purposes.",
                RangeInKm = 80,
                ImageUrl = "https://example.com/wright-model-b.jpg"
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

            _logger.LogInformation($"GET by ID ✈✈✈ ID: {id} ✈✈✈");


            if (plane == null)
            {
                return NotFound();
            }

            return Ok(plane);
        }

        [HttpPost]
        public ActionResult<Plane> Post(Plane plane)
        {

            // Return BadRequest if plane already exists by ID
            if (Planes.Any(p => p.Id == plane.Id))
            {
                return BadRequest();
            }

            Planes.Add(plane);

            _logger.LogInformation($"POST ✈✈✈ ID: {plane.Id} ✈✈✈");

            return CreatedAtAction(nameof(GetById), new { id = plane.Id }, plane);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, Plane plane)
        {
            if (id != plane.Id)
            {
                return BadRequest();
            }

            var index = Planes.FindIndex(p => p.Id == id);

            if (index == -1)
            {
                return NotFound();
            }

            Planes[index] = plane;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var index = Planes.FindIndex(p => p.Id == id);

            if (index == -1)
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

            return Ok(planes);
        }

        // Get method for year
        [HttpGet("year/{year}")]
        public ActionResult<List<Plane>> GetByYear(int year)
        {
            var planes = Planes.Where(p => p.Year == year).ToList();

            return Ok(planes);
        }

    }
}
