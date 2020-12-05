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
                int id = 0;
                for(int i = 0; i < pass.Length; ++i)
                {
                    if (pass[i] == 'B' || pass[i] == 'R')
                        id += 1 << (9 - i);
                }
                if (id > highest)
                    highest = id;
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
                int id = 0;
                for (int i = 0; i < pass.Length; ++i)
                {
                    if (pass[i] == 'B' || pass[i] == 'R')
                        id += 1 << (9 - i);
                }
                ids.Add(id);
            }

            ids.Sort();

            int start = ids[0];

            for(int i = 0; i < ids.Count; i++)
            {
                if(ids[i] != start++)
                {
                    Console.WriteLine(ids[i] - 1);
                    Console.Read();
                }
            }
        }
    }
}
