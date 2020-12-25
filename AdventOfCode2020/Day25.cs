using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    class Day25
    {
        public static void part1()
        {
            int doorPublicKey = 9232416;
            int cardPublicKey = 14144084;

            int doorLoopSize = 0;
            int cardLoopSize = 0;

            long value = 1;
            while (value != doorPublicKey)
            {
                value *= 7;
                value %= 20201227;
                doorLoopSize++;
            }

            long value2 = 1;
            while (value2 != cardPublicKey)
            {
                value2 *= 7;
                value2 %= 20201227;
                cardLoopSize++;
            }

            value = 1;
            value2 = 1;

            for(int i = 0; i < doorLoopSize; ++i)
            {
                value *= cardPublicKey;
                value %= 20201227;
            }

            for (int i = 0; i < cardLoopSize; ++i)
            {
                value2 *= doorPublicKey;
                value2 %= 20201227;
            }

            if (value == value2)
                Console.Write(value);
            Console.Read();
        }
    }
}
