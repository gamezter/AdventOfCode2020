using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    class Day1
    {
        public static void part1()
        {
            string[] numbers = new StreamReader("day1.txt").ReadToEnd().Trim().Split();
            int[] n = new int[numbers.Length];
            for(int i = 0; i < n.Length; i++)
            {
                n[i] = int.Parse(numbers[i]);
            }
            
            for(int i = 0; i < n.Length - 1; ++i)
            {
                for(int j = i + 1; j < n.Length; ++j)
                {
                    if (n[j] == 2020 - n[i])
                    {
                        Console.WriteLine(n[i] * n[j]);
                        Console.Read();
                    }
                }
            }
        }

        public static void part2()
        {
            string[] numbers = new StreamReader("day1.txt").ReadToEnd().Trim().Split();
            int[] n = new int[numbers.Length];
            for (int i = 0; i < n.Length; i++)
            {
                n[i] = int.Parse(numbers[i]);
            }

            for (int i = 0; i < n.Length - 2; ++i)
            {
                for (int j = i + 1; j < n.Length - 1; ++j)
                {
                    for (int k = j + 1; k < n.Length; ++k)
                    {
                        if (n[k] == 2020 - n[i] - n[j])
                        {
                            Console.WriteLine(n[i] * n[j] * n[k]);
                            Console.Read();
                        }
                    }
                }
            }
        }
    }
}
