﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    class Day11
    {
        public static void part1()
        {
            string[] input = new StreamReader("day11.txt").ReadToEnd().Trim().Split();

            int width = input[0].Length;
            int height = input.Length;
            char[,] map = new char[width, height];
            for(int y = 0; y < height; ++y)
            {
                for(int x = 0; x < width; ++x)
                {
                    map[x, y] = input[y][x];
                }
            }

            bool changed = true;
            while (changed)
            {
                changed = false;
                char[,] map2 = new char[width, height];

                for (int y = 0; y < height; ++y)
                {
                    int minY = (y == 0) ? 0 : -1;
                    int maxY = ((y == (height - 1)) ? 0 : 1);
                    for (int x = 0; x < width; ++x)
                    {
                        int minX = (x == 0) ? 0 : -1;
                        int maxX = ((x == (width - 1)) ? 0 : 1);

                        int occupied = 0;

                        for(int dx = minX; dx <= maxX; ++dx)
                        {
                            for(int dy = minY; dy <= maxY; ++dy)
                            {
                                if (dx == 0 && dy == 0)
                                    continue;
                                if (map[x + dx, y + dy] == '#')
                                    occupied++;
                            }
                        }

                        if (occupied == 0 && map[x, y] == 'L')
                        {
                            map2[x, y] = '#';
                            changed = true;
                        }
                        else if(occupied > 3 && map[x, y] == '#')
                        {
                            map2[x, y] = 'L';
                            changed = true;
                        }
                        else
                        {
                            map2[x, y] = map[x, y];
                        }
                    }
                }

                map = map2;
            }

            int count = 0;
            for (int y = 0; y < map.GetLength(1); ++y)
            {
                for (int x = 0; x < map.GetLength(0); ++x)
                {
                    if (map[x, y] == '#')
                        count++;
                }
            }

            Console.Write(count);
            Console.Read();
        }

        public static void part2()
        {
            string[] input = new StreamReader("day11.txt").ReadToEnd().Trim().Split();

            int width = input[0].Length;
            int height = input.Length;
            char[,] map = new char[width, height];
            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    map[x, y] = input[y][x];
                }
            }

            bool changed = true;
            while (changed)
            {
                changed = false;
                char[,] map2 = new char[width, height];

                for (int y = 0; y < height; ++y)
                {
                    for (int x = 0; x < width; ++x)
                    {
                        int occupied = 0;

                        for (int dx = -1; dx <= 1; ++dx)
                        {
                            for (int dy = -1; dy <= 1; ++dy)
                            {
                                if (dx == 0 && dy == 0)
                                    continue;

                                int nx = x + dx;
                                int ny = y + dy;
                                while (nx >= 0 && nx < width && ny >= 0 && ny < height)
                                {
                                    if (map[nx, ny] == '#')
                                    {
                                        occupied++;
                                        break;
                                    }
                                    if (map[nx, ny] == 'L')
                                        break;
                                    nx += dx;
                                    ny += dy;
                                }
                            }
                        }

                        if (occupied == 0 && map[x, y] == 'L')
                        {
                            map2[x, y] = '#';
                            changed = true;
                        }
                        else if (occupied > 4 && map[x, y] == '#')
                        {
                            map2[x, y] = 'L';
                            changed = true;
                        }
                        else
                        {
                            map2[x, y] = map[x, y];
                        }
                    }
                }

                map = map2;
            }

            int count = 0;
            for (int y = 0; y < map.GetLength(1); ++y)
            {
                for (int x = 0; x < map.GetLength(0); ++x)
                {
                    if (map[x, y] == '#')
                        count++;
                }
            }

            Console.Write(count);
            Console.Read();
        }
    }
}