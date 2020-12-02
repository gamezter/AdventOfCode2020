using System;
using System.IO;

namespace AdventOfCode2020
{
    class Day2
    {
        public static void part1()
        {
            string[] lines = new StreamReader("day2.txt").ReadToEnd().Trim().Split('\n');
            int valid = 0;
            for(int i = 0; i < lines.Length; ++i)
            {
                string[] parts = lines[i].Split('-', ' ', ':');
                int min = int.Parse(parts[0]);
                int max = int.Parse(parts[1]);
                char character = parts[2][0];
                string pass = parts[4];

                int count = 0;

                for(int j = 0; j < pass.Length; j++)
                {
                    if (pass[j] == character)
                        count++;
                }

                if (count <= max && count >= min)
                    valid++;
            }
            Console.WriteLine(valid);
            Console.Read();
        }

        public static void part2()
        {
            string[] lines = new StreamReader("day2.txt").ReadToEnd().Trim().Split('\n');
            int valid = 0;
            for (int i = 0; i < lines.Length; ++i)
            {
                string[] parts = lines[i].Split('-', ' ', ':');
                int i1 = int.Parse(parts[0]) - 1;
                int i2 = int.Parse(parts[1]) - 1;
                char character = parts[2][0];
                string pass = parts[4];

                int count = 0;
                if (pass[i1] == character)
                    count++;
                if (pass[i2] == character)
                    count++;
                if (count == 1)
                    valid++;
            }
            Console.WriteLine(valid);
            Console.Read();
        }
    }
}
