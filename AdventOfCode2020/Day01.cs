namespace AdventOfCode2020
{ 
    static class Day01
    {
        public static string FirstStar(string[] inputLines)
        {
            var inputNumbers = inputLines.Select(Int32.Parse).ToArray();

            for (int i = 0; i < inputNumbers.Length; i++)
            {
                for (int j = i + 1; j < inputNumbers.Length; j++)
                {
                    if (inputNumbers[i] + inputNumbers[j] == 2020)
                    {
                        return (inputNumbers[i] * inputNumbers[j]).ToString();
                    }
                }
            }
            throw new Exception("There is not a pair of entries that sum to 2020.");
        }
    }
}
