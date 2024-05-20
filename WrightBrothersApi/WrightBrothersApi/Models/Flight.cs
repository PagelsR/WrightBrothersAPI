using WrightBrothersApi.Models;

public class Flight
{
    public int Id { get; set; }
    public string FlightNumber { get; set; }
    public string Origin { get; set; }
    public string Destination { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public FlightStatus Status { get; set; }
    public int FuelRange { get; set; }

    public bool FuelTankLeak { get; set; }

    public string FlightLogSignature { get; set; }

    public FlightLog FlightLog => FlightLog.Parse(FlightLogSignature);

    public string AerobaticSequenceSignature { get; set; }

    public AerobaticSequence AerobaticSequence => AerobaticSequence.Parse(AerobaticSequenceSignature);
}

public enum FlightStatus
{
    Scheduled,
    Boarding,
    Departed,
    InAir,
    Landed,
    Cancelled,
    Delayed
}
