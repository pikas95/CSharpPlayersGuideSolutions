internal class GameRenderer
{
    public void DisplayMainMenu()
    {
        Console.WriteLine("[1] Start Expedition\n" +
                          "[2] Enter Contractor Store\n" +
                          "[0] Exit");
    }

    public void DisplayContractorStoreActions(bool playerHasContractor)
    {
        Console.WriteLine("[1] Hire a contractor");

        if (playerHasContractor)
            Console.WriteLine("[2] Dismiss a contractor");
    }

    public void DisplayAfterEventActions()
    {
        Console.WriteLine("[1] Heal\n" +
                          "[2] Continue");
    }

    public void DisplayHealOptions(IHealer healer)
    {
        Console.WriteLine($"Healer: {healer.Name}");
        Console.WriteLine("[1] Heal Self\n" +
                          "[2] Heal Target\n" +
                          "[3] Heal All");
    }

    public void DisplayPlayerAndGP(Player player)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"{player.Name} | {player.GP} GP | Daily Rate: {player.DailyContractorsGPRate} GP"); ;
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
        Console.WriteLine("Your contractors: ");
        bool anyContractorsPrinted = false;

        for (int i = 0; i < contractors.Length; i++)
            if (contractors[i] != null)
            {
                Console.ForegroundColor = DetermineColorOfContractor(contractors[i]);
                Console.Write($"[{i + 1}] {contractors[i]}\n");
                Console.ResetColor();
                anyContractorsPrinted = true;
            }

        if (!anyContractorsPrinted)
            Console.WriteLine("no contractors hired");

        Console.WriteLine();
    }

    public void DisplayHirableContractors(Contractor[] pool)
    {
        Console.WriteLine("Hirable Contractors:");

        bool anyContractorsPrinted = false;

        for (int i = 0; i < pool.Length; i++)
            if (pool[i] != null)
            {
                Console.ForegroundColor = DetermineColorOfContractor(pool[i]);
                Console.Write($"[{i + 1}] {pool[i]}\n");
                Console.ResetColor();
                anyContractorsPrinted = true;
            }

        if (!anyContractorsPrinted)
            Console.WriteLine("Contractor pool is empty");

        Console.WriteLine();
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

    public void DisplayRoleTypeEventRequirements(RoleTypeEvent roleTypeEvent)
    {
        if (roleTypeEvent is TrapEvent)
            DisplayRules("disarming");
        else if (roleTypeEvent is PuzzleEvent)
            DisplayRules("solving");
        else if (roleTypeEvent is TreasureEvent)
            DisplayRules("picklocking");

        void DisplayRules(string action)
        {
            Console.WriteLine($"{roleTypeEvent.EventType} is an event that requires contractor with {roleTypeEvent.RoleRequired} role to try {action}");
        }
    }

    public void DisplayDoesPlayerMeetRequirements(Contractor? contractor)
    {
        if (contractor == null)
            Console.WriteLine("You do not have a contractor with required role");
        else
            Console.WriteLine($"Your contractor {contractor.Name} has required role and can try completing event");

        Console.WriteLine();
    }

    public void DisplayLeftTryCount(int leftTryCount)
    {
        Console.WriteLine($"Left tries: {leftTryCount}");
    }

    public void DisplayRoleTypeEventTryOutcome(bool isSolved)
    {
        if (isSolved)
            Console.WriteLine("Event was completed!");
        else
            Console.WriteLine("You failed");
    }

    public void DisplayBattleVersus(Contractor[] contractors, Enemy[] enemies)
    {
        for (int i = 0; i < (contractors.Length > enemies.Length ? contractors.Length : enemies.Length); i++)
        {
            Combatant? contractor = i < contractors.Length ? contractors[i] : null;
            Combatant? enemy = i < enemies.Length ? enemies[i] : null;

            if (contractor == null && enemy == null)
                return;
            
            PrintCombatant(contractor);

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(" | ");

            if (enemy != null)
                Console.Write($"[{i + 1}] ");

            PrintCombatant(enemy);

            Console.ResetColor();
            Console.WriteLine();
        }

        Console.WriteLine();

        void PrintCombatant(Combatant? combatant)
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

    public void DisplayExpeditionResult(Expedition expedition, Player player)
    {
        Console.Write("You've completed: ");
        DisplayExpedition(expedition);

        if (player.ContractorsAreWiped())
            Console.WriteLine("Your contractors were wiped out!");
        else
            DisplayOwnedContractors(player.Contractors);

        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine($"Completed events: {expedition.GetCompletedEventsCount()}/{expedition.Events.Length} | Gained GP: {expedition.RewardedGP}");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Alive contractors took {player.DailyContractorsGPRate} GP for the expedition");
        Console.WriteLine();
        Console.ResetColor();
    }

    public ConsoleColor DetermineColorOfContractor(Contractor contractor)
    {
        if (contractor is Fighter)
            return ConsoleColor.DarkYellow;
        if (contractor is Medic)
            return ConsoleColor.DarkBlue;

        return ConsoleColor.DarkGreen;
    }

    public void DisplayMessage(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ResetColor();
    }

    public void DisplayReturnOption() => Console.WriteLine("[0] to return");
}