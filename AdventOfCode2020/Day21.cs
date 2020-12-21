using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    class Day21
    {
        public static void part1()
        {
            string[] input = new StreamReader("day21.txt").ReadToEnd().Trim().Split('\n');
            Regex r = new Regex(@"((?<ingredients>\w+) )+\(contains ((?<alergens>\w+)(, )?)+\)");
            Dictionary<string, List<string>> possibleAllergens = new Dictionary<string, List<string>>();

            List<string> allIngredients = new List<string>();
            foreach(var line in input)
            {
                var m = r.Match(line);
                Group ingredientGroup = m.Groups["ingredients"];
                Group allergenGroup = m.Groups["alergens"];

                for (int i = 0; i < ingredientGroup.Captures.Count; ++i)
                {
                    string ingredient = ingredientGroup.Captures[i].Value;
                    allIngredients.Add(ingredient);
                }

                for (int i = 0; i < allergenGroup.Captures.Count; ++i)
                {
                    if (possibleAllergens.TryGetValue(allergenGroup.Captures[i].Value, out var value))
                    {
                        for(int j = 0; j < value.Count; ++j)
                        {
                            for(int k = 0; k < ingredientGroup.Captures.Count; ++k)
                            {
                                if (value[j] == ingredientGroup.Captures[k].Value)
                                    goto nextIngredient;
                            }
                            value.RemoveAt(j);
                            --j;
                            nextIngredient:;
                        }
                    }
                    else
                    {
                        List<string> possibilities = new List<string>(ingredientGroup.Captures.Count);
                        for (int j = 0; j < ingredientGroup.Captures.Count; ++j)
                        {
                            possibilities.Add(ingredientGroup.Captures[j].Value);
                        }
                        possibleAllergens.Add(allergenGroup.Captures[i].Value, possibilities);
                    }
                }
            }

            List<string> allergens = new List<string>();

            while(possibleAllergens.Count > 0)
            {
                string removeKey = null;
                string removeIngredient = null;
                foreach (var kvp in possibleAllergens)
                {
                    if(kvp.Value.Count == 1)
                    {
                        removeKey = kvp.Key;
                        removeIngredient = kvp.Value[0];
                    }
                }

                if(removeKey != null && removeIngredient != null)
                {
                    possibleAllergens.Remove(removeKey);

                    foreach (var kvp in possibleAllergens)
                    {
                        kvp.Value.Remove(removeIngredient);
                    }
                }

                allergens.Add(removeIngredient);
            }

            foreach(var allergen in allergens)
            {
                allIngredients.RemoveAll(s => s == allergen);
            }

            Console.Write(allIngredients.Count);
            Console.Read();
        }

        public static void part2()
        {
            string[] input = new StreamReader("day21.txt").ReadToEnd().Trim().Split('\n');
            Regex r = new Regex(@"((?<ingredients>\w+) )+\(contains ((?<alergens>\w+)(, )?)+\)");
            Dictionary<string, List<string>> possibleAllergens = new Dictionary<string, List<string>>();
            foreach (var line in input)
            {
                var m = r.Match(line);
                Group ingredientGroup = m.Groups["ingredients"];
                Group allergenGroup = m.Groups["alergens"];

                for (int i = 0; i < allergenGroup.Captures.Count; ++i)
                {
                    if (possibleAllergens.TryGetValue(allergenGroup.Captures[i].Value, out var value))
                    {
                        for (int j = 0; j < value.Count; ++j)
                        {
                            for (int k = 0; k < ingredientGroup.Captures.Count; ++k)
                            {
                                if (value[j] == ingredientGroup.Captures[k].Value)
                                    goto nextIngredient;
                            }
                            value.RemoveAt(j);
                            --j;
                            nextIngredient:;
                        }
                    }
                    else
                    {
                        List<string> possibilities = new List<string>(ingredientGroup.Captures.Count);
                        for (int j = 0; j < ingredientGroup.Captures.Count; ++j)
                        {
                            possibilities.Add(ingredientGroup.Captures[j].Value);
                        }
                        possibleAllergens.Add(allergenGroup.Captures[i].Value, possibilities);
                    }
                }
            }

            List<(string, string)> allergens = new List<(string, string)>();

            while (possibleAllergens.Count > 0)
            {
                string removeKey = null;
                string removeIngredient = null;
                foreach (var kvp in possibleAllergens)
                {
                    if (kvp.Value.Count == 1)
                    {
                        removeKey = kvp.Key;
                        removeIngredient = kvp.Value[0];
                    }
                }

                if (removeKey != null && removeIngredient != null)
                {
                    possibleAllergens.Remove(removeKey);

                    foreach (var kvp in possibleAllergens)
                    {
                        kvp.Value.Remove(removeIngredient);
                    }
                }

                allergens.Add((removeKey, removeIngredient));
            }

            allergens.Sort();

            foreach(var allergen in allergens)
            {
                Console.Write(allergen.Item2 + ",");
            }
            Console.Read();
        }
    }
}
