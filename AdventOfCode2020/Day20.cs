using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2020
{
    class Day20
    {
        public static void part1()
        {
            string[] input = new StreamReader("day20.txt").ReadToEnd().Trim().Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            List<(int id, int[] sides, List<int> neighbors)> tiles = new List<(int id, int[] sides, List<int> neighbors)>();

            for(int i = 0; i < input.Length; ++i)
            {
                if(input[i][0] == 'T')
                {
                    int[] sides = new int[8];
                    for(int j = 0; j < 10; ++j) // top
                    {
                        if(input[i + 1][j] == '#')
                        {
                            sides[0] += 1 << (9 - j);
                            sides[1] += 1 << j;
                        }
                    }
                    for (int j = 0; j < 10; ++j) // right
                    {
                        if (input[i + 1 + j][9] == '#')
                        {
                            sides[2] += 1 << (9 - j);
                            sides[3] += 1 << j;
                        }
                    }
                    for (int j = 0; j < 10; ++j) // bottom
                    {
                        if (input[i + 10][j] == '#')
                        {
                            sides[4] += 1 << j;
                            sides[5] += 1 << (9 - j);
                        }
                    }
                    for (int j = 0; j < 10; ++j) // left
                    {
                        if (input[i + 1 + j][0] == '#')
                        {
                            sides[6] += 1 << j;
                            sides[7] += 1 << (9 - j);
                        }
                    }

                    tiles.Add((int.Parse(input[i].Substring(5, 4)), sides, new List<int>()));
                }
            }

            for(int i = 0; i < tiles.Count - 1; ++i)
            {
                var currentTile = tiles[i];

                for(int j = i + 1; j < tiles.Count; ++j)
                {
                    var otherTile = tiles[j];

                    foreach(int side in currentTile.sides)
                    {
                        foreach(int side2 in otherTile.sides)
                        {
                            if(side == side2)
                            {
                                currentTile.neighbors.Add(otherTile.id);
                                otherTile.neighbors.Add(currentTile.id);
                                goto nextTile;
                            }
                        }
                    }
                    nextTile:;
                }
            }

            long product = 1;

            foreach(var (id, sides, neighbors) in tiles)
            {
                if (neighbors.Count == 2)
                    product *= id;
            }

            Console.Write(product);
            Console.Read();
        }
    }
}
