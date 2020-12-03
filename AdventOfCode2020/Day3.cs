using System;
using System.IO;

namespace AdventOfCode2020
{
    class Day3
    {
        public static void part1()
        {
            string[] lines = new StreamReader("day3.txt").ReadToEnd().Trim().Split('\n');
            int width = lines[0].Length;
            int x = 0, y = 0;
            int treeCount = 0;
            while(y < lines.Length)
            {
                if (lines[y][x] == '#')
                    treeCount++;
                x = (x + 3) % width;
                y++;
            }
            Console.WriteLine(treeCount);
            Console.Read();
        }

        public static void part2()
        {
            string[] lines = new StreamReader("day3.txt").ReadToEnd().Trim().Split('\n');
            (int dx, int dy)[] runs = { (1, 1), (3, 1), (5, 1), (7, 1), (1, 2) };
            int width = lines[0].Length;
            long total = 1;
            foreach(var run in runs)
            {
                int x = 0, y = 0;
                int treeCount = 0;
                while (y < lines.Length)
                {
                    if (lines[y][x] == '#')
                        treeCount++;
                    x = (x + run.dx) % width;
                    y += run.dy;
                }
                total *= treeCount;
            }
            
            Console.WriteLine(total);
            Console.Read();
        }
    }
}
