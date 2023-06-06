using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.WriteLine("Plan Your Heist!");

        //store team members
        List<IRobber> rolodex = new List<IRobber>();

        // add initial team members
        rolodex.Add(new Hacker("Alonso", 60, 30));
        rolodex.Add(new Muscle("Bucky", 90, 40));
        rolodex.Add(new LockSpecialist("Cortana", 50, 20));
        rolodex.Add(new Hacker("Diesal", 30, 10));
        rolodex.Add(new Muscle("Ebenezer", 40, 10));

        Console.WriteLine($"Current number of operatives in rolodex: {rolodex.Count}");

        // prompt user to add new members
        while (true)
        {

        Console.Write("Enter team member's name (or leave blank to finish adding members): ");
        string name = Console.ReadLine();

        // if entered name is empty exit the loop
        if (string.IsNullOrWhiteSpace(name))
        break;
        Console.WriteLine();

        Console.WriteLine("Select the team member's specialty:");
        Console.WriteLine("-----------------------------------");
        Console.WriteLine("1. Hacker (disables alarms)");
        Console.WriteLine("2. Muscle (disarms guards)");
        Console.WriteLine("3. Lock picker (cracks vaults)");
        Console.Write("Enter specialty number:  ");
        int specialtyNumber = int.Parse(Console.ReadLine());

        Console.Write("Enter team member's skill level: ");
        int skillLevel = int.Parse(Console.ReadLine());

        Console.Write("Enter team member's cut percentage: ");
        int percentageCut = int.Parse(Console.ReadLine());

        IRobber teamMember;

        // create newmember based on selected specialty number
        switch (specialtyNumber)
        {
            case 1:
                teamMember = new Hacker(name, skillLevel, percentageCut);
                break;
            case 2: 
                teamMember = new Muscle(name, skillLevel, percentageCut);
                break;
            case 3:
                teamMember = new LockSpecialist(name, skillLevel, percentageCut);
                break;
            default:
                Console.WriteLine("Invalid specialty number. Skipping team member.");
                continue;
        }

        // add the team member to rolodex
        rolodex.Add(teamMember);
        Console.WriteLine();

        }
    Console.WriteLine();

        // creating new bank object with random values
    Bank bank = new Bank()
    {
        AlarmScore = new Random().Next(0, 100),
        VaultScore = new Random().Next(0, 100),
        SecurityGuardScore = new Random().Next(0, 100),
        CashOnHand = new Random().Next(50000, 1000000)
    };

        // printing recon report
    Console.WriteLine("--Recon Report--");
    Console.WriteLine("----------------");
    Console.WriteLine($"Most Secure: {GetMostSecureSystem(bank)}");
    Console.WriteLine($"Least Secure: {GetLeastSecureSystem(bank)}");
    Console.WriteLine();
    
        // printing rolo report
    Console.WriteLine("Rolodex Report: ");
    Console.WriteLine("----------------");
    for (int i = 0; i < rolodex.Count; i++)
    {
        IRobber robber = rolodex[i];
        Console.WriteLine($"Index: {i}, Name: {robber.Name}, Specialty: {robber.GetSpecialty()}, Skill Level: {robber.SkillLevel}, Cut Percentage: {robber.PercentageCut}");
    }

    Console.WriteLine();

    // create new list to store selected members
    List<IRobber> team = new List<IRobber>();

    while (true)
    {
        Console.Write("Enter index of the operative you'd like on the team (or leave blank to finish adding): ");
        string indexInput = Console.ReadLine();

        // if left empty exit loop
        if (string.IsNullOrWhiteSpace(indexInput))
        break;

        int index = int.Parse(indexInput);

        if (index < 0 || index >= rolodex.Count)
        {
            Console.WriteLine("Invalid index. skipping team member");
            continue;
        }

        IRobber selectedOp = rolodex[index];
        
        // add the selected operative to team
        team.Add(selectedOp);
        Console.WriteLine($"{selectedOp.Name} added to the team");
        Console.WriteLine();
    }

    Console.WriteLine();

        // perform skills on the bank
    foreach (IRobber teamMember in team)
    {
        teamMember.PerformSkill(bank);
    }
        // eval the heist results
    if (bank.IsSecure)
    {
        Console.WriteLine("");
        Console.WriteLine("The heist was a FAILURE bank is still secure!");
        Console.WriteLine("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^");
    }
    else
    {
        Console.WriteLine();
        Console.WriteLine("The heist was a SUCCESS bank is no longer secure!");
        Console.WriteLine("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^");
        Console.WriteLine();

        // sum up the percentage cuts of each member
        double totalPercentCut = team.Sum(c => c.PercentageCut);

        // retrieves total amount of cash in the bank
        int totalCash = bank.CashOnHand;

        //calculates the amount of money the team will get from the bank
        int lootAmount = (int)(totalCash * (totalPercentCut / 100));

        // subtracts the loot amount from the total to show remaining cash
        int remainingCash = totalCash - lootAmount;

        //calculates the loot for each member and prints out a report
        Console.WriteLine("--Loot Report--");
        Console.WriteLine("---------------");
        foreach (IRobber teamMember in team)
        {
            int memberLoot = (int)(lootAmount * (teamMember.PercentageCut / (double)totalPercentCut));
            Console.WriteLine($"{teamMember.Name} gets {memberLoot} from the job.");
        }
        Console.WriteLine();
        Console.WriteLine($"Remaining cash: {remainingCash}");
        }
    }

    // get most secure system
        static string GetMostSecureSystem(Bank bank)
    {
         if (bank.AlarmScore > bank.VaultScore && bank.AlarmScore >   bank.SecurityGuardScore)
            return "Alarm";
        else if (bank.VaultScore > bank.AlarmScore && bank.VaultScore >  bank.SecurityGuardScore)
            return "Vault";
        else
            return "Securtiy Guard";
    }

        //get least secure system
        static string GetLeastSecureSystem(Bank bank)
    {
        if (bank.AlarmScore < bank.VaultScore && bank.AlarmScore < bank.SecurityGuardScore)
            return "Alarm";
        else if (bank.VaultScore < bank.AlarmScore && bank.AlarmScore < bank.SecurityGuardScore)
             return "Vault";
         else
             return "Security Guard";
    }

    // check if the total percentage cut for the team is valid
    static bool ValidPercentage(List<IRobber> team, int percentageCut)
    {
        int totalPercentCut = team.Sum(c => c.PercentageCut);
        return totalPercentCut + percentageCut <= 100;
    }
}

interface IRobber
{
    string Name { get; }
    int SkillLevel { get; }
    int PercentageCut { get; }

    void PerformSkill(Bank bank);
    string GetSpecialty();
}








