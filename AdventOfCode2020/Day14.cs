using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    internal class Day14 : IDay
    {
        private static int NUMBER_OF_BITS = sizeof(long) * 8;

        public string FirstStar(string[] inputLines)
        {
            return RunFirstStar(inputLines).GetAwaiter().GetResult();
        }

        private async Task<string> RunFirstStar(string[] inputLines)
        {
            var memory = new Dictionary<int, long>();
            var andMask = new BitArray(NUMBER_OF_BITS, true);
            var orMask = new BitArray(NUMBER_OF_BITS, false);
            foreach (var line in inputLines)
            {
                if (line.Contains("mask"))
                {
                    andMask = await ParseAndMaskAsync(line);
                    orMask = await ParseOrMaskAsync(line);
                }
                else
                {
                    var address = await ParseAddressAsync(line);
                    var value = await ParseValueAsync(line);
                    memory[address] = await MaskValueAsync(value, andMask, orMask);
                }
            }
            return memory.Values.Sum().ToString();
        }

        private async Task<BitArray> ParseAndMaskAsync(string line)
        {
            // The mask is reversed since the ToBytes produces BitArray with least significant bit first.
            return new BitArray(line.Split("=")[1].Trim().PadLeft(NUMBER_OF_BITS, 'X').Reverse().Select(bit => bit == '0' ? false : true).ToArray());
        }

        private async Task<BitArray> ParseOrMaskAsync(string line)
        {
            // The mask is reversed since the ToBytes produces BitArray with least significant bit first.
            return new BitArray(line.Split("=")[1].Trim().PadLeft(NUMBER_OF_BITS, 'X').Reverse().Select(bit => bit == '1' ? true : false).ToArray());
        }

        private async Task<int> ParseAddressAsync(string line)
        {
            var memoryRegex = new Regex(@"mem\[([0-9]+)\]*");
            return int.Parse(memoryRegex.Match(line).Groups[1].Value);
        }

        private async Task<long> ParseValueAsync(string line)
        {
            return long.Parse(line.Split("=")[1].Trim());
        }

        private async Task<long> MaskValueAsync(long value, BitArray andMask, BitArray orMask)
        {
            var valueBitArray = new BitArray(BitConverter.GetBytes(value));
            valueBitArray.And(andMask).Or(orMask);
            var byteArray = new byte[8];
            valueBitArray.CopyTo(byteArray, 0);
            return BitConverter.ToInt64(byteArray, 0);
        }

        public string SecondStar(string[] inputLines)
        {
            throw new NotImplementedException();
        }
    }
}
