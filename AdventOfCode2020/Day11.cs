using System;
using System.IO;

namespace AdventOfCode2020
{
    class Day11
    {
        private static (int dx, int dy)[] offsets = new []{ (-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1) };
        public static void part1()
        {
            string[] input = new StreamReader("day11.txt").ReadToEnd().Trim().Split();

            int width = input[0].Length + 2;
            int height = input.Length + 2;
            char[,] map = new char[width, height];
            for(int y = 1; y < height - 1; ++y)
            {
                for (int x = 1; x < width - 1; ++x)
                {
                    map[x, y] = input[y - 1][x - 1];
                }
            }

            bool changed = true;
            while (changed)
            {
                changed = false;
                char[,] map2 = new char[width, height];

                for (int y = 1; y < height - 1; ++y)
                {
                    for (int x = 1; x < width - 1; ++x)
                    {
                        int occupied = 0;

                        foreach(var (dx, dy) in offsets)
                        {
                            if (map[x + dx, y + dy] == '#')
                                occupied++;
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
            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
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

                        foreach(var (dx, dy) in offsets)
                        {
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
            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
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
