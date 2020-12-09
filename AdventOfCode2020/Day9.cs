using System;
using System.IO;

namespace AdventOfCode2020
{
    class Day9
    {
        public static void part1()
        {
            string[] input = new StreamReader("day9.txt").ReadToEnd().Trim().Split();
            long[] numbers = new long[input.Length];
            for (int i = 0; i < numbers.Length; ++i)
                numbers[i] = long.Parse(input[i]);

            for(int i = 25; i < numbers.Length; ++i)
            {
                for(int j = i - 25; j < i - 1; ++j)
                {
                    for(int k = j + 1; k < i; ++k)
                    {
                        if (numbers[j] + numbers[k] == numbers[i])
                            goto skip;
                    }
                }
                Console.Write(numbers[i]);
                Console.Read();
                skip:;
            }
        }

        public static void part2()
        {
            string[] input = new StreamReader("day9.txt").ReadToEnd().Trim().Split();
            long[] numbers = new long[input.Length];
            for (int i = 0; i < numbers.Length; ++i)
                numbers[i] = long.Parse(input[i]);
            long target = 88311122;

            for(int minIndex = 0; minIndex < numbers.Length; ++minIndex)
            {
                long sum = 0;
                int maxIndex = minIndex;
                while (sum < target && maxIndex < numbers.Length)
                {
                    sum += numbers[maxIndex++];
                }

                if (sum == target)
                {
                    long min = long.MaxValue;
                    long max = 0;
                    for (int i = minIndex; i <= maxIndex; i++)
                    {
                        if (numbers[i] < min)
                            min = numbers[i];
                        if (numbers[i] > max)
                            max = numbers[i];
                    }

                    Console.WriteLine(min + max);
                    Console.Read();
                }
            }
        }
    }
}
