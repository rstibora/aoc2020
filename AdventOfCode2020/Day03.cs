namespace AdventOfCode2020
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
            private int width;
            private int height;
            public int X { get; private set; }
            public int Y { get; private set; }

            public WrapAround2DIndex(int width, int height)
            {
                this.width = width;
                this.height = height;
            }

            public void shift(int x, int y)
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

        Foliage[,] parseGroundPlan(string[] inputLines)
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

        public string FirstStar(string[] inputLines)
        {
            var groundPlan = parseGroundPlan(inputLines);
            WrapAround2DIndex index = new WrapAround2DIndex(groundPlan.GetLength(0), groundPlan.GetLength(1));

            int numOfTrees = 0;
            for (int y = 0; y < inputLines.Length; y++)
            {
                if (groundPlan[index.X, index.Y] == Foliage.Tree)
                    numOfTrees++;
                index.shift(3, 1);
            }
            return numOfTrees.ToString();
        }

        public string SecondStar(string[] inputLines)
        {
            throw new NotImplementedException();
        }
    }
}
