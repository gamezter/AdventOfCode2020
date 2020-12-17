using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    class Day17
    {
        public static void part1()
        {
            string[] input = new StreamReader("day17.txt").ReadToEnd().Trim().Split('\n');
            int size = 18;

            char[,,] map = new char[size, size, size];

            for(int y = 5; y < 13; ++y)
            {
                for(int x = 5; x < 13; ++x)
                {
                    map[x, y, 9] = input[y - 5][x - 5] == '#' ? '#' : (char)0;
                }
            }

            List<(int dx, int dy, int dz)> offsets = new List<(int dx, int dy, int dz)>();

            for (int dx = -1; dx <= 1; ++dx)
            for (int dy = -1; dy <= 1; ++dy)
            for (int dz = -1; dz <= 1; ++dz)
            {
                if (dx == 0 && dy == 0 && dz == 0)
                    continue;
                offsets.Add((dx, dy, dz));
            }

            for (int i = 0; i < 6; ++i)
            {
                char[,,] map2 = new char[size, size, size];
                for (int x = 0; x < size; ++x)
                for (int y = 0; y < size; ++y)
                for (int z = 0; z < size; ++z)
                {
                    int count = 0;
                    foreach(var (dx, dy, dz) in offsets)
                    {
                        int nx = x + dx;
                        int ny = y + dy;
                        int nz = z + dz;
                        if (nx < 0 || nx >= size || ny < 0 || ny >= size || nz < 0 || nz >= size)
                            continue;

                        if (map[nx, ny, nz] == '#')
                            count++;
                    }

                    if (map[x, y, z] == '#' && (count == 2 || count == 3))
                        map2[x, y, z] = '#';
                    else if (map[x, y, z] == 0 && count == 3)
                        map2[x, y, z] = '#';
                }

                map = map2;
            }

            int total = 0;
            for (int x = 0; x < size; ++x)
            for (int y = 0; y < size; ++y)
            for (int z = 0; z < size; ++z)
            {
                if (map[x, y, z] == '#')
                    total++;
            }

            Console.Write(total);
            Console.Read();
        }

        public static void part2()
        {
            string[] input = new StreamReader("day17.txt").ReadToEnd().Trim().Split('\n');
            int size = 18;

            char[,,,] map = new char[size, size, size, size];

            for (int y = 5; y < 13; ++y)
            {
                for (int x = 5; x < 13; ++x)
                {
                    map[x, y, 9, 9] = input[y - 5][x - 5] == '#' ? '#' : (char)0;
                }
            }

            List<(int dx, int dy, int dz, int dw)> offsets = new List<(int dx, int dy, int dz, int dw)>();

            for (int dx = -1; dx <= 1; ++dx)
            for (int dy = -1; dy <= 1; ++dy)
            for (int dz = -1; dz <= 1; ++dz)
            for (int dw = -1; dw <= 1; ++dw)
            {
                if (dx == 0 && dy == 0 && dz == 0 && dw == 0)
                    continue;
                offsets.Add((dx, dy, dz, dw));
            }

            for (int i = 0; i < 6; ++i)
            {
                char[,,,] map2 = new char[size, size, size, size];
                for (int x = 0; x < size; ++x)
                for (int y = 0; y < size; ++y)
                for (int z = 0; z < size; ++z)
                for (int w = 0; w < size; ++w)
                {
                    int count = 0;
                    foreach (var (dx, dy, dz, dw) in offsets)
                    {
                        int nx = x + dx;
                        int ny = y + dy;
                        int nz = z + dz;
                        int nw = w + dw;
                        if (nx < 0 || nx >= size || ny < 0 || ny >= size || nz < 0 || nz >= size || nw < 0 || nw >= size)
                            continue;

                        if (map[nx, ny, nz, nw] == '#')
                            count++;
                    }

                    if (map[x, y, z, w] == '#' && (count == 2 || count == 3))
                        map2[x, y, z, w] = '#';
                    else if (map[x, y, z, w] == 0 && count == 3)
                        map2[x, y, z, w] = '#';
                }

                map = map2;
            }

            int total = 0;
            for (int x = 0; x < size; ++x)
            for (int y = 0; y < size; ++y)
            for (int z = 0; z < size; ++z)
            for (int w = 0; w < size; ++w)
            {
                if (map[x, y, z, w] == '#')
                    total++;
            }

            Console.Write(total);
            Console.Read();
        }
    }
}
