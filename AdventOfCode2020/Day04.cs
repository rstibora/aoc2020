namespace AdventOfCode2020
{
    internal class Day04 : IDay
    {
        public string FirstStar(string[] inputLines)
        {
            var passportStrings = string.Join("\n", inputLines).Split("\n\n");
            var validityTests = new[] { 
                (string passportString) => passportString.Contains("byr:"),
                (string passportString) => passportString.Contains("iyr:"),
                (string passportString) => passportString.Contains("eyr:"),
                (string passportString) => passportString.Contains("hgt:"),
                (string passportString) => passportString.Contains("hcl:"),
                (string passportString) => passportString.Contains("ecl:"),
                (string passportString) => passportString.Contains("pid:"),

            };
            int validPassportsCount = 0;
            for (int i = 0; i < passportStrings.Length; i++)
                if (validityTests.All(test => test(passportStrings[i])))
                    validPassportsCount++;
            return validPassportsCount.ToString();
        }

        public string SecondStar(string[] inputLines)
        {
            throw new NotImplementedException();
        }
    }
}
