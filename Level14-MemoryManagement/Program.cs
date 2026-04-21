int manticoreHealth = 10, cityHealth = 15, round = 1;

int manticoreDistance = AskForNumberInRange("Player 1, how far away from the city do you want to station the Manticore? ", 0, 100);
Console.Clear();

Console.WriteLine("Player 2, it is your turn.");
while (manticoreHealth > 0 && cityHealth > 0)
{
    Console.WriteLine("-----------------------------------------------------------");
    Console.WriteLine($"STATUS: Round: {round}  City: {cityHealth}/15  Manticore: {manticoreHealth}/10");

    int cannonDamage = DamageForRound(round);
    DisplayDamageForRound(cannonDamage);

    int targetRange = AskForNumberInRange("Enter desired cannon range: ", 0, 100);

    DisplayOverOrUnder(targetRange, manticoreDistance);
    if (targetRange == manticoreDistance) manticoreHealth -= cannonDamage;

    if (manticoreHealth > 0) cityHealth--;
    round++;
}

bool won = cityHealth > 0;
DisplayWinOrLose(won);

void DisplayWinOrLose(bool won)
{
    if (won)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("The Manticore has been destroyed! The city of Consolas has been saved!");
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("The city of Consolas has been destroyed...");
    }
    Console.ForegroundColor = ConsoleColor.White;
}

int DamageForRound(int round)
{
    if (round % 3 == 0 && round % 5 == 0) return 10;
    else if (round % 3 == 0) return 3;
    else if (round % 5 == 0) return 5;
    return 1;
}

void DisplayDamageForRound(int cannonDamage)
{
    Console.ForegroundColor = cannonDamage switch
    {
        10 => ConsoleColor.Blue,
        5 => ConsoleColor.Red,
        3 => ConsoleColor.Yellow,
        _ => ConsoleColor.White
    };
    Console.WriteLine($"The cannon is expected to deal {cannonDamage} damage this round.");
    Console.ForegroundColor = ConsoleColor.White;
}

void DisplayOverOrUnder(int targetRange, int manticoreDistance)
{
    Console.ForegroundColor = ConsoleColor.Magenta;
    if (targetRange > manticoreDistance)      Console.WriteLine("That round OVERSHOT the target.");
    else if (targetRange < manticoreDistance) Console.WriteLine("That round FELL SHORT of the target.");
    else                                      Console.WriteLine("That round was a DIRECT HIT!");
    Console.ForegroundColor = ConsoleColor.White;
}

int AskForNumberInRange(string text, int min, int max)
{
    while (true)
    {
        Console.Write(text);
        int input = Convert.ToInt32(Console.ReadLine());
        if (input >= min && input <= max)
            return input;
        Console.WriteLine($"Try between {min} and {max}.");
    }
}