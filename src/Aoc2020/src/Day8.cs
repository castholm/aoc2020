using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Aoc2020
{
    public static class Day8
    {
        public static string Part1(string input)
        {
//            input = @"nop +0
//acc +1
//jmp +4
//acc +3
//jmp -3
//acc -99
//acc +1
//jmp -4
//acc +6";
            var x = SplitOnNewline(input)
                .Select(s => s.Split(' '))
                .Select(s => (s[0], int.Parse(s[1]), false))
                .ToArray();
                ;

            var a = 0;
            var i = 0;
            while (true)
            {
                if (x[i].Item3)
                {
                    break;
                }

                x[i].Item3 = true;
                switch (x[i].Item1)
                {
                case "acc":
                    a += x[i].Item2;
                    i += 1;
                    break;
                case "jmp":
                    i += x[i].Item2;
                    break;
                case "nop":
                    i += 1;
                    break;
                }
            }

            return "";
        }

        public static string Part2(string input)
        {
//            input = @"nop +0
//acc +1
//jmp +4
//acc +3
//jmp -3
//acc -99
//acc +1
//jmp -4
//acc +6";
            var x = SplitOnNewline(input)
                .Select(s => s.Split(' '))
                .Select(s => (s[0], int.Parse(s[1]), false))
                .ToArray();
            ;
            var xCopy = x.ToArray();

            var a = 0;
            var i = 0;
            var lastModifI = -1;
            while (i < x.Length)
            {
                if (x[i].Item3)
                {
                    x = xCopy.ToArray();
                    for (int ii = lastModifI + 1; ii < x.Length; ii++)
                    {
                        if (x[ii].Item1 is "nop" or "jmp")
                        {
                            x[ii].Item1 = x[ii].Item1 switch
                            {
                                "nop" => "jmp",
                                "jmp" => "nop",
                                _ => throw null!,
                            };

                            lastModifI = ii;
                            break;
                        }
                    }
                    a = 0;
                    i = 0;
                    continue;
                }

                x[i].Item3 = true;
                switch (x[i].Item1)
                {
                case "acc":
                    a += x[i].Item2;
                    i += 1;
                    break;
                case "jmp":
                    i += x[i].Item2;
                    break;
                case "nop":
                    i += 1;
                    break;
                }
            }

            var pp = x.Select((p, o) => (o, p.Item1, p.Item2, p.Item3)).Where(ppp => !ppp.Item4).ToList();

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
