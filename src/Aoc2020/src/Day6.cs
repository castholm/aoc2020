using System;
using System.Globalization;
using System.Linq;

namespace Aoc2020
{
    public static class Day6
    {
        public static string Part1(string input)
            => input.Split("\n\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(g => g.Split("\n", StringSplitOptions.RemoveEmptyEntries)
                .SelectMany(p => p)
                .Distinct()
                .Count())
            .Sum()
            .ToString(CultureInfo.InvariantCulture);

        public static string Part2(string input)
            => input.Split("\n\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(g => g.Split("\n", StringSplitOptions.RemoveEmptyEntries)
                .Aggregate("abcdefghijklmnopqrstuvwxyz".AsEnumerable(), (a, p) => a.Intersect(p))
                .Count())
            .Sum()
            .ToString(CultureInfo.InvariantCulture);
    }
}
