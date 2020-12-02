using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Aoc2020
{
    public static class Day2
    {
        public static string Part1(string input)
            => SplitOnNewline(input)
            .Select(s => Regex.Match(s, "^([0-9]+)-([0-9]+) ([A-Za-z]): ([A-Za-z]+)$"))
            .Select(m => (
                Min: int.Parse(m.Groups[1].Value, NumberStyles.Integer, CultureInfo.InvariantCulture),
                Max: int.Parse(m.Groups[2].Value, NumberStyles.Integer, CultureInfo.InvariantCulture),
                Character: m.Groups[3].Value[0],
                Password: m.Groups[4].Value))
            .Count(x => Enumerable.Range(x.Min, x.Max - x.Min + 1)
                .Contains(x.Password.Count(c => c == x.Character)))
            .ToString(CultureInfo.InvariantCulture);

        public static string Part2(string input)
            => SplitOnNewline(input)
            .Select(s => Regex.Match(s, "^([0-9]+)-([0-9]+) ([A-Za-z]): ([A-Za-z]+)$"))
            .Select(m => (
                Pos1: int.Parse(m.Groups[1].Value, NumberStyles.Integer, CultureInfo.InvariantCulture),
                Pos2: int.Parse(m.Groups[2].Value, NumberStyles.Integer, CultureInfo.InvariantCulture),
                Character: m.Groups[3].Value[0],
                Password: m.Groups[4].Value))
            .Count(x => x.Password[x.Pos1 - 1] == x.Character ^ x.Password[x.Pos2 - 1] == x.Character)
            .ToString(CultureInfo.InvariantCulture);

        private static IEnumerable<string> SplitOnNewline(string input)
        {
            using var reader = new StringReader(input);

            while (reader.ReadLine() is string line)
                yield return line;
        }
    }
}
