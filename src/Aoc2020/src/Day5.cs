using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Aoc2020
{
    public static class Day5
    {
        public static string Part1(string input)
            => SplitOnNewline(input)
            .Select(s => Convert.ToInt32(
                Regex.Replace(Regex.Replace(s, "[BR]", "1"), "[FL]", "0"),
                fromBase: 2))
            .Max()
            .ToString(CultureInfo.InvariantCulture);

        public static string Part2(string input)
        {
            var availableSeats = Enumerable.Range(0, 1024)
                .Except(SplitOnNewline(input)
                    .Select(s => Convert.ToInt32(
                        Regex.Replace(Regex.Replace(s, "[BR]", "1"), "[FL]", "0"),
                        fromBase: 2)));

            var existentSeat = availableSeats
                .Except(availableSeats.Select(n => n + 1))
                .Except(availableSeats.Select(n => n - 1))
                .Single();

            return existentSeat.ToString(CultureInfo.InvariantCulture);
        }

        private static IEnumerable<string> SplitOnNewline(string input)
        {
            using var reader = new StringReader(input);

            while (reader.ReadLine() is string line)
                yield return line;
        }
    }
}
