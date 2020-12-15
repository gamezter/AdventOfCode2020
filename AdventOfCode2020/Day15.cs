using System;
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

            int last = 0;
            int index = 6;

            while (index < 29999999)
            {
                if(numbers.TryGetValue(last, out int value))
                {
                    numbers[last] = index;
                    last = index - value;
                }
                else
                {
                    numbers[last] = index;
                    last = 0;
                }
                index++;
            }
            Console.Write(last);
            Console.Read();
        }
    }
}
