using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    class Day5
    {
        public static void part1()
        {
            string[] passes = new StreamReader("day5.txt").ReadToEnd().Trim().Split();

            int highest = 0;

            foreach(var pass in passes)
            {
                int row = 0;
                for(int i = 0; i < 7; ++i)
                {
                    if (pass[i] == 'B')
                        row += 1 << (6 - i);
                }
                int column = 0;
                for(int i = 0; i < 3; ++i)
                {
                    if (pass[i + 7] == 'R')
                        column += 1 << (2 - i);
                }

                if (row * 8 + column > highest)
                    highest = row * 8 + column;
            }

            Console.Write(highest);
            Console.Read();
        }

        public static void part2()
        {
            string[] passes = new StreamReader("day5.txt").ReadToEnd().Trim().Split();

            List<int> ids = new List<int>();

            foreach (var pass in passes)
            {
                int row = 0;
                for (int i = 0; i < 7; ++i)
                {
                    if (pass[i] == 'B')
                        row += 1 << (6 - i);
                }
                int column = 0;
                for (int i = 0; i < 3; ++i)
                {
                    if (pass[i + 7] == 'R')
                        column += 1 << (2 - i);
                }
                ids.Add(row * 8 + column);
            }

            ids.Sort();

            int id = ids[0];

            for(int i = 0; i < ids.Count; i++)
            {
                if(ids[i] != id++)
                {
                    Console.WriteLine(ids[i] - 1);
                    Console.Read();
                }
            }
        }
    }
}
