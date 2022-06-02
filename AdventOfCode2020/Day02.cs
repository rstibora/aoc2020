namespace AdventOfCode2020;

using System.Globalization;

public abstract class PasswordPolicy
{
    protected readonly char character;
    protected readonly uint numberA;
    protected readonly uint numberB;

    public PasswordPolicy(char character, uint numberA, uint numberB)
    {
        this.character = character;
        this.numberA = numberA;
        this.numberB = numberB;
    }

    public PasswordPolicy(string policy) : this(policy.Split(' ')[1][0],
                                                UInt32.Parse(policy.Split(' ')[0].Split('-')[0], CultureInfo.InvariantCulture),
                                                UInt32.Parse(policy.Split(' ')[0].Split('-')[1], CultureInfo.InvariantCulture)) { }

    public abstract bool CheckPassword(string password);
}


public class RangePasswordPolicy : PasswordPolicy
{
    public RangePasswordPolicy(string policy) : base(policy) { }

    public override bool CheckPassword(string password)
    {
        var characterOccurences = password.Where((char character) => character == this.character).Count();
        return characterOccurences >= numberA && characterOccurences <= numberB;
    }
}

public class PositionPasswordPolicy : PasswordPolicy
{
    public PositionPasswordPolicy(string policy) : base(policy) { }

    public override bool CheckPassword(string password)
    {
        if (password[Convert.ToInt32(numberA) - 1] == character && password[Convert.ToInt32(numberB) - 1] != character)
            return true;
        if (password[Convert.ToInt32(numberA) - 1] != character && password[Convert.ToInt32(numberB) - 1] == character)
            return true;
        return false;
    }
}

class Day02 : IDay
{
    public string FirstStar(string[] inputLines)
    {
        uint numberOfCompliantPasswords = 0;
        foreach (var line in inputLines)
        {
            var (policyString, password) = (line.Split(':')[0], line.Split(':')[1].Trim());
            var policy = new RangePasswordPolicy(policyString);
            if (policy.CheckPassword(password))
            {
                numberOfCompliantPasswords++;
            }
        }
        return numberOfCompliantPasswords.ToString();
    }

    public string SecondStar(string[] inputLines)
    {
        uint numberOfCompliantPasswords = 0;
        foreach (var line in inputLines)
        {
            var (policyString, password) = (line.Split(':')[0], line.Split(':')[1].Trim());
            var policy = new PositionPasswordPolicy(policyString);
            if (policy.CheckPassword(password))
            {
                numberOfCompliantPasswords++;
            }
        }
        return numberOfCompliantPasswords.ToString();
    }
}
