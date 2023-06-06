
class Muscle : IRobber
{
    public string Name { get; }
    public int SkillLevel { get; }
    public int PercentageCut { get; }

    public Muscle(string name, int skillLevel, int percentageCut)
    {
        Name = name;
        SkillLevel = skillLevel;
        PercentageCut = percentageCut;
    }

    public void PerformSkill(Bank bank)
    {
        // decrease the security score by the skill level of the muscle
        bank.SecurityGuardScore -= SkillLevel;
        Console.WriteLine($"{Name} is disarming security guards. Decreased security {SkillLevel} points.");

        if (bank.SecurityGuardScore <= 0)
        {
            Console.WriteLine($"{Name} HAS DISABLED THE GUARDS!");
        }
    }

    public string GetSpecialty()
    {
        return "Muscle";
    }
}