using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    internal class Day06 : IDay
    {
        public string FirstStar(string[] inputLines)
        {
            var sets = GetGroupSets(inputLines);
            return sets.Select(group => group.Aggregate((a, b) => new HashSet<char>(a.Union(b)))).Select(group => group.Count).Sum().ToString();
        }

        public string SecondStar(string[] inputLines)
        {
            var sets = GetGroupSets(inputLines);
            return sets.Select(group => group.Aggregate((a, b) => new HashSet<char>(a.Intersect(b)))).Select(group => group.Count).Sum().ToString();
        }

        private IEnumerable<List<HashSet<char>>> GetGroupSets(string[] inputLines)
        {
            var groups = new List<List<string>> { new List<string>() };
            foreach (var line in inputLines)
            {
                if (line == string.Empty)
                {
                    groups.Add(new List<string>());
                }
                else
                {
                    groups.Last().Add(line);
                }
            }

            return groups.Select(groupItems => groupItems.ConvertAll(item => new HashSet<char>(item)));
        }
    }
}
