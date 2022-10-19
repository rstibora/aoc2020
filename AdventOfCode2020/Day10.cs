using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    internal class Day10 : IDay
    {
        public string FirstStar(string[] inputLines)
        {
            var jolts = inputLines.Select(int.Parse).Append(0).OrderBy(x => x).ToList();
            jolts.Add(jolts.Max() + 3);
            var diffs1 = jolts.Zip(jolts.Skip(1)).Select(item => item.Second - item.First == 1 ? 1 : 0).Sum();
            var diffs3 = jolts.Zip(jolts.Skip(1)).Select(item => item.Second - item.First == 3 ? 1 : 0).Sum();
            return (diffs1 * diffs3).ToString();
        }

        public string SecondStar(string[] inputLines)
        {
            var jolts = inputLines.Select(int.Parse).Append(0).OrderBy(x => x).ToList();
            jolts.Add(jolts.Max() + 3);
            return CountArrangementsFor(0, jolts, new Dictionary<int, long>()).ToString();

        }

        private long CountArrangementsFor(int targetJolts, List<int> jolts, Dictionary<int, long> cache)
        {
            if (cache.ContainsKey(targetJolts))
            {
                return cache[targetJolts];
            }

            if (targetJolts == jolts.Max())
            {
                return 1;
            }
            var possibleJolts = jolts.FindAll(candidateJolts => candidateJolts - targetJolts <= 3 && candidateJolts - targetJolts > 0);
            var value = possibleJolts.Select(j => CountArrangementsFor(j, jolts, cache)).Sum();
            cache[targetJolts] = value;
            return value;
        }
    }
}
