using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;

namespace Aoc2020
{
    public static class Day3
    {
        public static string Part1(string input)
            => SplitOnNewline(input)
            .Where((s, y) => s[3 * y % s.Length] == '#')
            .Count()
            .ToString(CultureInfo.InvariantCulture);

        public static string Part2(string input)
        {
            var lines = SplitOnNewline(input).ToArray();

            return new (int Right, int Down)[] { (1, 1), (3, 1), (5, 1), (7, 1), (1, 2) }
                .Select(rd => lines
                    .Where((s, y) => y % rd.Down == 0)
                    .Where((s, y) => s[rd.Right * y % s.Length] == '#')
                    .Count())
                .Aggregate(BigInteger.One, (p, n) => p * n)
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
