using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    class Day6
    {
        public static void part1()
        {
            string[] groups = new StreamReader("day6.txt").ReadToEnd().Trim().Split(new[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries);

            int sum = 0;

            foreach(var group in groups)
            {
                bool[] votedYes = new bool[26]; 
                string[] persons = group.Split('\n');
                foreach(var person in persons)
                {
                    for (int i = 0; i < person.Length; ++i)
                        votedYes[person[i] - 'a'] = true;
                }

                foreach (var b in votedYes)
                {
                    if (b)
                        sum++;
                }
            }
            Console.Write(sum);
            Console.Read();
        }

        public static void part2()
        {
            string[] groups = new StreamReader("day6.txt").ReadToEnd().Trim().Split(new[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries);

            int sum = 0;

            foreach (var group in groups)
            {
                string[] persons = group.Split('\n');
                for(int i = 0; i < persons[0].Length; ++i)
                {
                    char c = persons[0][i];
                    bool allYes = true;
                    for(int j = 1; j < persons.Length; ++j)
                    {
                        if (persons[j].IndexOf(c) == -1)
                            allYes = false;
                    }
                    if (allYes)
                        sum++;
                }
            }
            Console.Write(sum);
            Console.Read();
        }
    }
}
