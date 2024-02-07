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

    // Format: DDMMYY-DEP-ARR-FLIGHT
    public string FlightLogSignature { get; set; }

    // Encodes a series of aerobatic maneuvers
    // L = Loop, H = Hammerhead, R = Roll, S = Spin, T = Tailslide
    // Number represents repeat count, Letter represents difficulty (A-E), and each has a unique ID
    // Example 1: L4B-H2C-R3A-S1D-T2E
    // Example 2: L1A-H1B-R1C-S1D-T1E
    public string AerobaticSequence { get; set; }
}