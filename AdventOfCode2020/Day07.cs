using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    internal class Day07 : IDay
    {
        public string FirstStar(string[] inputLines)
        {
            var rules = ParseRules(inputLines);
            var canContainShinyGolden = new HashSet<string>();
            var discoveredNew = true;

            do
            {
                discoveredNew = false;
                foreach (var rule in rules)
                {
                    foreach (var ruleTarget in rule.Value)
                    {
                        if (canContainShinyGolden.Contains(ruleTarget.Item1) || ruleTarget.Item1 == "shiny gold")
                        {
                            if (!canContainShinyGolden.Contains(rule.Key))
                            {
                                canContainShinyGolden.Add(rule.Key);
                                discoveredNew = true;
                            }
                        }
                    }
                }
            } while (discoveredNew);

            return canContainShinyGolden.Count.ToString();
        }

        public string SecondStar(string[] inputLines)
        {
            var rules = ParseRules(inputLines);
            return CountInnerBags("shiny gold", rules).ToString();
        }

        private int CountInnerBags(string bagName, Dictionary<string, List<(string, int)>> rules)
        {
            if (!rules.ContainsKey(bagName) || rules[bagName].Count == 0)
            {
                return 0;
            }

            var sum = 0;
            foreach (var rule in rules[bagName])
            {
                var innerBags = CountInnerBags(rule.Item1, rules);
                if (innerBags == 0)
                {
                    sum += rule.Item2;
                }
                else
                {
                    sum += rule.Item2 + rule.Item2 * innerBags;
                }
            }
            return sum;
        }

        private static Dictionary<string, List<(string, int)>> ParseRules(string[] inputLines)
        {
            var rules = new Dictionary<string, List<(string, int)>>(inputLines.Length);
            foreach (var line in inputLines)
            { 
                var sourceName = line.Split("bags").First().Trim();
                rules[sourceName] = new List<(string, int)>();

                foreach (var targetLine in line.Split("contain").Last().Trim().Split(","))
                {
                    if (targetLine == "no other bags.")
                    {
                        continue;
                    }

                    var splits = targetLine.Trim().Split(" ");
                    var targetName = $"{splits[1]} {splits[2]}";
                    int targetCount = int.Parse(splits[0]);
                    rules[sourceName].Add((targetName, targetCount));
                }
            }

            return rules;
        }
    }
}
