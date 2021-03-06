﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    class Day15
    {
        public static void part1()
        {
            List<int> numbers = new List<int> { 0, 14, 6, 20, 1, 4 };

            while(numbers.Count < 2020)
            {
                int lastIndex = numbers.LastIndexOf(numbers[numbers.Count - 1], numbers.Count - 2);
                if (lastIndex == -1)
                    numbers.Add(0);
                else
                    numbers.Add(numbers.Count - 1 - lastIndex);
            }
            Console.Write(numbers[2019]);
            Console.Read();
        }

        public static void part2()
        {
            Dictionary<int, int> numbers = new Dictionary<int, int> { { 0, 0 }, { 14, 1 }, { 6, 2 }, { 20, 3 }, { 1, 4 }, { 4, 5 } };

            int number = 0;

            for(int i = 6; (i + 1) < 30000000; ++i)
            {
                if(numbers.TryGetValue(number, out int lastIndex))
                {
                    numbers[number] = i;
                    number = i - lastIndex;
                }
                else
                {
                    numbers[number] = i;
                    number = 0;
                }
            }

            Console.Write(number);
            Console.Read();
        }
    }
}
