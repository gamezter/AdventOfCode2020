using System;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    class Day4
    {
        public static void part1()
        {
            string[] passports = new StreamReader("day4.txt").ReadToEnd().Trim().Split(new[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
            int validCount = 0;
            foreach(var passport in passports)
            {
                string[] kvps = passport.Split(' ', '\n');
                bool hasBYR = false;
                bool hasIYR = false;
                bool hasEYR = false;
                bool hasHGT = false;
                bool hasHCL = false;
                bool hasECL = false;
                bool hasPID = false;
                bool hasCID = false;
                foreach(var kvp in kvps)
                {
                    string key = kvp.Substring(0, 3);
                    switch (key)
                    {
                        case "byr":
                            hasBYR = true; break;
                        case "iyr":
                            hasIYR = true; break;
                        case "eyr":
                            hasEYR = true; break;
                        case "hgt":
                            hasHGT = true; break;
                        case "hcl":
                            hasHCL = true; break;
                        case "ecl":
                            hasECL = true; break;
                        case "pid":
                            hasPID = true; break;
                        case "cid":
                            hasCID = true; break;
                    }
                }
                if (hasBYR && hasIYR && hasEYR && hasHGT && hasHCL && hasECL && hasPID)
                    validCount++;
            }
            Console.WriteLine(validCount);
            Console.Read();
        }

        public static void part2()
        {
            string[] passports = new StreamReader("day4.txt").ReadToEnd().Trim().Split(new[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries);

            int validCount = 0;

            Regex byr = new Regex(@"byr:(19[2-9][0-9]|200[0-2])");
            Regex iyr = new Regex(@"iyr:(201[0-9]|2020)");
            Regex eyr = new Regex(@"eyr:(202[0-9]|2030)");
            Regex hgt = new Regex(@"hgt:((1[5-8][0-9]|19[0-3])cm|(59|6[0-9]|7[0-6])in)");
            Regex hcl = new Regex(@"hcl:#[0-9a-f]{6}");
            Regex ecl = new Regex(@"ecl:(amb|blu|brn|gry|grn|hzl|oth)");
            Regex pid = new Regex(@"pid:[0-9]{9}([^0-9]|$)");

            foreach (var passport in passports)
            {
                bool validBYR = byr.IsMatch(passport);
                bool validIYR = iyr.IsMatch(passport);
                bool validEYR = eyr.IsMatch(passport);
                bool validHGT = hgt.IsMatch(passport);
                bool validHCL = hcl.IsMatch(passport);
                bool validECL = ecl.IsMatch(passport);
                bool validPID = pid.IsMatch(passport);

                if (validBYR && validIYR && validEYR && validHGT && validHCL && validECL && validPID)
                    validCount++;
            }
            Console.WriteLine(validCount);
            Console.Read();
        }
    }
}
