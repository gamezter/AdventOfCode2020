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

                numbers[current] = numbers[next3];
                numbers[next3] = numbers[value];
                numbers[value] = next1;

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

        public static void part2()
        {
            int[] numbers = new int[1000001];
            numbers[0] = 0; // not used
            numbers[1] = 9;
            numbers[9] = 8;
            numbers[8] = 7;
            numbers[7] = 5;
            numbers[5] = 3;
            numbers[3] = 4;
            numbers[4] = 6;
            numbers[6] = 2;
            numbers[2] = 10;
            numbers[1000000] = 1;
            for(int i = 10; i < 1000000; ++i)
            {
                numbers[i] = i + 1;
            }

            int current = 1;

            for (int i = 0; i < 10000000; ++i)
            {
                int value = current;
                int next1 = numbers[current];
                int next2 = numbers[next1];
                int next3 = numbers[next2];

                do
                {
                    value--;
                    if (value == 0)
                        value = 1000000;
                }
                while (next1 == value || next2 == value || next3 == value);

                numbers[current] = numbers[next3];
                numbers[next3] = numbers[value];
                numbers[value] = next1;

                current = numbers[current];
            }

            long prod = numbers[1];
            prod *= numbers[numbers[1]];
            Console.Write(prod);
            Console.Read();
        }
    }
}
