using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020
{
    public static class Day9
    {
        public static string Part1(string input)
            => SolveFor(input).FirstInvalid.ToString();

        public static string Part2(string input)
            => SolveFor(input).Weakness.ToString();

        private static (long FirstInvalid, long Weakness) SolveFor(string input)
        {
            var numbers = input.Split('\n', StringSplitOptions.RemoveEmptyEntries)
                .Select(n => long.Parse(n))
                .ToList();

            var firstInvalid = numbers
                .Skip(25)
                .Where((n, i) => !numbers
                    .Skip(i)
                    .Take(25)
                    .SelectMany(n1 => numbers
                        .Skip(i)
                        .Take(25)
                        .Where(n2 => n2 != n1)
                        .Select(n2 => n1 + n2))
                    .Contains(n))
                .First();

            var weakness = numbers
                .Select((_, i1) => numbers
                    .Skip(i1)
                    .AggregateWhile(
                        (Min: long.MaxValue, Max: 0L, Sum: 0L),
                        (a, n) => (Math.Min(a.Min, n), Math.Max(a.Max, n), a.Sum + n),
                        a => a.Sum <= firstInvalid))
                .Where(x => x.Sum == firstInvalid)
                .Select(x => x.Min + x.Max)
                .First();

            return (firstInvalid, weakness);
        }

        private static TAccumulate AggregateWhile<TSource, TAccumulate>(
            this IEnumerable<TSource> source,
            TAccumulate seed,
            Func<TAccumulate, TSource, TAccumulate> func,
            Func<TAccumulate, bool> predicate)
        {
            var result = seed;
            foreach (var element in source)
            {
                var nextResult = func(result, element);

                if (!predicate(nextResult))
                    break;

                result = nextResult;
            }

            return result;
        }
    }
}
