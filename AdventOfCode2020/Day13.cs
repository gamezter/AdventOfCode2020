using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    class Day13
    {
        public static void part1()
        {
            string[] input = new StreamReader("day13.txt").ReadToEnd().Trim().Split();
            int target = int.Parse(input[0]);
            string[] times = input[1].Split(',');

            int minWait = int.MaxValue;
            int busId = 0;
            foreach(var time in times)
            {
                if(int.TryParse(time, out int t))
                {
                    int passes = target / t;
                    if (passes * t < target)
                        passes++;
                    int diff = passes * t - target;
                    if (diff < minWait)
                    {
                        minWait = diff;
                        busId = t;
                    }
                }
            }

            Console.Write(minWait * busId);
            Console.Read();
        }

        public static void part2()
        {
            //solve 13 * a == 41 * b - 3 == 641 * c - 13 == 19 * d - 25 == 17 * e - 30 == 29 * f - 42 == 661 * g - 44 == 37 * h - 50 == 23 * i - 67
        }
    }
}
