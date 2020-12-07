using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    class Day7
    {
        public static void part1()
        {
            string[] rules = new StreamReader("day7.txt").ReadToEnd().Trim().Split('\n');
            Regex r = new Regex(@"(?<container>\w+ \w+) bags contain ((?<count0>\d) (?<containee0>\w+ \w+) bags?(\.|,))?( (?<count1>\d) (?<containee1>\w+ \w+) bags?(\.|,))?( (?<count2>\d) (?<containee2>\w+ \w+) bags?(\.|,))?( (?<count3>\d) (?<containee3>\w+ \w+) bags?(\.|,))?");

            Dictionary<string, List<string>> isIn = new Dictionary<string, List<string>>();

            foreach(var rule in rules)
            {
                Match m = r.Match(rule);
                string container = m.Groups["container"].Value;
                if (int.TryParse(m.Groups["count0"].Value, out int count0))
                {
                    if (isIn.TryGetValue(m.Groups["containee0"].Value, out List<string> parents))
                        parents.Add(container);
                    else
                        isIn.Add(m.Groups["containee0"].Value, new List<string>{ container });
                }
                    
                if (int.TryParse(m.Groups["count1"].Value, out int count1))
                {
                    if (isIn.TryGetValue(m.Groups["containee1"].Value, out List<string> parents))
                        parents.Add(container);
                    else
                        isIn.Add(m.Groups["containee1"].Value, new List<string> { container });
                }
                    
                if (int.TryParse(m.Groups["count2"].Value, out int count2))
                {
                    if (isIn.TryGetValue(m.Groups["containee2"].Value, out List<string> parents))
                        parents.Add(container);
                    else
                        isIn.Add(m.Groups["containee2"].Value, new List<string> { container });
                }
                    
                if (int.TryParse(m.Groups["count3"].Value, out int count3))
                {
                    if (isIn.TryGetValue(m.Groups["containee3"].Value, out List<string> parents))
                        parents.Add(container);
                    else
                        isIn.Add(m.Groups["containee3"].Value, new List<string> { container });
                }
            }

            List<string> allParentsList = new List<string>();

            allParentsList.AddRange(isIn["shiny gold"]);

            for(int i = 0; i < allParentsList.Count; ++i)
            {
                string bag = allParentsList[i];
                if(isIn.TryGetValue(bag, out List<string> parents))
                {
                    foreach (var b in parents)
                    {
                        if (!allParentsList.Contains(b))
                            allParentsList.Add(b);
                    }
                }
            }

            Console.Write(allParentsList.Count);
            Console.Read();
        }

        public static void part2()
        {
            string[] rules = new StreamReader("day7.txt").ReadToEnd().Trim().Split('\n');
            Regex r = new Regex(@"(?<container>\w+ \w+) bags contain ((?<count0>\d) (?<containee0>\w+ \w+) bags?(\.|,))?( (?<count1>\d) (?<containee1>\w+ \w+) bags?(\.|,))?( (?<count2>\d) (?<containee2>\w+ \w+) bags?(\.|,))?( (?<count3>\d) (?<containee3>\w+ \w+) bags?(\.|,))?");

            Dictionary<string, List<string>> contains = new Dictionary<string, List<string>>();

            foreach (var rule in rules)
            {
                Match m = r.Match(rule);
                string container = m.Groups["container"].Value;
                List<string> containees = new List<string>();
                if (int.TryParse(m.Groups["count0"].Value, out int count0))
                {
                    for(int i = 0; i < count0; ++i)
                    {
                        containees.Add(m.Groups["containee0"].Value);
                    }
                }

                if (int.TryParse(m.Groups["count1"].Value, out int count1))
                {
                    for (int i = 0; i < count1; ++i)
                    {
                        containees.Add(m.Groups["containee1"].Value);
                    }
                }

                if (int.TryParse(m.Groups["count2"].Value, out int count2))
                {
                    for (int i = 0; i < count2; ++i)
                    {
                        containees.Add(m.Groups["containee2"].Value);
                    }
                }

                if (int.TryParse(m.Groups["count3"].Value, out int count3))
                {
                    for (int i = 0; i < count3; ++i)
                    {
                        containees.Add(m.Groups["containee3"].Value);
                    }
                }

                contains.Add(container, containees);
            }

            Stack<string> bags = new Stack<string>();
            bags.Push("shiny gold");
            int count = 0;
            while(bags.Count > 0)
            {
                List<string> inside = contains[bags.Pop()];
                foreach(var bag in inside)
                {
                    bags.Push(bag);
                    count++;
                }
            }
            Console.Write(count);
            Console.Read();
        }
    }
}
