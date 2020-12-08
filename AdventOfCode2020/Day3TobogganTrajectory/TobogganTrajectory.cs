using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day3TobogganTrajectory
{
    public class TobogganTrajectory
    {
        public struct Slope
        {
            public int Run;
            public int Drop;

            public Slope(int run, int drop)
            {
                Run = run;
                Drop = drop;
            }
        }

        private static bool[][] ReadMapFromInput()
        {
            string[] lines = File.ReadAllLines("Day3TobogganTrajectory/map.txt");

            return lines
                .Select(l => l
                    .Select(c => c == '#')
                    .ToArray())
                .ToArray();
        }

        public static long CountTreesOnRoute(Slope slope)
        {
            bool[][] map = ReadMapFromInput();

            var trees = 0;

            for (int x = 0, y = 0; y < map.Length; x = (x + slope.Run) % map[x].Length, y += slope.Drop)
            {
                if (map[y][x]) trees++;
            }

            return trees;
        }

        public static long CheckAllRoutes(IEnumerable<Slope> slopes)
        {
            return slopes
                .Select(CountTreesOnRoute)
                .Aggregate((a, b) => a * b);
        }
    }
}