using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AdventOfCode2020
{
    internal class Day12 : IDay
    {
        class NavigationComputer
        {
            private Dictionary<char, Action<int>> instructions;
            public NavigationComputer(Dictionary<char, Action<int>> instructions)
            {
                this.instructions = instructions;
            }

            public void Run(string[] rawInstructions)
            {
                foreach (var rawInstruction in rawInstructions)
                {
                    var instruction = rawInstruction[0];
                    var amount = int.Parse(rawInstruction[1..]);

                    instructions[instruction].Invoke(amount);
                }
            }
        }

        public string FirstStar(string[] inputLines)
        {
            int shipOrientationDegrees = 0;
            int shipX = 0;
            int shipY = 0;

            var computer = new NavigationComputer(new Dictionary<char, Action<int>>() {
                { 'N', (int amount) => shipY += amount },
                { 'S', (int amount) => shipY -= amount },
                { 'E', (int amount) => shipX += amount },
                { 'W', (int amount) => shipX -= amount },
                { 'L', (int amount) => { shipOrientationDegrees -= amount; if (shipOrientationDegrees < 0) { shipOrientationDegrees += 360; } } },
                { 'R', (int amount) => { shipOrientationDegrees += amount; if (shipOrientationDegrees >= 360) { shipOrientationDegrees -= 360; } } },
                { 'F', (int amount) =>
                    {
                        switch (shipOrientationDegrees)
                        {
                            case 0: { shipX += amount; break; }
                            case 90: { shipY -= amount; break; }
                            case 180: { shipX -= amount; break; }
                            case 270: { shipY += amount; break; }
                        }
                    }
                }});
            computer.Run(inputLines);
            return (Math.Abs(shipX) + Math.Abs(shipY)).ToString();
        }

        public string SecondStar(string[] inputLines)
        {
            var rotate = (int x, int y, int deg) =>
            {
                if (deg == 0) { return (x, y); }
                else if (deg == 90) { return (y, -x); }
                else if (deg == 180) { return (-x, -y); }
                else if (deg == 270) { return (-y, x); }
                throw new Exception($"Unsupported rotation {deg} degrees.");
            };

            int shipX = 0;
            int shipY = 0;
            int waypointX = 10;
            int waypointY = 1;

            var computer = new NavigationComputer(new Dictionary<char, Action<int>>() {
                { 'N', (int amount) => waypointY += amount },
                { 'S', (int amount) => waypointY -= amount },
                { 'E', (int amount) => waypointX += amount },
                { 'W', (int amount) => waypointX -= amount },
                { 'L', (int amount) => {  (waypointX, waypointY) = rotate(waypointX, waypointY, 360 - amount); } },
                { 'R', (int amount) => {  (waypointX, waypointY) = rotate(waypointX, waypointY, amount); } },
                { 'F', (int amount) => { shipX += waypointX * amount; shipY += waypointY * amount; } }
            });
            computer.Run(inputLines);
            return (Math.Abs(shipX) + Math.Abs(shipY)).ToString();
        }
    }
}
