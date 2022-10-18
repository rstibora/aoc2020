using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    internal class Day09 : IDay
    {
        private const int PREAMBLE_SIZE = 25;
        public string FirstStar(string[] inputLines)
        {
            var numbers = ParseNumbers(inputLines);
            return FindFirstInvalidNumber(numbers).ToString();
        }

        public string SecondStar(string[] inputLines)
        {
            var numbers = ParseNumbers(inputLines);
            var targetNumber = FindFirstInvalidNumber(numbers);

            var relevantNumbers = numbers.Reverse().SkipWhile(number => number > targetNumber).Reverse().ToList();
            for (int contiguousSize = 2; contiguousSize <= relevantNumbers.Count(); contiguousSize++)
            {
                var sum = relevantNumbers.Take(contiguousSize).Sum();
                for (int index = 0; index < relevantNumbers.Count() - contiguousSize; index++)
                {
                    if (sum == targetNumber)
                    {
                        var contiguousSet = relevantNumbers.Skip(index).Take(contiguousSize);
                        return (contiguousSet.Max() + contiguousSet.Min()).ToString();
                    }

                    sum += relevantNumbers[index + contiguousSize];
                    sum -= relevantNumbers[index];
                }
            }
            throw new Exception("No such number.");
        }

        private long FindFirstInvalidNumber(IEnumerable<long> numbers)
        {
            var preamble = new LinkedList<long>(numbers.Take(PREAMBLE_SIZE));
            foreach (var number in numbers.Skip(PREAMBLE_SIZE))
            {
                if (!IsSumOfTwoFrom(preamble, number))
                {
                    return number;
                }
                preamble.RemoveFirst();
                preamble.AddLast(number);
            }
            throw new Exception("Could not find the number.");
        }

        private IEnumerable<long> ParseNumbers(string[] inputLines)
        {
            return inputLines.Select(long.Parse);
        }

        private bool IsSumOfTwoFrom(IEnumerable<long> possibleNumbers, long number)
        {
            foreach (var (a, i) in possibleNumbers.Select((value, index) => (value, index)))
            {
                foreach (var (b, y) in possibleNumbers.Select((value, index) => (value, index)))
                {
                    if (a + b == number && i != y)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
