using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    class Day23
    {
        private class Node
        {
            public int value;
            public Node next;
        }

        public static void part1()
        {
            Node one = new Node { value = 1 };
            Node two = new Node { value = 2 };
            Node three = new Node { value = 3 };
            Node four = new Node { value = 4 };
            Node five = new Node { value = 5 };
            Node six = new Node { value = 6 };
            Node seven = new Node { value = 7 };
            Node eight = new Node { value = 8 };
            Node nine = new Node { value = 9 };

            one.next = nine;
            nine.next = eight;
            eight.next = seven;
            seven.next = five;
            five.next = three;
            three.next = four;
            four.next = six;
            six.next = two;
            two.next = one;

            Node current = one;

            for(int i = 0; i < 100; ++i)
            {
                int value = current.value;
                Node next1 = current.next;
                Node next2 = next1.next;
                Node next3 = next2.next;

                do
                {
                    value--;
                    if (value == 0)
                        value = 9;
                }
                while (next1.value == value || next2.value == value || next3.value == value);

                Node search = next3.next;
                while (search.value != value)
                    search = search.next;

                current.next = next3.next;
                next3.next = search.next;
                search.next = next1;

                current = current.next;
            }

            current = one.next;
            for(int i = 0; i < 8; ++i)
            {
                Console.Write(current.value);
                current = current.next;
            }
            Console.Read();
        }
    }
}
