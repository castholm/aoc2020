using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Aoc2020
{
    public static class Day1
    {
        public static string Part1(string input)
        {
            var lines = SplitOnNewline(input)
                .Select(s => int.Parse(s, NumberStyles.Integer, CultureInfo.InvariantCulture))
                .ToArray();

            return lines
                .SelectMany((left, leftOffset) => lines
                    .Skip(leftOffset)
                    .SelectMany(right => left + right == 2020
                        ? new[] { left * right }
                        : Enumerable.Empty<int>()))
                .First()
                .ToString(CultureInfo.InvariantCulture);
        }

        public static string Part2(string input)
        {
            var lines = SplitOnNewline(input)
                .Select(s => int.Parse(s, NumberStyles.Integer, CultureInfo.InvariantCulture))
                .ToArray();

            return lines
                .SelectMany((left, leftOffset) => lines
                    .Skip(leftOffset)
                    .SelectMany((middle, middleOffset) => lines
                        .Skip(middleOffset)
                        .SelectMany(right => left + middle + right == 2020
                            ? new[] { left * middle * right }
                            : Enumerable.Empty<int>())))
                .First()
                .ToString(CultureInfo.InvariantCulture);
        }

        private static IEnumerable<string> SplitOnNewline(string input)
        {
            using var reader = new StringReader(input);

            while (reader.ReadLine() is string line)
                yield return line;
        }
    }
}
