using System;
using System.Globalization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Aoc2020
{
    public static class Day7
    {
        public static string Part1(string input)
        {
//            input = @"light red bags contain 1 bright white bag, 2 muted yellow bags.
//dark orange bags contain 3 bright white bags, 4 muted yellow bags.
//bright white bags contain 1 shiny gold bag.
//muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.
//shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.
//dark olive bags contain 3 faded blue bags, 4 dotted black bags.
//vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.
//faded blue bags contain no other bags.
//dotted black bags contain no other bags.";

            var xx = new Regex(@"(\w+ \w+) contain");

            var x = SplitOnNewline(input)
                .Select(s => s.Split(" bags contain ", 2))
                .Select(ss => (Left: ss[0], Right: ss[1].Split(", ")
                    .Select(m => Regex.Match(m, @"^\d+\s(\w+\s\w+)"))
                    .Where(m => m.Success)
                    .Select(m => m.Groups[1].Value)
                    .ToList()))
                .ToList();

            var prevCount = 0;
            var set = new HashSet<string>{"shiny gold"};

            while (x.Where(p => p.Right.Intersect(set).Any()).Count() is int count && count > prevCount)
            {
                prevCount = count;
                set.UnionWith(x.Where(p => p.Right.Intersect(set).Any()).Select(z => z.Left));

            }

            var z = x.Where(p => p.Right.Contains("shiny gold"));

                //.Aggregate(new[]{ "shiny gold" }.AsEnumerable(), (a, z) => a.Intersect(z.Right).Any()
                //    ? a.Append(z.Left).Distinct()
                //    : a)
                //.Count();

            return "";
        }

        public static string Part2(string input)
        {
//            input = @"light red bags contain 1 bright white bag, 2 muted yellow bags.
//dark orange bags contain 3 bright white bags, 4 muted yellow bags.
//bright white bags contain 1 shiny gold bag.
//muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.
//shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.
//dark olive bags contain 3 faded blue bags, 4 dotted black bags.
//vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.
//faded blue bags contain no other bags.
//dotted black bags contain no other bags.";

            var xxx = SplitOnNewline(input)
                .Select(s => s.Split(" bags contain ", 2))
                .Select(ss => (Left: ss[0], Right: ss[1].Split(", ")
                    .Select(m => Regex.Match(m, @"^(\d+)\s(\w+\s\w+)"))
                    .Where(m => m.Success)
                    .Select(m => (N: int.Parse(m.Groups[1].Value), Color: m.Groups[2].Value))
                    .ToList()))
                .ToDictionary(p => p.Left, p => p.Right);

            int GetC(string s)
            {
                return xxx![s].Select(z => z.N * GetC(z.Color)).Sum() + 1;
            }
            var ff = GetC("shiny gold");

            return "";
        }

        private static IEnumerable<string> SplitOnNewline(string input)
        {
            using var reader = new StringReader(input);

            while (reader.ReadLine() is string line)
                yield return line;
        }
    }
}
