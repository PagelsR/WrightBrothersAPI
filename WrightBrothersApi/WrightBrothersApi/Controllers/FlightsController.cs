using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class FlightsController : ControllerBase
{
    private readonly ILogger<FlightsController> _logger;

    private List<Flight> Flights = new List<Flight>
    {
        // First ever flight of the Wright Brothers
        new Flight
        {
            Id = 1,
            FlightNumber = "WB001",
            Origin = "Kitty Hawk, NC",
            Destination = "Manteo, NC",
            DepartureTime = new DateTime(1903, 12, 17, 10, 35, 0),
            ArrivalTime = new DateTime(1903, 12, 17, 10, 35, 0).AddMinutes(12),
            Status = FlightStatus.Scheduled,
            // Format: DDMMYY-DEP-ARR-FLIGHT
            // For this flight
            // 17th of December 1903
            // Departure from Kitty Hawk, NC
            // Arrival at Manteo, NC
            // Flight number WB001
            FlightLogSignature = "171203-DEP-ARR-WB001"

        },
        // Second ever flight of the Wright Brothers
        new Flight
        {
            Id = 2,
            FlightNumber = "WB002",
            Origin = "Kitty Hawk, NC",
            Destination = "Manteo, NC",
            DepartureTime = new DateTime(1903, 12, 17, 10, 35, 0),
            ArrivalTime = new DateTime(1903, 12, 17, 10, 35, 0).AddMinutes(12),
            Status = FlightStatus.Scheduled,
            // Format: DDMMYY-DEP-ARR-FLIGHT
            // For this flight
            // 17th of December 1903
            // Departure from Kitty Hawk, NC
            // Arrival at Manteo, NC
            // Flight number WB002
            FlightLogSignature = "171203-DEP-ARR-WB002"
        },
        // Third ever flight of the Wright Brothers
        new Flight
        {
            Id = 3,
            FlightNumber = "WB003",
            Origin = "Kitty Hawk, NC",
            Destination = "Manteo, NC",
            DepartureTime = new DateTime(1903, 12, 17, 10, 35, 0),
            ArrivalTime = new DateTime(1903, 12, 17, 10, 35, 0).AddMinutes(12),
            Status = FlightStatus.Scheduled,
            // Format: DDMMYY-DEP-ARR-FLIGHT
            // For this flight
            // 17th of December 1903
            // Departure from Kitty Hawk, NC
            // Arrival at Manteo, NC
            // Flight number WB003
            FlightLogSignature = "171203-DEP-ARR-WB003"
        }
        
    };

    public FlightsController(ILogger<FlightsController> logger)
    {
        _logger = logger;
    }

    [HttpGet("{id}")]
    public ActionResult<Flight> GetById(int id)
    {
        _logger.LogInformation($"GET flight with --- Id:{id} ---");

        var flight = Flights.Find(f => f.Id == id);

        if (flight == null)
        {
            return NotFound();
        }

        return Ok(flight);
    }

    [HttpPost("{id}/status")]
    public ActionResult UpdateFlightStatus(int id, FlightStatus newStatus)
    {
        var flight = Flights.Find(f => f.Id == id);
        if (flight == null)
        {
            return NotFound("Flight not found.");
        }

        switch (newStatus)
        {
            case FlightStatus.Boarding:
                if (DateTime.Now > flight.DepartureTime)
                {
                    return BadRequest("Cannot board, past departure time.");
                }

                break;

            case FlightStatus.Departed:
                if (flight.Status != FlightStatus.Boarding)
                {
                    return BadRequest("Flight must be in 'Boarding' status before it can be 'Departed'.");
                }

                break;

            case FlightStatus.InAir:
                if (flight.Status != FlightStatus.Departed)
                {
                    return BadRequest("Flight must be in 'Departed' status before it can be 'In Air'.");
                }
                break;

            case FlightStatus.Landed:
                if (flight.Status != FlightStatus.InAir)
                {
                    return BadRequest("Flight must be in 'In Air' status before it can be 'Landed'.");
                }

                break;

            case FlightStatus.Cancelled:
                if (DateTime.Now > flight.DepartureTime)
                {
                    return BadRequest("Cannot cancel, past departure time.");
                }
                break;

            case FlightStatus.Delayed:
                if (flight.Status == FlightStatus.Cancelled)
                {
                    return BadRequest("Cannot delay, flight is cancelled.");
                }
                break;

            default:
                // Handle other statuses or unknown status
                return BadRequest("Unknown or unsupported flight status.");
        }

        flight.Status = newStatus;

        return Ok($"Flight status updated to {newStatus}.");
    }
}