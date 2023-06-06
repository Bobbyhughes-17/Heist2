using System;


class LockSpecialist : IRobber
{
    public string Name { get; }
    public int SkillLevel { get; }
    public int PercentageCut { get; }

    public LockSpecialist(string name, int skillLevel, int percentageCut)
    {
        Name = name;
        SkillLevel = skillLevel;
        PercentageCut = percentageCut;
    }

    public void PerformSkill(Bank bank)
    {
        // decrease the vault score by skill level of the lock picker
        bank.VaultScore -= SkillLevel;
        Console.WriteLine($"{Name} is cracking the vault. Decreased security {SkillLevel} points");

        if (bank.VaultScore <= 0)
        {
            Console.WriteLine($"{Name} HAS CRACKED THE VAULT!");
        }
    }

    public string GetSpecialty()
    {
        return "Lock Specialist";
    }
}
