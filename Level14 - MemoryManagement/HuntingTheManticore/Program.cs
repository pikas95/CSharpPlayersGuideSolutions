int healthManticore = 10, healthCity = 15, min = 0, max = 100;
int manticorePosition = whereIsManticore(min, max);
int whereIsManticore(int min, int max)
{
    Console.Title = "Player 1";
    Console.Write("Player 1, how far from the city do you want to station the Manticore? ");
    return Convert.ToInt32(Console.ReadLine());
}
Console.Clear();

Console.Title = "Player 2";
Console.WriteLine("Player 2, it is your turn.");
for (int round = 1; healthManticore > 0 && healthCity > 0; round++)
{
    // DASHBOARD
    Console.WriteLine("-----------------------------------------------------------");
    Console.Write("STATUS: ");
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.Write($"Round: {round} ");
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write($"City: {healthCity}/15 ");
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"Manticore: {healthManticore}/10");
    Console.ForegroundColor = ConsoleColor.White;

    // CANNON DAMAGE
    int cannonDamage = ThisRoundCannonDamage(round);
    Console.Write("The cannon is expected to deal ");
    Console.ForegroundColor = ConsoleColor.DarkCyan;
    Console.Write(cannonDamage);
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine(" damage this round.");

    // CANNON SHOT
    Console.Write("Enter desired cannon range: ");
    int range = Convert.ToInt32(Console.ReadLine());

    if (range > manticorePosition)
        Console.WriteLine("That round OVERSHOT the target.");
    else if (range < manticorePosition)
        Console.WriteLine("That round FELL SHORT of the target.");
    else
    {
        Console.WriteLine("That round was a DIRECT HIT!");
        healthManticore -= cannonDamage;
    }

    if(healthManticore > 0)
        healthCity--;
}

// END OF GAME MESSAGE
Console.WriteLine();
Console.ForegroundColor = ConsoleColor.Magenta;
if (healthManticore <= 0)
    Console.WriteLine("The Manticore has been destroyed! The City of Consolas has been saved!");
else 
    Console.WriteLine("The City has been destroyed... RETREAT!");
Console.ForegroundColor = ConsoleColor.White;

int ThisRoundCannonDamage(int round)
{
    if (round % 3 == 0 && round % 5 == 0)
        return 10;
    else if (round % 3 == 0)
        return 3;
    else if (round % 5 == 0)
        return 3;
    else
        return 1;
}