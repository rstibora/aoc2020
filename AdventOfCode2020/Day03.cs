﻿namespace AdventOfCode2020
{
    internal class Day03 : IDay
    {
        enum Foliage : byte
        {
            None,
            Tree
        }

        class WrapAround2DIndex
        {
            private readonly int width;
            private readonly int height;
            public int X { get; private set; }
            public int Y { get; private set; }

            public WrapAround2DIndex(int width, int height)
            {
                this.width = width;
                this.height = height;
            }

            public void Shift(int x, int y)
            {
                X += x;
                if (X >= width)
                    X -= width;
                if (X < 0)
                    X += width;

                Y += y;
                if (Y >= height)
                    Y -= height;
                if (Y < 0)
                    Y += height;
            }
        }

        static Foliage[,] ParseGroundPlan(string[] inputLines)
        {
            Foliage[,] groundPlan = new Foliage[inputLines[0].Length, inputLines.Length];
            for (int x = 0; x < groundPlan.GetLength(0); x++)
                for (int y = 0; y < groundPlan.GetLength(1); y++)
                    groundPlan[x, y] = inputLines[y][x] switch
                    {
                        '.' => Foliage.None,
                        '#' => Foliage.Tree,
                        object unsupported => throw new ArgumentOutOfRangeException(nameof(inputLines), $"Unexpected symbol {unsupported}.")
                    };
            return groundPlan;
        }

        static int[] TreesForSlopes((int x, int y)[] slopes, Foliage[,] groundPlan)
        {
            var treeCounts = new int[slopes.Length];
            for (int i = 0; i < slopes.Length; i++)
            {
                WrapAround2DIndex index = new(groundPlan.GetLength(0), groundPlan.GetLength(1));

                for (int y = 0; y < groundPlan.GetLength(1); y += slopes[i].y)
                {
                    if (groundPlan[index.X, index.Y] == Foliage.Tree)
                        treeCounts[i]++;
                    index.Shift(slopes[i].x, slopes[i].y);
                }
            }
            return treeCounts;
        }

        public string FirstStar(string[] inputLines)
        {
            var groundPlan = ParseGroundPlan(inputLines);
            return TreesForSlopes(new[] { (3, 1) }, groundPlan)[0].ToString();
        }

        public string SecondStar(string[] inputLines)
        {
            var groundPlan = ParseGroundPlan(inputLines);
            return TreesForSlopes(new[] { (1, 1), (3, 1), (5, 1), (7, 1), (1, 2) }, groundPlan).Select(a => (Int64)a).Aggregate((a, b) => a * b).ToString();
        }
    }
}
