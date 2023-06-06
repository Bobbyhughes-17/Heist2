using System;


class Hacker : IRobber
{
    public string Name { get; }
    public int SkillLevel { get; }
    public int PercentageCut { get; }

    public Hacker(string name, int skillLevel, int percentageCut)
    {
        Name = name;
        SkillLevel = skillLevel;
        PercentageCut = percentageCut;
    }

    public void PerformSkill(Bank bank)
    {
        // decrease alarm score by hackers skill level
        bank.AlarmScore -= SkillLevel;
        Console.WriteLine($"{Name} is hacking alarms. Decreased security {SkillLevel} points.");

        if (bank.AlarmScore <= 0)
        {
            Console.WriteLine($"{Name} HAS DISABLED THE ALARMS!");
        }
    }

    public string GetSpecialty()
    {
        return "Hacker";
    }
}
