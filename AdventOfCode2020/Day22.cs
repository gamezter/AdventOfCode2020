using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2020
{
    class Day22
    {
        public static void part1()
        {
            string[] input = new StreamReader("day22.txt").ReadToEnd().Trim().Split('\n');

            Queue<int> player1 = new Queue<int>();
            Queue<int> player2 = new Queue<int>();
            for(int i = 1; i < 26; ++i)
            {
                player1.Enqueue(int.Parse(input[i]));
            }
            for (int i = 28; i < 53; ++i)
            {
                player2.Enqueue(int.Parse(input[i]));
            }

            while(player1.Count > 0 && player2.Count > 0)
            {
                int card1 = player1.Dequeue();
                int card2 = player2.Dequeue();

                if(card1 > card2)
                {
                    player1.Enqueue(card1);
                    player1.Enqueue(card2);
                }
                else
                {
                    player2.Enqueue(card2);
                    player2.Enqueue(card1);
                }
            }

            Queue<int> winner = player1.Count > player2.Count ? player1 : player2;

            int total = 0;

            while(winner.Count > 0)
            {
                int pos = winner.Count;
                total += winner.Dequeue() * pos;
            }

            Console.Write(total);
            Console.Read();
        }

        public static void part2()
        {
            string[] input = new StreamReader("day22.txt").ReadToEnd().Trim().Split('\n');

            Queue<int> player1 = new Queue<int>();
            Queue<int> player2 = new Queue<int>();
            for (int i = 1; i < 26; ++i)
            {
                player1.Enqueue(int.Parse(input[i]));
            }
            for (int i = 28; i < 53; ++i)
            {
                player2.Enqueue(int.Parse(input[i]));
            }

            Game(player1, player2);

            Queue<int> winner = player1.Count > player2.Count ? player1 : player2;

            int total = 0;

            while (winner.Count > 0)
            {
                int pos = winner.Count;
                total += winner.Dequeue() * pos;
            }

            Console.Write(total);
            Console.Read();
        }

        private static int Game(Queue<int> player1, Queue<int> player2)
        {
            HashSet<(int, int)> visited = new HashSet<(int, int)>();

            while (player1.Count > 0 && player2.Count > 0)
            {
                int[] h1 = player1.ToArray();
                int[] h2 = player2.ToArray();
                int hash1 = 17;
                int hash2 = 17;
                for (int i = 0, l = h1.Length; i < l; ++i)
                {
                    hash1 = hash1 * 19 + h1[i];
                }
                for (int i = 0, l = h2.Length; i < l; ++i)
                {
                    hash2 = hash2 * 19 + h2[i];
                }

                if (!visited.Add((hash1, hash2)))
                    return 1;

                int card1 = player1.Dequeue();
                int card2 = player2.Dequeue();

                int winner;

                if (card1 <= player1.Count && card2 <= player2.Count)
                {
                    Queue<int> p1 = new Queue<int>();
                    Queue<int> p2 = new Queue<int>();

                    for(int i = 0; i < card1; ++i)
                    {
                        p1.Enqueue(h1[i + 1]);
                    }
                    for (int i = 0; i < card2; ++i)
                    {
                        p2.Enqueue(h2[i + 1]);
                    }

                    winner = Game(p1, p2);
                }
                else
                {
                    winner = card1 > card2 ? 1 : 2;
                }

                if (winner == 1)
                {
                    player1.Enqueue(card1);
                    player1.Enqueue(card2);
                }
                else
                {
                    player2.Enqueue(card2);
                    player2.Enqueue(card1);
                }
            }

            return player1.Count > player2.Count ? 1 : 2;
        }
    }
}
