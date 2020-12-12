using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2020
{
    class Day12
    {
        public static void part1()
        {
            string[] input = new StreamReader("day12.txt").ReadToEnd().Trim().Split();

            int x = 0; 
            int y = 0;
            int d = 1;//0 : N, 1 : E, 2 : S, 3 : W

            foreach(var line in input)
            {
                int number = int.Parse(line.Substring(1));

                switch (line[0])
                {
                    case 'N':
                        y -= number;
                        break;
                    case 'S':
                        y += number;
                        break;
                    case 'E':
                        x += number;
                        break;
                    case 'W':
                        x -= number;
                        break;
                    case 'L':
                        d -= number / 90;
                        if (d < 0)
                            d += 4;
                        break;
                    case 'R':
                        d = (d + number / 90) % 4;
                        break;
                    case 'F':
                        if (d == 0)
                            y -= number;
                        else if (d == 1)
                            x += number;
                        else if (d == 2)
                            y += number;
                        else if (d == 3)
                            x -= number;
                        break;
                }
            }

            Console.Write(Math.Abs(x) + Math.Abs(y));
            Console.Read();
        }

        public static void part2()
        {
            string[] input = new StreamReader("day12.txt").ReadToEnd().Trim().Split();

            int sx = 0;
            int sy = 0;
            int wx = 10;
            int wy = -1;

            foreach (var line in input)
            {
                int number = int.Parse(line.Substring(1));

                switch (line[0])
                {
                    case 'N':
                        wy -= number;
                        break;
                    case 'S':
                        wy += number;
                        break;
                    case 'E':
                        wx += number;
                        break;
                    case 'W':
                        wx -= number;
                        break;
                    case 'L':
                        {
                            int nx = 0, ny = 0;
                            if(number == 90)
                            {
                                nx = wy;
                                ny = -wx;
                            }
                            else if(number == 180)
                            {
                                nx = -wx;
                                ny = -wy;
                            }
                            else if(number == 270)
                            {
                                nx = -wy;
                                ny = wx;
                            }
                            wx = nx;
                            wy = ny;
                        }
                        break;
                    case 'R':
                        {
                            int nx = 0, ny = 0;
                            if (number == 90)
                            {
                                nx = -wy;
                                ny = wx;
                            }
                            else if (number == 180)
                            {
                                nx = -wx;
                                ny = -wy;
                            }
                            else if (number == 270)
                            {
                                nx = wy;
                                ny = -wx;
                            }
                            wx = nx;
                            wy = ny;
                        }
                        break;
                    case 'F':
                        sx += wx * number;
                        sy += wy * number;
                        break;
                }
            }

            Console.Write(Math.Abs(sx) + Math.Abs(sy));
            Console.Read();
        }
    }
}
