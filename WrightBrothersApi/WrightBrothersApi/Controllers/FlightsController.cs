using System.Collections.Concurrent;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

[ApiController]
[Route("[controller]")]
public class FlightsController : ControllerBase
{
    private readonly ILogger<FlightsController> _logger;

    private List<Flight> Flights = new List<Flight>
    {
        new Flight
        {
            Id = 1,
            FlightNumber = "WB001",
            Origin = "Kitty Hawk, NC",
            Destination = "Manteo, NC",
            DepartureTime = new DateTime(1903, 12, 17, 10, 35, 0),
            ArrivalTime = new DateTime(1903, 12, 17, 10, 35, 0).AddMinutes(12),
            Status = FlightStatus.Scheduled,
            FuelRange = 100,
            FuelTankLeak = false,
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
            FuelRange = 100,
            FuelTankLeak = false,
            FlightLogSignature = "171203-DEP-ARR-WB002"
        }

    };

    public FlightsController(ILogger<FlightsController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public ActionResult<Flight> Post([FromBody] Flight flight)
    {
        _logger.LogInformation("POST ✈✈✈ NO PARAMS ✈✈✈");

        Flights.Add(flight);

        return CreatedAtAction(nameof(GetById), new { id = flight.Id }, flight);
    }

    [HttpGet("{id}")]
    public ActionResult<Flight> GetById(int id)
    {
        _logger.LogInformation($"GET ✈✈✈ {id} ✈✈✈");

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
        if (flight != null)
        {
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
        else
        {
            return NotFound("Flight not found.");
        }
    }


    [HttpPost("{id}/takeFlight/{flightLength}")]
    public ActionResult takeFlight(int id, int flightLength)
    {
        var flight = Flights.Find(f => f.Id == id);

        for (int i = 0; i < flightLength; i++)
        {
            if (flight.FuelRange == 0)
            {
                throw new Exception("Plane crashed, due to lack of fuel");
            }
            else
            {
                var fuelConsumption = 0;
                if (flight.FuelTankLeak)
                {
                    fuelConsumption = 2;
                }


                flight.FuelRange -= fuelConsumption;
            }
        }

        return Ok($"Flight took off and flew {flightLength} kilometers/miles.");
    }
    
    [HttpPost("{id}/lightningStrike")]
    public ActionResult lightningStrike(int id)
    {
        // Removed the recursive call
        // lightningStrike(id);

        return Ok($"Recovers from lightning strike.");
    }

    [HttpPost("{id}/calculateAerodynamics")]
    public ActionResult calculateAerodynamics(int id)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        List<int> primes = CalculatePrimes(2, 300000);

        stopwatch.Stop();
        Console.WriteLine($"Found {primes.Count} prime numbers.");
        Console.WriteLine($"Elapsed Time: {stopwatch.ElapsedMilliseconds / 1000.0} seconds");

        return Ok($"Calculated aerodynamics.");
    }

    public static List<int> CalculatePrimes(int start, int end)
    {
        List<int> primes = new List<int>();
        for (int number = start; number <= end; number++)
        {
            if (IsPrime(number))
            {
                primes.Add(number);
            }
        }
        return primes;
    }

    public static bool IsPrime(int number)
    {
        if (number <= 1) return false;
        int boundary = (int)Math.Floor(Math.Sqrt(number));

        for (int i = 2; i <= boundary; i++)  // Only check up to the square root of number
        {
            if (number % i == 0) return false;
        }
        return true;
    }}
