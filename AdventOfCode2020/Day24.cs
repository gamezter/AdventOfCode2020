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

            Regex r = new Regex(@"(e|se|sw|w|ne|nw)+");

            int size = 140;
            bool[,,] map = new bool[size, size, size];

            for (int i = 0; i < input.Length; ++i)
            {
                var m = r.Match(input[i]);
                var captures = m.Groups[1].Captures;

                int x = size/2, y = size / 2, z = size / 2;

                for (int j = 0; j < captures.Count; ++j)
                {
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

                map[x, y, z] = !map[x, y, z]; 
            }

            (int dx, int dy, int dz)[] offsets = new[] { (0, 1, -1), (1, 0, -1), (1, -1, 0), (0, -1, 1), (-1, 0, 1), (-1, 1, 0) };

            for (int i = 0; i < 100; ++i)
            {
                bool[,,] map2 = new bool[size, size, size];
                for (int x = 0; x < size; ++x)
                    for (int y = 0; y < size; ++y)
                        for (int z = 0; z < size; ++z)
                        {
                            int count = 0;
                            foreach (var (dx, dy, dz) in offsets)
                            {
                                int nx = x + dx;
                                int ny = y + dy;
                                int nz = z + dz;
                                if (nx < 0 || nx >= size || ny < 0 || ny >= size || nz < 0 || nz >= size)
                                    continue;

                                if (map[nx, ny, nz])
                                    count++;
                            }

                            if (map[x, y, z] && (count == 0 || count > 2))
                                map2[x, y, z] = false;
                            else if (!map[x, y, z] && count == 2)
                                map2[x, y, z] = true;
                            else
                                map2[x, y, z] = map[x, y, z];
                        }

                map = map2;
            }

            int total = 0;
            for (int x = 0; x < size; ++x)
                for (int y = 0; y < size; ++y)
                    for (int z = 0; z < size; ++z)
                    {
                        if (map[x, y, z])
                            total++;
                    }

            Console.Write(total);
            Console.Read();
        }
    }
}
