internal class InputHandler
{
    public string AskName()
    {
        Console.Write("What is your name? ");
        string name = Console.ReadLine()!;

        while (name == null || name == "")
        {
            Console.Write("Name can not be empty. Again: ");
            name = Console.ReadLine()!;
        }

        Console.Clear();
        return name;
    }

    public int AskForIntInRange(string text, int moreOrEqual, int lessOrEqual)
    {
        Console.Write(text);
        int input = Convert.ToInt32(Console.ReadLine()); // TODO: implement TryParse

        while (input < moreOrEqual || input > lessOrEqual)
        {
            NoSuchOption();
            input = Convert.ToInt32(Console.ReadLine());
        }

        Console.Clear();
        return input;
    }

    public bool AskConfirmExpeditionLaunch()
    {
        Console.Write("Confirm (yes/no) expedition launch: ");
        
        string input = Console.ReadLine()!;
        Console.Clear();

        if (input == "yes")
            return true;

        Console.WriteLine("You didn't say \"yes\" so I guess it's a no..");
        Console.WriteLine();
        return false;
    }

    public Enemy AskForTargetOfAttacker(Enemy[] enemies, Contractor attacker)
    {
        Console.Write($"Who you wish to attack with {attacker.Name}? ");
        Enemy target = (Enemy)GetCombatant(enemies)!;

        while (target == null || target.Health == 0)
        {
            NoSuchOption();
            target = (Enemy)GetCombatant(enemies)!;
        }

        Console.Clear();
        return target;
    }

    public Contractor AskForTargetOfHealer(Contractor[] contractors)
    {
        Console.Write("Which contractor you wish to heal? ");
        Contractor target = (Contractor)GetCombatant(contractors)!; // - 1 because combatants are displayed index + 1; TODO: implement TryParse

        while (target == null || target.Health == 0)
        {
            NoSuchOption();
            target = (Contractor)GetCombatant(contractors)!;
        }

        Console.Clear();
        return target;
    }

    private Combatant? GetCombatant(Combatant[] combatants)
    {
        int index = Convert.ToInt32(Console.ReadLine()) - 1; // - 1 because combatants are displayed index + 1; TODO: implement TryParse
        return index < combatants.Length ? combatants[index] : null;
    }

    public void WaitForEnter(string text)
    {
        Console.Write(text);
        Console.ReadLine();
        Console.Clear();
    }

    private void NoSuchOption() => Console.Write("No such option. Again: ");
}