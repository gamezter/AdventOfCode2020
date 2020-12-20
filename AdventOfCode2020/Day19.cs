using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    class Day19
    {
        public static void part1()
        {
            //place rules in Chomsky normal form and hardcode a and b terminals
            string[] input = new StreamReader("day19.txt").ReadToEnd().Trim().Split('\n');
            int[][] rules = new int[137][];

            Regex nonTerminals = new Regex(@"(?<index>\d+): (?<r1>\d+) (?<r2>\d+)( \| (?<r3>\d+) (?<r4>\d+))?");

            for(int i = 0; i < rules.Length; ++i)
            {
                var m = nonTerminals.Match(input[i]);
                if (m.Success)
                {
                    int index = int.Parse(m.Groups["index"].Value);
                    int r1 = int.Parse(m.Groups["r1"].Value);
                    int r2 = int.Parse(m.Groups["r2"].Value);
                    if(int.TryParse(m.Groups["r3"].Value, out int r3))
                    {
                        int r4 = int.Parse(m.Groups["r4"].Value);
                        rules[index] = new[] { r1, r2, r3, r4 };
                    }
                    else
                    {
                        rules[index] = new[] { r1, r2 };
                    }
                }
            }

            bool parse(string s)
            {
                List<int>[][] p = new List<int>[s.Length][];

                for(int i = 0; i < s.Length; ++i)
                {
                    p[i] = new List<int>[s.Length - i];
                    if(s[i] == 'a')
                    {
                        p[0][i] = new List<int> { 58, 64 }; // hard coded
                    }
                    else if(s[i] == 'b')
                    {
                        p[0][i] = new List<int> { 14, 58 }; // hard coded
                    }
                }

                //use cyk algorithm

                for(int i = 1; i < p.Length; ++i)
                for(int j = 0; j < p[i].Length; ++j)
                for(int k = 0; k < i; ++k)
                {
                    if (p[k][j] == null || p[i - k - 1][j + k + 1] == null)
                        continue;

                    var a = p[k][j];
                    var b = p[i - k - 1][j + k + 1];

                    for (int l = 0; l < rules.Length; ++l)
                    {
                        if (rules[l] == null)
                            continue;

                        if (a.Contains(rules[l][0]) && b.Contains(rules[l][1]))
                        {
                            if (p[i][j] == null)
                                p[i][j] = new List<int> { l };
                            else
                                p[i][j].Add(l);
                        }
                        if (rules[l].Length == 4)
                        {
                            if (a.Contains(rules[l][2]) && b.Contains(rules[l][3]))
                            {
                                if (p[i][j] == null)
                                    p[i][j] = new List<int> { l };
                                else
                                    p[i][j].Add(l);
                            }
                        }
                    }
                }


                return p[s.Length - 1][0] != null;
            }


            int count = 0;
            for(int i = 138; i < input.Length; ++i)
            {
                if (parse(input[i]))
                    count++;
            }

            Console.Write(count);
            Console.Read();
        }

        public static void part2()
        {
            //place rules in Chomsky normal form and hardcode a and b terminals
            string[] input = new StreamReader("day19.txt").ReadToEnd().Trim().Split('\n');
            int[][] rules = new int[138][];

            Regex nonTerminals = new Regex(@"(?<index>\d+): (?<r1>\d+) (?<r2>\d+)( \| (?<r3>\d+) (?<r4>\d+))?( \| (?<r5>\d+) (?<r6>\d+))?");

            for (int i = 0; i < rules.Length; ++i)
            {
                var m = nonTerminals.Match(input[i]);
                if (m.Success)
                {
                    int index = int.Parse(m.Groups["index"].Value);
                    int r1 = int.Parse(m.Groups["r1"].Value);
                    int r2 = int.Parse(m.Groups["r2"].Value);
                    if (int.TryParse(m.Groups["r5"].Value, out int r5))
                    {
                        int r3 = int.Parse(m.Groups["r3"].Value);
                        int r4 = int.Parse(m.Groups["r4"].Value);
                        int r6 = int.Parse(m.Groups["r6"].Value);
                        rules[index] = new[] { r1, r2, r3, r4, r5, r6 };
                    }
                    else if (int.TryParse(m.Groups["r3"].Value, out int r3))
                    {
                        int r4 = int.Parse(m.Groups["r4"].Value);
                        rules[index] = new[] { r1, r2, r3, r4 };
                    }
                    else
                    {
                        rules[index] = new[] { r1, r2 };
                    }
                }
            }

            bool parse(string s)
            {
                List<int>[][] p = new List<int>[s.Length][];

                for (int i = 0; i < s.Length; ++i)
                {
                    p[i] = new List<int>[s.Length - i];
                    if (s[i] == 'a')
                    {
                        p[0][i] = new List<int> { 58, 64 }; // hard coded
                    }
                    else if (s[i] == 'b')
                    {
                        p[0][i] = new List<int> { 14, 58 }; // hard coded
                    }
                }

                //use cyk algorithm

                for (int i = 1; i < p.Length; ++i)
                for (int j = 0; j < p[i].Length; ++j)
                for (int k = 0; k < i; ++k)
                {
                    if (p[k][j] == null || p[i - k - 1][j + k + 1] == null)
                        continue;

                    var a = p[k][j];
                    var b = p[i - k - 1][j + k + 1];

                    for (int l = 0; l < rules.Length; ++l)
                    {
                        if (rules[l] == null)
                            continue;

                        if (a.Contains(rules[l][0]) && b.Contains(rules[l][1]))
                        {
                            if (p[i][j] == null)
                                p[i][j] = new List<int> { l };
                            else
                                p[i][j].Add(l);
                        }
                        if (rules[l].Length >= 4)
                        {
                            if (a.Contains(rules[l][2]) && b.Contains(rules[l][3]))
                            {
                                if (p[i][j] == null)
                                    p[i][j] = new List<int> { l };
                                else
                                    p[i][j].Add(l);
                            }
                        }
                        if (rules[l].Length == 6)
                        {
                            if (a.Contains(rules[l][4]) && b.Contains(rules[l][5]))
                            {
                                if (p[i][j] == null)
                                    p[i][j] = new List<int> { l };
                                else
                                    p[i][j].Add(l);
                            }
                        }
                    }
                }

                return p[s.Length - 1][0] != null;
            }


            int count = 0;
            for (int i = 139; i < input.Length; ++i)
            {
                if (parse(input[i]))
                    count++;
            }

            Console.Write(count);
            Console.Read();
        }
    }
}
