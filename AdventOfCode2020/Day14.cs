using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    class Day14
    {
        public static void part1()
        {
            string[] input = new StreamReader("day14.txt").ReadToEnd().Trim().Split('\n');
            int[] mask = new int[36];

            Dictionary<int, ulong> memory = new Dictionary<int, ulong>();

            foreach(var line in input)
            {
                if(line[1] == 'a') // mask
                {
                    string m = line.Substring(7);
                    for(int i = 0; i < mask.Length; ++i)
                    {
                        switch (m[mask.Length - 1 - i])
                        {
                            case '1':
                                mask[i] = 1;
                                break;
                            case '0':
                                mask[i] = 0;
                                break;
                            case 'X':
                                mask[i] = -1;
                                break;
                        }
                    }
                }
                else // mem
                {
                    int address = int.Parse(line.Substring(4, line.IndexOf(']') - 4));
                    ulong value = ulong.Parse(line.Substring(line.IndexOf('=') + 2));

                    for(int i = 0; i < mask.Length; ++i)
                    {
                        if (mask[i] == 1)
                        {
                            value |= (1UL << i);
                        }
                        else if (mask[i] == 0)
                        {
                            value &= (~(1UL << i));
                        }
                    }

                    memory[address] = value;
                }
            }

            ulong sum = 0;
            foreach(var v in memory)
            {
                sum += v.Value;
            }
            Console.Write(sum);
            Console.Read();
        }

        public static void part2()
        {
            string[] input = new StreamReader("day14.txt").ReadToEnd().Trim().Split('\n');
            List<int[]> masks = new List<int[]>();

            Dictionary<ulong, int> memory = new Dictionary<ulong, int>();

            foreach (var line in input)
            {
                if (line[1] == 'a') // mask
                {
                    string m = line.Substring(7);
                    masks.Clear();
                    masks.Add(new int[36]);

                    for (int i = m.Length - 1; i >= 0; --i)
                    {
                        if(m[i] == '1')
                        {
                            for(int n = 0; n < masks.Count; ++n)
                            {
                                masks[n][m.Length - 1 - i] = 1;
                            }
                        }
                        else if(m[i] == '0')
                        {
                            for (int n = 0; n < masks.Count; ++n)
                            {
                                masks[n][m.Length - 1 - i] = -1;
                            }
                        }
                        else if(m[i] == 'X')
                        {
                            int c = masks.Count;
                            for(int n = 0; n < c; ++n)
                            {
                                var t = new int[36];
                                Array.Copy(masks[n], t, 36);
                                t[m.Length - 1 - i] = 1;
                                masks.Add(t);
                            }
                        }
                    }
                }
                else // mem
                {
                    ulong address = uint.Parse(line.Substring(4, line.IndexOf(']') - 4));
                    int value = int.Parse(line.Substring(line.IndexOf('=') + 2));

                    foreach(var mask in masks)
                    {
                        for (int i = 0; i < mask.Length; ++i)
                        {
                            if (mask[i] == 1)
                            {
                                address |= (1UL << i);
                            }
                            else if (mask[i] == 0)
                            {
                                address &= (~(1UL << i));
                            }
                        }

                        memory[address] = value;
                    }
                }
            }

            long sum = 0;
            foreach (var v in memory)
            {
                sum += v.Value;
            }
            Console.Write(sum);
            Console.Read();
        }
    }
}
