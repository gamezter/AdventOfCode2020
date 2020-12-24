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

            Regex regex = new Regex(@"(e|se|sw|w|ne|nw)+");

            HashSet<(int, int)> flipped = new HashSet<(int, int)>();

            for (int i = 0; i < input.Length; ++i)
            {
                var m = regex.Match(input[i]);
                var captures = m.Groups[1].Captures;

                int q = 0, r = 0;

                for (int j = 0; j < captures.Count; ++j) {
                    switch (captures[j].Value)
                    {
                        case "e":
                            q++;
                            break;
                        case "se":
                            r++;
                            break;
                        case "sw":
                            q--;
                            r++;
                            break;
                        case "w":
                            q--;
                            break;
                        case "ne":
                            q++;
                            r--;
                            break;
                        case "nw":
                            r--;
                            break;
                    }
                }

                if (!flipped.Remove((q, r)))
                    flipped.Add((q, r));
            }

            Console.Write(flipped.Count);
            Console.Read();
        }

        public static void part2()
        {
            string[] input = new StreamReader("day24.txt").ReadToEnd().Trim().Split('\n');

            Regex regex = new Regex(@"(e|se|sw|w|ne|nw)+");

            int size = 140;
            bool[,] map = new bool[size, size];

            for (int i = 0; i < input.Length; ++i)
            {
                var m = regex.Match(input[i]);
                var captures = m.Groups[1].Captures;

                int q = size/2, r = size / 2;

                for (int j = 0; j < captures.Count; ++j)
                {
                    switch (captures[j].Value)
                    {
                        case "e":
                            q++;
                            break;
                        case "se":
                            r++;
                            break;
                        case "sw":
                            q--;
                            r++;
                            break;
                        case "w":
                            q--;
                            break;
                        case "ne":
                            q++;
                            r--;
                            break;
                        case "nw":
                            r--;
                            break;
                    }
                }

                map[q, r] = !map[q, r]; 
            }

            (int dq, int dr)[] offsets = new[] { (1, -1), (1, 0), (0, 1), (-1, 1), (-1, 0), (0, -1) };

            for (int i = 0; i < 100; ++i)
            {
                bool[,] map2 = new bool[size, size];
                for (int q = 0; q < size; ++q)
                    for (int r = 0; r < size; ++r)
                    {
                        int count = 0;
                        foreach (var (dq, dr) in offsets)
                        {
                            int nq = q + dq;
                            int nr = r + dr;
                            if (nq < 0 || nq >= size || nr < 0 || nr >= size)
                                continue;

                            if (map[nq, nr])
                                count++;
                        }

                        if (map[q, r] && (count == 0 || count > 2))
                            map2[q, r] = false;
                        else if (!map[q, r] && count == 2)
                            map2[q, r] = true;
                        else
                            map2[q, r] = map[q, r];
                    }

                map = map2;
            }

            int total = 0;
            for (int x = 0; x < size; ++x)
                for (int y = 0; y < size; ++y)
                {
                    if (map[x, y])
                        total++;
                }

            Console.Write(total);
            Console.Read();
        }
    }
}
