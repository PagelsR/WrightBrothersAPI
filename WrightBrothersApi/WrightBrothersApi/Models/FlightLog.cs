using System;
using System.Globalization;

public record FlightLog
{
    public DateTime Date { get; init; }
    public string Departure { get; init; }
    public string Arrival { get; init; }
    public string FlightNumber { get; init; }

    public static FlightLog Parse(string flightLogSignature)
    {
        var parts = flightLogSignature.Split('-');
        if (parts.Length != 4)
        {
            throw new FormatException("Invalid flight log signature format.");
        }

        var date = DateTime.ParseExact(parts[0], "ddMMyyyy", CultureInfo.InvariantCulture);
        var departure = parts[1];
        var arrival = parts[2];
        var flightNumber = parts[3];

        return new FlightLog
        {
            Date = date,
            Departure = departure,
            Arrival = arrival,
            FlightNumber = flightNumber
        };
    }
}