using System;
using System.IO;

namespace AdventOfCode2020
{
    class Day10
    {
        public static void part1()
        {
            string[] input = new StreamReader("day10.txt").ReadToEnd().Trim().Split();
            int[] numbers = new int[input.Length];
            for (int i = 0; i < numbers.Length; ++i)
                numbers[i] = int.Parse(input[i]);

            Array.Sort(numbers);

            int oneJolt = 0;
            int threeJolt = 0;
            for(int i = 0; i < numbers.Length - 1; ++i)
            {
                int diff = numbers[i + 1] - numbers[i];
                if (diff == 1)
                    oneJolt++;
                if (diff == 3)
                    threeJolt++;
            }
            oneJolt++; // from 0 to 1
            threeJolt++; // last jolt jump is always 3
            Console.Write(oneJolt * threeJolt);
            Console.Read();
        }

        public static void part2()
        {
            string[] input = new StreamReader("day10.txt").ReadToEnd().Trim().Split();
            int[] numbers = new int[input.Length];
            for (int i = 0; i < numbers.Length; ++i)
                numbers[i] = int.Parse(input[i]);

            Array.Sort(numbers);

            long[] nVariants = new long[numbers.Length];
            nVariants[0] = 1; // (0) 1
            nVariants[1] = 2; // (0) 2 | (0) 1 2
            nVariants[2] = 4; // (0) 3 | (0) 1 3 | (0) 2 3 | (0) 1 2 3

            for(int i = 3; i < numbers.Length; ++i)
            {
                long sum = 0;
                if (numbers[i] - numbers[i - 3] <= 3)
                    sum += nVariants[i - 3];
                if (numbers[i] - numbers[i - 2] <= 3)
                    sum += nVariants[i - 2];
                if (numbers[i] - numbers[i - 1] <= 3)
                    sum += nVariants[i - 1];
                nVariants[i] = sum;
            }

            Console.Write(nVariants[nVariants.Length - 1]);
            Console.Read();
        }
    }
}
