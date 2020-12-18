using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    class Day18
    {
        public static void part1()
        {
            string[] input = new StreamReader("day18.txt").ReadToEnd().Trim().Split('\n');

            long solve(string s, ref int i)
            {
                bool add = true;

                long total = 0;

                while (i < s.Length && s[i] != ')')
                {
                    char c = s[i++];
                    switch (c)
                    {
                        case ' ': break;
                        case '+':
                            add = true;
                            break;
                        case '*':
                            add = false;
                            break;
                        case '(':
                            if (add)
                                total += solve(s, ref i);
                            else
                                total *= solve(s, ref i);
                            break;
                        default:
                            if (add)
                                total += (c - 48);
                            else
                                total *= (c - 48);
                            break;
                    }
                }

                i++;
                return total;
            }

            long sum = 0;
            foreach(var line in input)
            {
                int i = 0;
                long v = solve(line, ref i);
                sum += v;
            }

            Console.Write(sum);
            Console.Read();
        }

        public static void part2()
        {
            string[] input = new StreamReader("day18.txt").ReadToEnd().Trim().Split('\n');

            long solve(string s, ref int i)
            {
                bool add = true;

                Stack<long> numbers = new Stack<long>();
                numbers.Push(0);

                while (i < s.Length && s[i] != ')')
                {
                    char c = s[i++];
                    switch (c)
                    {
                        case ' ': break;
                        case '+':
                            add = true;
                            break;
                        case '*':
                            add = false;
                            break;
                        case '(':
                            {
                                long value = solve(s, ref i);
                                if (add)
                                    value += numbers.Pop();
                                numbers.Push(value);
                            }
                            break;
                        default:
                            {
                                long value = (c - 48);
                                if (add)
                                    value += numbers.Pop();
                                numbers.Push(value);
                            }
                            break;
                    }
                }
                long total = 1;

                while (numbers.Count > 0)
                    total *= numbers.Pop();

                i++;
                return total;
            }

            long sum = 0;
            foreach (var line in input)
            {
                int i = 0;
                sum += solve(line, ref i);
            }

            Console.Write(sum);
            Console.Read();
        }
    }
}
