using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    internal class Day11 : IDay
    {
        private struct SeatingMap
        {
            public int XSize { get; init; }
            public int YSize { get; init; }
            public Dictionary<(int, int), char> Data { get; init;  }
        }
        public string FirstStar(string[] inputLines)
        {
            var seatingMap = ParseSeatingMap(inputLines);
            var seatingMapData = seatingMap.Data;
            bool change;
            do
            {
                change = false;
                var freshSeatingMapData = new Dictionary<(int, int), char>(seatingMapData);
                foreach (var kvp in freshSeatingMapData)
                {
                    var occupation = kvp.Value;
                    var seat = kvp.Key;

                    var adjancedOccupation = PositionsAround(seat, (seatingMap.XSize, seatingMap.YSize)).Select(position => seatingMapData[position]);
                    if (occupation == 'L' && adjancedOccupation.All(o => o == '.' || o == 'L'))
                    {
                        freshSeatingMapData[seat] = '#';
                        change = true;
                    }
                    else if (occupation == '#' && adjancedOccupation.Where(o => o == '#').Count() >= 4)
                    {
                        freshSeatingMapData[seat] = 'L';
                        change = true;
                    }
                }
                seatingMapData = freshSeatingMapData;
            } while (change);

            return seatingMapData.Where(kvp => kvp.Value == '#').Count().ToString();
        }

        public string SecondStar(string[] inputLines)
        {
            throw new NotImplementedException();
        }

        private SeatingMap ParseSeatingMap(string[] inputLines)
        {
            var data = new Dictionary<(int, int), char>();
            var ySize = inputLines.Count();
            var xSize = inputLines[0].Length;
            for (int y = 0; y < ySize; y++)
            {
                for (int x = 0; x < xSize; x++)
                {
                    data.Add((x, y), inputLines[y][x]);
                }
            }
            return new SeatingMap() { Data = data, XSize = xSize, YSize=ySize };
        }

        private HashSet<(int, int)> PositionsAround((int, int)  position, (int, int) maxBounds)
        {
            var positions = new HashSet<(int, int)>();
            for (int y = -1; y < 2; y++)
            {
                for (int x = -1; x < 2; x++)
                {
                    positions.Add((
                        Math.Min(Math.Max(position.Item1 + x, 0), maxBounds.Item1 - 1),
                        Math.Min(Math.Max(position.Item2 + y, 0), maxBounds.Item2 - 1)));
                }
            }
            positions.Remove(position);
            return positions;
        }
    }
}
