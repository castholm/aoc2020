using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Aoc2020
{
    public static class Day4
    {
        public static string Part1(string input)
            => input.Split("\n\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(s => s.Split(new[] { '\n', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(kvp => kvp.Split(':')[0])
                .Count(k => new[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" }.Contains(k)))
            .Count(c => c == 7)
            .ToString(CultureInfo.InvariantCulture);

        public static string Part2(string input)
        {
            var validationRegex = new Regex(
                @"^(
                    byr:(19[2-9][0-9]|200[0-2])|
                    iyr:20(1[0-9]|20)|
                    eyr:20(2[0-9]|30)|
                    hgt:(1([5-8][0-9]|9[0-3])cm|(59|6[0-9]|7[0-6])in)|
                    hcl:\#([0-9a-f]{6})|
                    ecl:(amb|blu|brn|gry|grn|hzl|oth)|
                    pid:[0-9]{9})
                $",
                RegexOptions.IgnorePatternWhitespace);

            return input
                .Split("\n\n", StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Split(new[] { '\n', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Count(kvp => validationRegex.IsMatch(kvp)))
                .Count(c => c == 7)
                .ToString(CultureInfo.InvariantCulture);
        }
    }
}
