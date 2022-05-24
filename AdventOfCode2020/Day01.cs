namespace AdventOfCode2020
{ 
    class Day01 : IDay
    {
        public string FirstStar(string[] inputLines)
        {
            var inputNumbers = inputLines.Select(Int32.Parse).ToArray();
            return FindNumbersWithSum(inputNumbers, 2020, 2).ToString();
        }

        public string SecondStar(string[] inputLines)
        {
            var inputNumbers = inputLines.Select(Int32.Parse).ToArray();
            return FindNumbersWithSum(inputNumbers, 2020, 3).ToString();
        }

        private static int FindNumbersWithSum(int[] numbers, int desiredSum, uint numberOfSumElements)
        {
            var indices = new uint[numberOfSumElements];
            for (uint i = 0; i < indices.Length; i++)
                indices[i] = i;

            do
            {
                if (indices.Select(index => numbers[index]).Sum() == desiredSum)
                {
                    return indices.Select(index => numbers[index]).Aggregate((a, b) => a * b);
                }

                indices[^1]++;

                for (int i = indices.Length - 1; i >= 1; i--)
                {
                    if (indices[i] == numbers.Length - (indices.Length - i))
                    {
                        indices[i - 1]++;
                    }
                }

                for (int i = 1; i < indices.Length; i++)
                {
                    if (indices[i] == numbers.Length - (indices.Length - i))
                    {
                        indices[i] = indices[i - 1] + 1;
                    }
                }
            }
            while (indices[0] < numbers.Length - indices.Length + 1);

            throw new Exception($"There are no {numberOfSumElements} numbers with sum of {desiredSum}.");
        }

    }
}
