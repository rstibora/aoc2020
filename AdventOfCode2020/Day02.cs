namespace AdventOfCode2020;

class PasswordPolicy
{
    private char character;
    private uint minOccurences;
    private uint maxOccurences;

    public PasswordPolicy(char character, uint minOccurences, uint maxOccurences)
    {
        this.character = character;
        this.minOccurences = minOccurences;
        this.maxOccurences = maxOccurences;
    }

    public PasswordPolicy(string policy) : this(policy.Split(' ')[1][0], UInt32.Parse(policy.Split(' ')[0].Split('-')[0]), UInt32.Parse(policy.Split(' ')[0].Split('-')[1])) { }

    public bool checkPassword(string password)
    {
        var characterOccurences = password.Where((char character) => character == this.character).Count();
        return characterOccurences >= minOccurences && characterOccurences <= maxOccurences;
    }
}

static class Day02
{
    public static string FirstStar(string[] inputLines)
    {
        uint numberOfCompliantPasswords = 0;
        foreach (var line in inputLines)
        {
            var (policyString, password) = (line.Split(':')[0], line.Split(':')[1]);
            var policy = new PasswordPolicy(policyString);
            if (policy.checkPassword(password))
            {
                numberOfCompliantPasswords++;
            }
        }
        return numberOfCompliantPasswords.ToString();
    }
}
