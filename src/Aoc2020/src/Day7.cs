using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Aoc2020
{
    public static class Day7
    {
        private record ContaineeInfo(string Container, string Containee, int ContaineeCount);

        private static IEnumerable<ContaineeInfo> AsContaineeInfo(string input)
            => input.Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(s => s.Split(" bags contain ", 2))
            .SelectMany(lr => lr[1].Split(", ")
                .Select(r => Regex.Match(r, @"^(\d+) (\w+ \w+)"))
                .Where(m => m.Success)
                .Select(m => new ContaineeInfo(lr[0], m.Groups[2].Value, int.Parse(m.Groups[1].Value))));

        public static string Part1(string input)
        {
            var containerLookup = AsContaineeInfo(input).ToLookup(x => x.Containee);

            return GetContainers("shiny gold").Count().ToString();

            IEnumerable<string> GetContainers(string containee)
                => containerLookup![containee]
                .SelectMany(x => GetContainers(x.Container).Append(x.Container))
                .Distinct();
        }

        public static string Part2(string input)
        {
            var containeeLookup = AsContaineeInfo(input).ToLookup(x => x.Container);

            return GetTotalContaineeCount("shiny gold").ToString();

            int GetTotalContaineeCount(string container)
                => containeeLookup![container]
                .Select(x => (GetTotalContaineeCount(x.Containee) + 1) * x.ContaineeCount)
                .Sum();
        }
    }
}
