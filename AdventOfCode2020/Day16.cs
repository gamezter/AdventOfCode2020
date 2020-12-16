using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    class Day16
    {
        public static void part1()
        {
            string[] input = new StreamReader("day16.txt").ReadToEnd().Trim().Split('\n');
            (int min, int max, int min2, int max2)[] rules = new (int min, int max2, int min2, int max)[20];
            Regex r = new Regex(@"(\w*): (\d*)-(\d*) or (\d*)-(\d*)");
            for(int i = 0; i < 20; ++i)
            {
                Match m = r.Match(input[i]);
                rules[i].min = int.Parse(m.Groups[2].Value);
                rules[i].max = int.Parse(m.Groups[3].Value);
                rules[i].min2 = int.Parse(m.Groups[4].Value);
                rules[i].max2 = int.Parse(m.Groups[5].Value);
            }

            int sum = 0;
            for(int i = 25; i < input.Length; ++i)
            {
                string[] ticket = input[i].Split(',');
                foreach(var number in ticket)
                {
                    int value = int.Parse(number);

                    foreach(var (min, max, min2, max2) in rules)
                    {
                        if ((value >= min && value <= max) || (value >= min2 && value <= max2))
                            goto skipNumber;
                    }
                    sum += value;
                    skipNumber:;
                }
            }
            Console.Write(sum);
            Console.Read();
        }

        public static void part2()
        {
            string[] input = new StreamReader("day16.txt").ReadToEnd().Trim().Split('\n');
            (int min, int max, int min2, int max2)[] rules = new (int min, int max2, int min2, int max)[20];
            Regex r = new Regex(@"(\w*): (\d*)-(\d*) or (\d*)-(\d*)");
            for (int i = 0; i < 20; ++i)
            {
                Match m = r.Match(input[i]);
                rules[i].min = int.Parse(m.Groups[2].Value);
                rules[i].max = int.Parse(m.Groups[3].Value);
                rules[i].min2 = int.Parse(m.Groups[4].Value);
                rules[i].max2 = int.Parse(m.Groups[5].Value);
            }

            List<int>[] validNumbers = new List<int>[rules.Length];
            for (int i = 0; i < validNumbers.Length; ++i)
                validNumbers[i] = new List<int>();

            for (int i = 25; i < input.Length; ++i)
            {
                string[] ticket = input[i].Split(',');
                int[] numbers = new int[ticket.Length];
                for(int j = 0; j < ticket.Length; ++j)
                {
                    numbers[j] = int.Parse(ticket[j]);
                    foreach (var (min, max, min2, max2) in rules)
                    {
                        if ((numbers[j] >= min && numbers[j] <= max) || (numbers[j] >= min2 && numbers[j] <= max2))
                            goto skipNumber; // if number matches a rule, skip it
                    }
                    goto skipTicket; //if a number matched none of the rules, skip the ticket
                    skipNumber:;
                }

                for (int j = 0; j < validNumbers.Length; ++j)
                    validNumbers[j].Add(numbers[j]);
                skipTicket:;
            }

            foreach(var v in validNumbers)
            {
                v.Sort();
            }

            bool[,] grid = new bool[rules.Length, validNumbers.Length];

            for(int i = 0; i < rules.Length; ++i)
            {
                (int min, int max, int min2, int max2) = rules[i];

                for(int j = 0; j < validNumbers.Length; ++j)
                {
                    foreach(var number in validNumbers[j])
                    {
                        if ((number < min || number > max) && (number < min2 || number > max2))
                            goto skipNumbers;
                    }
                    grid[i, j] = true;
                    skipNumbers:;
                }
            }

            for(int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                    Console.Write(grid[i, j] ? "X ": "O ");
                Console.WriteLine();
            }


            // following has been solved by hand
            /*
             * 0  | O X O X X O O O X X O O X O O X O O O O
             * 1  | X X O X X X O O X X O O X O X X O X O O
             * 2  | O X O X X X O O X X O O X O O X O O O O
             * 3  | X X O X X X O O X X O O X O X X O O O O
             * 4  | O X O X X X O O X X O O X O X X O O O O
             * 5  | X X O X X X O O X X X O X O X X O X O O
             * 6  | X X O X X X X O X X X O X O X X X X O X
             * 7  | O X O O O O O O O X O O O O O O O O O O
             * 8  | X X X X X X X X X X X O X X X X X X X X
             * 9  | X X O X X X X X X X X O X X X X X X X X
             * 10 | O X O X X O O O X X O O X O O O O O O O
             * 11 | O X O O O O O O O X O O X O O O O O O O
             * 12 | X X X X X X X X X X X X X X X X X X X X
             * 13 | X X O X X X X X X X X O X O X X X X O X
             * 14 | O X O O O O O O O O O O O O O O O O O O
             * 15 | O X O X X O O O O X O O X O O O O O O O
             * 16 | O X O X O O O O O X O O X O O O O O O O
             * 17 | X X O X X X X O X X X O X O X X O X O X
             * 18 | X X O X X X X X X X X O X X X X X X O X
             * 19 | X X O X X X X O X X X O X O X X O X O O
             */

            /*
             * rule 12 -> 11
             * rule 8 -> 2
             * rule 9 -> 18
             * rule 18 -> 13
             * rule 13 -> 7
             * rule 6 -> 16
             * rule 17 -> 19
             * rule 19 -> 6
             * rule 5 -> 10
             * rule 1 -> 17
             * rule 3 -> 0
             * rule 4 -> 14
             * rule 2 -> 5
             * rule 0 -> 15
             * rule 10 -> 8
             * rule 15 -> 4
             * rule 16 -> 3
             * rule 11 -> 12
             * rule 7 -> 9
             * rule 14 -> 1
             * */

            int[] mapping = new[] { 15, 17, 5, 0, 14, 10, 16, 9, 2, 18, 8, 12, 11, 7, 1, 4, 3, 19, 13, 6 };

            string[] myTicket = input[22].Split(',');

            long mul = 1;

            for(int i = 0; i < 6; ++i)
            {
                mul *= int.Parse(myTicket[mapping[i]]);
            }

            Console.Write(mul);
            Console.Read();
        }
    }
}
