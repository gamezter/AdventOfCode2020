using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    class Day24
    {
        public static void part1()
        {
            string[] input = new StreamReader("day24.txt").ReadToEnd().Trim().Split('\n');

            Regex r = new Regex(@"(e|se|sw|w|ne|nw)+");

            HashSet<(int, int, int)> flipped = new HashSet<(int, int, int)>();

            for (int i = 0; i < input.Length; ++i)
            {
                var m = r.Match(input[i]);
                var captures = m.Groups[1].Captures;

                int x = 0, y = 0, z = 0;

                for (int j = 0; j < captures.Count; ++j) {
                    switch (captures[j].Value)
                    {
                        case "e":
                            x++;
                            y--;
                            break;
                        case "se":
                            y--;
                            z++;
                            break;
                        case "sw":
                            x--;
                            z++;
                            break;
                        case "w":
                            x--;
                            y++;
                            break;
                        case "ne":
                            x++;
                            z--;
                            break;
                        case "nw":
                            y++;
                            z--;
                            break;
                    }
                }

                if (!flipped.Remove((x, y, z)))
                    flipped.Add((x, y, z));
            }

            Console.Write(flipped.Count);
            Console.Read();
        }
    }
}
