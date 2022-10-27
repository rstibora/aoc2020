using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    internal class Day12 : IDay
    {
        public string FirstStar(string[] inputLines)
        {
            int shipOrientationDegrees = 0;
            int shipX = 0;
            int shipY = 0;

            foreach (var line in inputLines)
            {
                var instruction = line[0];
                var amount = int.Parse(line[1..]);

                switch (instruction) {
                    case 'N': { shipY += amount; break; }
                    case 'S': { shipY -= amount; break; }
                    case 'E': { shipX += amount; break; }
                    case 'W': { shipX -= amount; break; }
                    case 'L': { shipOrientationDegrees -= amount; if (shipOrientationDegrees < 0) { shipOrientationDegrees += 360; } break; }
                    case 'R': { shipOrientationDegrees += amount; if (shipOrientationDegrees >= 360) { shipOrientationDegrees -= 360; } break; }
                    case 'F':
                        {
                            switch (shipOrientationDegrees)
                            {
                                case 0: { shipX += amount; break; }
                                case 90: { shipY -= amount; break; }
                                case 180: { shipX -= amount; break; }
                                case 270: { shipY += amount; break; }
                            }
                            break;
                        }
                    default: throw new Exception($"Unsupported command '{instruction}'.");
                }
            }
            return (Math.Abs(shipX) + Math.Abs(shipY)).ToString();
        }

        public string SecondStar(string[] inputLines)
        {
            throw new NotImplementedException();
        }
    }
}
