using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    class Day23
    {
        public static void part1()
        {
            int[] numbers = new int[] { 0, 9, 1, 4, 6, 3, 2, 5, 7, 8 };

            int current = 1;

            for (int i = 0; i < 100; ++i)
            {
                int value = current;
                int next1 = numbers[current];
                int next2 = numbers[next1];
                int next3 = numbers[next2];

                do
                {
                    value--;
                    if (value == 0)
                        value = 9;
                }
                while (next1 == value || next2 == value || next3 == value);

                int search = numbers[next3];
                while (search != value)
                    search = numbers[search];

                numbers[current] = numbers[next3];
                numbers[next3] = numbers[search];
                numbers[search] = next1;

                current = numbers[current];
            }

            current = numbers[1];
            for (int i = 0; i < 8; ++i)
            {
                Console.Write(current);
                current = numbers[current];
            }
            Console.Read();
        }
    }
}
