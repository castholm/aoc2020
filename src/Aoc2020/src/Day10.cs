using System;
using System.Linq;

namespace Aoc2020
{
    public static class Day10
    {
        public static string Part1(string input)
        {
            var (_, diff1Count, diff3Count) = input.Split('\n', StringSplitOptions.RemoveEmptyEntries)
                .Select(n => long.Parse(n))
                .OrderBy(n => n)
                .Aggregate((Previous: 0L, Diff1Count: 0, Diff3Count: 1), (a, n) => (
                    n,
                    a.Diff1Count + (n - a.Previous == 1 ? 1 : 0),
                    a.Diff3Count + (n - a.Previous == 3 ? 1 : 0)));

            return (diff1Count * diff3Count).ToString();
        }

        public static string Part2(string input)
        {
            var nodes = input.Split('\n', StringSplitOptions.RemoveEmptyEntries)
                .Select(n => int.Parse(n))
                .Append(0)
                .OrderBy(n => n)
                .Select(n => (Jolts: n, Connections: 0L))
                .ToArray();

            nodes[0].Connections = 1;
            for (int i = 0; i < nodes.Length; i++)
            {
                for (int j = i + 1; j < nodes.Length && nodes[j].Jolts - nodes[i].Jolts <= 3; j++)
                    nodes[j].Connections += nodes[i].Connections;
            }

            return nodes.Last().Connections.ToString();
        }
    }
}
