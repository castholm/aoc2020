using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Aoc2020
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var day = args.ElementAtOrDefault(0);
            var credentials = args.ElementAtOrDefault(1);

            if (day is null || !int.TryParse(day, NumberStyles.None, CultureInfo.InvariantCulture, out _))
            {
                Console.WriteLine("No day was specified.");

                return;
            }

            var inputPath = Path.Join("inputs", $"day{day}.txt");

            if (!File.Exists(inputPath))
            {
                if (credentials is null || !Regex.IsMatch(credentials, "^[0-9A-Fa-f]+$"))
                {
                    Console.WriteLine(
                        $"No previously downloaded input for day {day} could be found and no credentials were specified.");

                    return;
                }

                var httpClientHandler = new HttpClientHandler();
                var httpClient = new HttpClient(httpClientHandler);

                httpClientHandler.CookieContainer.Add(new Cookie("session", credentials, "/", ".adventofcode.com"));

                var response = await httpClient.GetAsync($"https://adventofcode.com/2020/day/{day}/input");
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Could not retrieve the input for day {day} from the Advent of Code servers.");

                    return;
                }

                Directory.CreateDirectory(Path.GetDirectoryName(inputPath)!);
                using var fileStream = File.OpenWrite(inputPath);
                await response.Content.CopyToAsync(fileStream);
            }

            var input = await File.ReadAllTextAsync(inputPath);

            var type = typeof(Program).Assembly.GetType($"Aoc2020.Day{day}");
            var part1Solver = CreateSolverDelegate(type?.GetMethod("Part1"));
            var part2Solver = CreateSolverDelegate(type?.GetMethod("Part2"));
            if (part1Solver is null || part2Solver is null)
            {
                Console.WriteLine($"No solver has been defined for day {day}.");

                return;
            }

            Console.WriteLine($"Advent of Code 2020, Day {day}");
            Console.WriteLine();
            Console.WriteLine("Answer 1:");
            Console.WriteLine($"    {part1Solver(input)}");
            Console.WriteLine();
            Console.WriteLine("Answer 2:");
            Console.WriteLine($"    {part2Solver(input)}");
        }

        private static Func<string, string>? CreateSolverDelegate(MethodInfo? method)
        {
            if (method is null)
                return null;

            return (Func<string, string>?)Delegate.CreateDelegate(typeof(Func<string, string>), method);
        }
    }
}
