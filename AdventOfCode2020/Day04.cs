using System.Text.RegularExpressions;

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
            return CountValidPassports(passportStrings, validityTests).ToString();
        }

        public string SecondStar(string[] inputLines)
        {
            Func<string, bool> ruleWrapper(string fieldName, Func<string, bool> detailedCheck)
            {
                return (string passportString) =>
                {
                    if (!passportString.Contains(fieldName))
                        return false;
                    return detailedCheck(passportString.Split(fieldName)[1].Split(' ', '\n')[0]);
                };
            }

            var passportStrings = string.Join("\n", inputLines).Split("\n\n");
            var validityTests = new[] {
                ruleWrapper("byr:", dataFragment => dataFragment.Length == 4 && Int32.Parse(dataFragment) >= 1920 && Int32.Parse(dataFragment) <= 2002),
                ruleWrapper("iyr:", dataFragment => dataFragment.Length == 4 && Int32.Parse(dataFragment) >= 2010 && Int32.Parse(dataFragment) <= 2020),
                ruleWrapper("eyr:", dataFragment => dataFragment.Length == 4 && Int32.Parse(dataFragment) >= 2020 && Int32.Parse(dataFragment) <= 2030),
                ruleWrapper("hgt:", dataFragment =>
                {
                    return dataFragment[^2..] switch {
                        "cm" => Int32.Parse(dataFragment[..^2]) >= 150 && Int32.Parse(dataFragment[..^2]) <= 193,
                        "in" => Int32.Parse(dataFragment[..^2]) >= 59 && Int32.Parse(dataFragment[..^2]) <= 76,
                        _ => false
                    };
                }),
                ruleWrapper("hcl:", dataFragment => new Regex(@"#[0-9a-f]{6,6}").Matches(dataFragment).Count == 1),
                ruleWrapper("ecl:", dataFragment => (new []{"amb", "blu", "brn", "gry", "grn", "hzl", "oth"}).Contains(dataFragment)),
                ruleWrapper("pid:", dataFragment => new Regex(@"[0-9]{9,9}").Matches(dataFragment).Count == 1),
            };
            return CountValidPassports(passportStrings, validityTests).ToString();
        }

        private static int CountValidPassports(string[] passportStrings, Func<string, bool>[] validityTests)
        {
            // TODO: remove the debug print once the second star solution is correct.
            foreach (string strr in (passportStrings.Where(passportString => validityTests.All(test => test(passportString)))))
            {
                System.Console.WriteLine(strr);
                System.Console.Write("---\n");
            };
            return passportStrings.Where(passportString => validityTests.All(test => test(passportString))).Count();
        }
    }
}
