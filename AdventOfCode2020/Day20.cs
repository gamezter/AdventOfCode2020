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

            for (int i = 0; i < input.Length; ++i)
            {
                if (input[i][0] == 'T')
                {
                    int[] sides = new int[4];
                    for (int j = 0; j < 10; ++j)
                    {
                        if (input[i + 1][j] == '#') // top
                        {
                            sides[0] += 1 << (9 - j);
                        }
                        if (input[i + 1 + j][9] == '#') // right
                        {
                            sides[1] += 1 << (9 - j);
                        }
                        if (input[i + 10][j] == '#') // bottom
                        {
                            sides[2] += 1 << (9 - j);
                        }
                        if (input[i + 1 + j][0] == '#') // left
                        {
                            sides[3] += 1 << (9 - j);
                        }
                    }

                    tiles.Add((int.Parse(input[i].Substring(5, 4)), sides, new List<int>()));
                }
            }

            for (int i = 0; i < tiles.Count - 1; ++i)
            {
                var currentTile = tiles[i];

                for (int j = i + 1; j < tiles.Count; ++j)
                {
                    var otherTile = tiles[j];

                    for (int side = 0; side < 4; ++side)
                    {
                        int hash = currentTile.sides[side];
                        int reverseHash = getReverse(hash);
                        for (int side2 = 0; side2 < 4; ++side2)
                        {
                            int otherHash = otherTile.sides[side2];
                            if (hash == otherHash || reverseHash == otherHash)
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

        public static int getReverse(int x)
        {
            x = ((x & 0x01F) << 5) | ((x & 0x3E0) >> 5);
            x = ((x & 0x63) << 3) | ((x & 0x318) >> 3) | (x & 0x84);
            x = ((x & 0x129) << 1) | ((x & 0x252) >> 1) | (x & 0x84);
            return x;
        }
    }
}
