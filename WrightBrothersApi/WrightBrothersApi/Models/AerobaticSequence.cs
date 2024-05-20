using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class AerobaticSequence
{
    public List<Maneuver> Maneuvers { get; set; }
    public double Difficulty { get; set; }

    public class Maneuver
    {
        public string Type { get; set; }
        public int RepeatCount { get; set; }
        public double Difficulty { get; set; }
    }

    public static AerobaticSequence Parse(string signature)
    {
        var sequence = new AerobaticSequence { Maneuvers = new List<Maneuver>() };
        var maneuvers = Regex.Split(signature, "-");

        for (int i = 0; i < maneuvers.Length; i++)
        {
            var maneuver = new Maneuver
            {
                Type = maneuvers[i].Substring(0, 1),
                RepeatCount = int.Parse(maneuvers[i].Substring(1, maneuvers[i].Length - 2)),
                Difficulty = "ABCDE".IndexOf(maneuvers[i].Substring(maneuvers[i].Length - 1)) * 0.2 + 1.0
            };

            if (i > 0)
            {
                if (sequence.Maneuvers[i - 1].Type == "L" && maneuver.Type == "R")
                {
                    maneuver.Difficulty *= 2;
                }
                else if (sequence.Maneuvers[i - 1].Type == "T" && maneuver.Type == "S")
                {
                    maneuver.Difficulty *= 3;
                }
            }

            sequence.Maneuvers.Add(maneuver);
        }

        sequence.Difficulty = Math.Round(sequence.Maneuvers.Sum(m => m.RepeatCount * m.Difficulty), 2);

        return sequence;
    }
}