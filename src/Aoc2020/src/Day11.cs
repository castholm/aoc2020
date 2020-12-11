using System;
using System.Linq;

namespace Aoc2020
{
    public static class Day11
    {
        public static string Part1(string input)
        {
            var linesPrev = input.Split('\n', StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.ToCharArray())
                .ToArray();

            var linesCurr = linesPrev.Select(s => s.ToArray()).ToArray();

            for (int iterations = 0; iterations < 1000; iterations++)
            {
                for (int y = 0; y < linesPrev.Length; y++)
                {
                    for (int x = 0; x < linesPrev[y].Length; x++)
                    {
                        if (linesPrev[y][x] == '.')
                            continue;

                        var numOccupiedNeighbors
                            = (x - 1 >= 0 && y - 1 >= 0 && linesPrev[y - 1][x - 1] == '#' ? 1 : 0)
                            + (y - 1 >= 0 && linesPrev[y - 1][x] == '#' ? 1 : 0)
                            + (x + 1 < linesPrev[y].Length && y - 1 >= 0 && linesPrev[y - 1][x + 1] == '#' ? 1 : 0)
                            + (x - 1 >= 0 && linesPrev[y][x - 1] == '#' ? 1 : 0)
                            + (x + 1 < linesPrev[y].Length && linesPrev[y][x + 1] == '#' ? 1 : 0)
                            + (x - 1 >= 0 && y + 1 < linesPrev.Length && linesPrev[y + 1][x - 1] == '#' ? 1 : 0)
                            + (y + 1 < linesPrev.Length && linesPrev[y + 1][x] == '#' ? 1 : 0)
                            + (x + 1 < linesPrev[y].Length && y + 1 < linesPrev.Length && linesPrev[y + 1][x + 1] == '#' ? 1 : 0);

                        linesCurr[y][x] = linesPrev[y][x] switch
                        {
                            'L' when numOccupiedNeighbors == 0 => '#',
                            '#' when numOccupiedNeighbors >= 4 => 'L',
                            _ => linesPrev[y][x],
                        };
                    }
                }

                (linesCurr, linesPrev) = (linesPrev, linesCurr);
            }

            return linesCurr.Sum(x => x.Count(c => c == '#')).ToString();
        }

        public static string Part2(string input)
        {
            var linesPrev = input.Split('\n', StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.ToCharArray())
                .ToArray();

            var linesCurr = linesPrev.Select(s => s.ToArray()).ToArray();

            for (int iterations = 0; iterations < 1000; iterations++)
            {
                for (int y = 0; y < linesPrev.Length; y++)
                {
                    for (int x = 0; x < linesPrev[y].Length; x++)
                    {
                        if (linesPrev[y][x] == '.')
                            continue;

                        var numOccupiedNeighbors
                            = WalkUntilOccupied(linesPrev, x, y, -1, -1)
                            + WalkUntilOccupied(linesPrev, x, y,  0, -1)
                            + WalkUntilOccupied(linesPrev, x, y,  1, -1)
                            + WalkUntilOccupied(linesPrev, x, y, -1,  0)
                            + WalkUntilOccupied(linesPrev, x, y,  1,  0)
                            + WalkUntilOccupied(linesPrev, x, y, -1,  1)
                            + WalkUntilOccupied(linesPrev, x, y,  0,  1)
                            + WalkUntilOccupied(linesPrev, x, y,  1,  1);

                        linesCurr[y][x] = linesPrev[y][x] switch
                        {
                            'L' when numOccupiedNeighbors == 0 => '#',
                            '#' when numOccupiedNeighbors >= 5 => 'L',
                            _ => linesPrev[y][x],
                        };

                        static int WalkUntilOccupied(char[][] array, int x, int y, int xInc, int yInc)
                        {
                            x += xInc;
                            y += yInc;
                            while (y >= 0 && y < array.Length && x >= 0 && x < array[y].Length)
                            {
                                if (array[y][x] == '#')
                                    return 1;

                                if (array[y][x] == 'L')
                                    return 0;

                                x += xInc;
                                y += yInc;
                            }

                            return 0;
                        }
                    }
                }

                (linesCurr, linesPrev) = (linesPrev, linesCurr);
            }

            return linesCurr.Sum(x => x.Count(c => c == '#')).ToString();
        }
    }
}
