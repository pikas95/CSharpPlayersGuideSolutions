internal class GameRenderer
{
    public void DisplayMainMenu()
    {
        Console.WriteLine("[1] Start Expedition\n" +
                          "[2] Enter Contractor Store");
    }

    public void DisplayPlayerAndGP(Player player)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"{player.Name} | {player.GP} GP");
        Console.ResetColor();
    }

    public void DisplayAllExpeditions(Expedition[] expeditions)
    {
        Console.WriteLine("Available Expeditions:");

        for (int i = 0; i < expeditions.Length; i++)
        {
            Console.ForegroundColor = expeditions[i].Difficulty switch
            {
                Difficulty.Easy => ConsoleColor.Green,
                Difficulty.Medium => ConsoleColor.DarkYellow,
                Difficulty.Hard => ConsoleColor.Red,
            };
            Console.WriteLine($"[{i + 1}] {expeditions[i]}");
        }
        Console.ResetColor();
    }

    public void DisplayExpedition(Expedition expedition)
    {
        Console.ForegroundColor = DetermineColorOfExpedition(expedition);
        Console.WriteLine($"{expedition}");
        Console.ResetColor();
    }

    public void DisplayExpeditionProgress(int currentEvent, int totalEvents)
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write($"{currentEvent}/{totalEvents} ");
        Console.ResetColor();
    }

    private ConsoleColor DetermineColorOfExpedition(Expedition expedition)
    {
        return Console.ForegroundColor = expedition.Difficulty switch
        {
            Difficulty.Easy => ConsoleColor.Green,
            Difficulty.Medium => ConsoleColor.DarkYellow,
            Difficulty.Hard => ConsoleColor.Red,
        };
    }

    public void DisplayRolesReport(string report)
    {
        Console.WriteLine(report);
        Console.WriteLine();
    }

    public void DisplayMissingRolesReport(string report)
    {
        Console.WriteLine(report);
        Console.WriteLine();
    }

    public void DisplayOwnedContractors(Contractor[] contractors)
    {
        string contractorsReport = null!;
        foreach (Contractor contractor in contractors) 
            if (contractor != null)
                contractorsReport += $"{contractor}\n";

        Console.WriteLine(contractorsReport == null ? "No contractors hired" : "Your Contractors:\n" + contractorsReport);
    }

    public void DisplayHirableContractors(Contractor[] pool)
    {
        string poolText = null!;

        for (int i = 0; i < pool.Length; i++)
            if (pool[i] != null)
                poolText += $"[{i + 1}] {pool[i]}\n";

        Console.WriteLine(poolText == null ? "Contractor pool is empty\n" : poolText);
    }

    public void DisplayEncounteredEvent(ExpeditionEvent expeditionEvent)
    {
        Console.Write("Encountered Event: ");

        Console.ForegroundColor = DetermineColorOfEvent(expeditionEvent);
        Console.WriteLine(expeditionEvent);
        Console.ResetColor();
    }
    private ConsoleColor DetermineColorOfEvent(ExpeditionEvent expeditionEvent)
    {
        if (expeditionEvent is AssaultEvent)
            return ConsoleColor.DarkRed;

        return ConsoleColor.Blue;
    }

    public void DisplayEvent(ExpeditionEvent expeditionEvent)
    {
        Console.ForegroundColor = DetermineColorOfEvent(expeditionEvent);
        Console.WriteLine($"{expeditionEvent}");
        Console.ResetColor();
    }

    public void DisplayAssaultEvent(AssaultEvent assaultEvent, int round)
    {
        Console.ForegroundColor = DetermineColorOfEvent(assaultEvent);
        Console.Write($"{assaultEvent.Name}");
        Console.WriteLine($" | Round: {round}");
        Console.ResetColor();
    }

    public void DisplayBattleVersus(Contractor[] contractors, Enemy[] enemies)
    {
        for (int i = 0; i < (contractors.Length > enemies.Length ? contractors.Length : enemies.Length); i++)
        {
            Combatant contractor = i < contractors.Length ? contractors[i] : null!;
            Combatant enemy = i < enemies.Length ? enemies[i] : null!;

            if (contractor == null && enemy == null)
                return;

            
            PrintCombatant(contractor!);

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(" | ");

            Console.Write($"[{i + 1}] ");
            PrintCombatant(enemy!);

            Console.WriteLine();
        }

        Console.WriteLine();

        void PrintCombatant(Combatant combatant)
        {
            if (combatant != null)
            {
                if (combatant.Health > combatant.MaxHealth / 2)
                    Console.ForegroundColor = ConsoleColor.Green;
                else if (combatant.Health > combatant.MaxHealth / 5)
                    Console.ForegroundColor = ConsoleColor.Yellow;
                else
                    Console.ForegroundColor = ConsoleColor.Red;

                Console.Write($"{combatant.Name,-15} - {combatant.Health,3}/{combatant.MaxHealth,-3} HP");
                Console.ResetColor();
            }
            else
                Console.Write($"{" ", -28}");
        }
    }

    public void DisplayEnemyAttackMessage()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Enemies attacked your team!");
        Console.ResetColor();
    }

    public void DisplayBattleOutcome(bool playerWon)
    {
        if (playerWon)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("You've defeated all enemies!");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You've lost...");
        }
        Console.ResetColor();
    }

    public void DisplayReturnOption() => Console.WriteLine("[0] to return");
}