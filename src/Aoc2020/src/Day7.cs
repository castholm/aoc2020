using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Aoc2020
{
    public static class Day7
    {
        public static string Part1(string input)
        {
            var containerLookup = SplitOnNewline(input)
                .Select(s => s.Split(" bags contain ", 2))
                .SelectMany(lr => lr[1].Split(", ")
                    .Select(r => Regex.Match(r, "^[0-9]+ ([a-z]+ [a-z]+)"))
                    .Where(m => m.Success)
                    .Select(m => (Container: lr[0], Containee: m.Groups[1].Value)))
                .ToLookup(x => x.Containee);

            return GetContainers("shiny gold")
                .Count()
                .ToString(CultureInfo.InvariantCulture);

            IEnumerable<string> GetContainers(string containee)
                => containerLookup![containee]
                .SelectMany(x => GetContainers(x.Container).Append(x.Container))
                .Distinct();
        }

        public static string Part2(string input)
        {
            var containeeLookup = SplitOnNewline(input)
                .Select(s => s.Split(" bags contain ", 2))
                .SelectMany(lr => lr[1].Split(", ")
                    .Select(r => Regex.Match(r, "^([0-9]+) ([a-z]+ [a-z]+)"))
                    .Where(m => m.Success)
                    .Select(m => (
                        Container: lr[0],
                        Containee: m.Groups[2].Value,
                        ContaineeCount: int.Parse(
                            m.Groups[1].Value,
                            NumberStyles.None,
                            CultureInfo.InvariantCulture))))
                .ToLookup(x => x.Container);

            return SumContainees("shiny gold").ToString(CultureInfo.InvariantCulture);

            int SumContainees(string container)
                => containeeLookup![container]
                .Select(x => (SumContainees(x.Containee) + 1) * x.ContaineeCount)
                .Sum();
        }

        private static IEnumerable<string> SplitOnNewline(string input)
        {
            using var reader = new StringReader(input);

            while (reader.ReadLine() is string line)
                yield return line;
        }
    }
}
