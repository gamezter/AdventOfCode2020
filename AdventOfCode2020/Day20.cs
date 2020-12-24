using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2020
{
    class Day20
    {
        public static void part1()
        {
            string[] input = new StreamReader("day20.txt").ReadToEnd().Trim().Split('\n');
            List<(int id, int[] sides, int[] neighbors)> tiles = new List<(int id, int[] sides, int[] neighbors)>();

            for (int i = 0; i < input.Length; i += 12)
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

                tiles.Add((int.Parse(input[i].Substring(5, 4)), sides, new int[4]));
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
                                currentTile.neighbors[side] = otherTile.id;
                                otherTile.neighbors[side2] = currentTile.id;
                                goto nextTile;
                            }
                        }
                    }
                    nextTile:;
                }
            }

            long product = 1;

            foreach (var (id, sides, neighbors) in tiles)
            {
                int count = 0;
                for (int i = 0; i < 4; ++i)
                {
                    if (neighbors[i] != 0)
                        count++;
                }
                if (count == 2) // corner tile
                    product *= id;
            }

            Console.Write(product);
            Console.Read();
        }

        public static int getReverse(int x) // 10 bit reverse
        {
            x = ((x & 0x01F) << 5) | ((x & 0x3E0) >> 5);
            x = ((x & 0x063) << 3) | ((x & 0x318) >> 3) | (x & 0x084);
            x = ((x & 0x129) << 1) | ((x & 0x252) >> 1) | (x & 0x084);
            return x;
        }

        private class Tile
        {
            public int[] sides;
            public char[,] pixels;


            public int matches(int hash)
            {
                int reverseHash = getReverse(hash);
                for(int i = 0; i < 4; ++i)
                {
                    if (sides[i] == hash || sides[i] == reverseHash)
                        return i;
                }
                return -1;
            }
            public void flipX()
            {
                for (int y = 0; y < 8; ++y)
                {
                    for (int x = 0; x < 4; ++x)
                    {
                        (pixels[x, y], pixels[7 - x, y]) = (pixels[7 - x, y], pixels[x, y]);
                    }
                }
                (sides[1], sides[3]) = (sides[3], sides[1]);
                sides[0] = getReverse(sides[0]);
                sides[2] = getReverse(sides[2]);
            }

            public void flipY()
            {
                for (int y = 0; y < 4; ++y)
                {
                    for (int x = 0; x < 8; ++x)
                    {
                        (pixels[x, y], pixels[x, 7 - y]) = (pixels[x, 7 - y], pixels[x, y]);
                    }
                }
                (sides[0], sides[2]) = (sides[2], sides[0]);
                sides[1] = getReverse(sides[1]);
                sides[3] = getReverse(sides[3]);
            }

            public void rotCW()
            {
                for (int y = 0; y < 7; ++y)
                {
                    for (int x = y + 1; x < 8; ++x)
                    {
                        (pixels[x, y], pixels[y, x]) = (pixels[y, x], pixels[x, y]);
                    }
                }

                (sides[0], sides[3]) = (sides[3], sides[0]);
                (sides[1], sides[2]) = (sides[2], sides[1]);

                flipX();
            }

            public void rotCCW()
            {
                for (int y = 0; y < 7; ++y)
                {
                    for (int x = y + 1; x < 8; ++x)
                    {
                        (pixels[x, y], pixels[y, x]) = (pixels[y, x], pixels[x, y]);
                    }
                }

                (sides[0], sides[3]) = (sides[3], sides[0]);
                (sides[1], sides[2]) = (sides[2], sides[1]);

                flipY();
            }
        }

        public static void part2()
        {
            string[] input = new StreamReader("day20.txt").ReadToEnd().Trim().Split('\n');
            Dictionary<int, Tile> tiles = new Dictionary<int, Tile>();

            for (int i = 1; i < input.Length; i += 12)
            {
                int[] sides = new int[4];
                for (int j = 0; j < 10; ++j)
                {
                    if (input[i][j] == '#') // top
                    {
                        sides[0] += 1 << (9 - j);
                    }
                    if (input[i + j][9] == '#') // right
                    {
                        sides[1] += 1 << (9 - j);
                    }
                    if (input[i + 9][j] == '#') // bottom
                    {
                        sides[2] += 1 << (9 - j);
                    }
                    if (input[i + j][0] == '#') // left
                    {
                        sides[3] += 1 << (9 - j);
                    }
                }
                char[,] pixels = new char[8, 8];

                for(int y = 0; y < 8; ++y)
                {
                    for(int x = 0; x < 8; ++x)
                    {
                        pixels[x, y] = input[i + y + 1][x + 1];
                    }
                }

                tiles.Add(int.Parse(input[i - 1].Substring(5, 4)), new Tile { sides = sides, pixels = pixels});
            }

            char[,] map = new char[96, 96];

            Queue<(int x, int y, Tile t)> open = new Queue<(int x, int y, Tile t)>();
            Tile topLeft = tiles[1439];// specific to this solution;
            open.Enqueue((0, 0, topLeft));
            tiles.Remove(1439);

            while(open.Count > 0)
            {
                var tile = open.Dequeue();

                List<int> toRemove = new List<int>();

                foreach(var kvp in tiles)
                {
                    var otherTile = kvp.Value;
                    //top
                    {
                        int r = otherTile.matches(tile.t.sides[0]);
                        if(r != -1)
                        {
                            if(r == 0)
                                otherTile.flipY();
                            else if(r == 1)
                                otherTile.rotCW();
                            else if(r == 3)
                                otherTile.rotCCW();

                            if (otherTile.sides[2] != tile.t.sides[0])
                                otherTile.flipX();

                            open.Enqueue((tile.x, tile.y - 1, otherTile));
                            toRemove.Add(kvp.Key);
                            //found tile
                            goto nextTile;
                        }
                    }
                    //right
                    {
                        int r = otherTile.matches(tile.t.sides[1]);
                        if (r != -1)
                        {
                            if (r == 0)
                                otherTile.rotCCW();
                            else if (r == 1)
                                otherTile.flipX();
                            else if (r == 2)
                                otherTile.rotCW();

                            if (otherTile.sides[3] != tile.t.sides[1])
                                otherTile.flipY();

                            open.Enqueue((tile.x + 1, tile.y, otherTile));
                            toRemove.Add(kvp.Key);
                            //found tile
                            goto nextTile;
                        }
                    }
                    //bottom
                    {
                        int r = otherTile.matches(tile.t.sides[2]);
                        if (r != -1)
                        {
                            if (r == 1)
                                otherTile.rotCCW();
                            else if (r == 2)
                                otherTile.flipY();
                            else if (r == 3)
                                otherTile.rotCW();

                            if (otherTile.sides[0] != tile.t.sides[2])
                                otherTile.flipX();

                            open.Enqueue((tile.x, tile.y + 1, otherTile));
                            toRemove.Add(kvp.Key);
                            //found tile
                            goto nextTile;
                        }
                    }
                    //left
                    {
                        int r = otherTile.matches(tile.t.sides[3]);
                        if (r != -1)
                        {
                            if (r == 0)
                                otherTile.rotCCW();
                            else if (r == 2)
                                otherTile.rotCW();
                            else if (r == 3)
                                otherTile.flipX();

                            if (otherTile.sides[1] != tile.t.sides[3])
                                otherTile.flipY();

                            open.Enqueue((tile.x - 1, tile.y, otherTile));
                            toRemove.Add(kvp.Key);
                            //found tile
                            goto nextTile;
                        }
                    }
                    nextTile:;
                }

                foreach (var key in toRemove)
                    tiles.Remove(key);

                for (int y = 0; y < 8; ++y)
                {
                    for (int x = 0; x < 8; ++x)
                    {
                        map[tile.x * 8 + x, tile.y * 8 + y] = tile.t.pixels[x, y];
                    }
                }
            }

            int totalCount = 0;

            for(int y = 0; y < 96; ++y)
            {
                for(int x = 0; x < 96; ++x)
                {
                    if (map[x, y] == '#')
                        totalCount++;
                }
            }

            (int mx, int my)[] monsterKernel = new[] { (1, 0), (19, 1), (14, 1), (13, 1), (8, 1), (7, 1), (2, 1), (1, 1), (0, 1), (18, 2), (15, 2), (12, 2), (9, 2), (6, 2), (3, 2) }; // already flipped
            int width = 20;
            int height = 3;

            int count = 0;

            while(count == 0)
            {
                for (int y = 0; y < 96 - height; ++y)
                {
                    for (int x = 0; x < 96 - width; ++x)
                    {
                        foreach (var (mx, my) in monsterKernel)
                        {
                            if (map[x + mx, y + my] != '#')
                            {
                                goto next;
                            }
                        }

                        count++;
                        next:;
                    }
                }

                if(count == 0)
                {
                    //rotate
                    int px = width / 2; // 10
                    int py = height / 2; // 1
                    for(int i = 0; i < monsterKernel.Length; ++i)
                    {
                        var (mx, my) = monsterKernel[i];
                        int nx = 2 * py - my;
                        int ny = mx;
                        monsterKernel[i] = (nx, ny);
                    }

                    (width, height) = (height, width);
                }

            }

            Console.Write(totalCount - count * 15);
            Console.Read();
        }
    }
}
