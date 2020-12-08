using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    class Day8
    {
        public static void part1()
        {
            string[] program = new StreamReader("day8.txt").ReadToEnd().Trim().Split('\n');
            int pc = 0;
            int acc = 0;
            bool running = true;

            HashSet<int> visited = new HashSet<int>();

            while (running)
            {
                if(!visited.Add(pc))
                {
                    Console.Write(acc);
                    Console.Read();
                }
                string op = program[pc].Substring(0, 3);

                switch (op)
                {
                    case "nop":
                        {
                            pc++;
                        }
                        break;
                    case "acc":
                        {
                            acc += int.Parse(program[pc].Substring(3));
                            pc++;
                        }
                        break;
                    case "jmp":
                        {
                            pc += int.Parse(program[pc].Substring(3));
                        }
                        break;
                }
            }
        }

        public static void part2()
        {
            string[] program = new StreamReader("day8.txt").ReadToEnd().Trim().Split('\n');

            for(int i = 0; i < program.Length; ++i)
            {
                if(program[i][0] == 'j')
                {
                    program[i] = "nop" + program[i].Substring(3);
                }
                else if(program[i][0] == 'n')
                {
                    program[i] = "jmp" + program[i].Substring(3);
                }
                else
                {
                    continue;
                }


                int pc = 0;
                int acc = 0;
                bool running = true;

                HashSet<int> visited = new HashSet<int>();

                while (running)
                {
                    if(pc == program.Length)
                    {
                        Console.Write(acc);
                        Console.Read();
                    }

                    if (!visited.Add(pc))
                    {
                        break;
                    }
                    string op = program[pc].Substring(0, 3);

                    switch (op)
                    {
                        case "nop":
                            {
                                pc++;
                            }
                            break;
                        case "acc":
                            {
                                acc += int.Parse(program[pc].Substring(3));
                                pc++;
                            }
                            break;
                        case "jmp":
                            {
                                pc += int.Parse(program[pc].Substring(3));
                            }
                            break;
                    }
                }

                if (program[i][0] == 'j')
                {
                    program[i] = "nop" + program[i].Substring(3);
                }
                else if (program[i][0] == 'n')
                {
                    program[i] = "jmp" + program[i].Substring(3);
                }
            }
        }
    }
}
