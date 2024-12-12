int healthManticore = 10, healthCity = 15;
int manticorePosition = new ComputerChooses().Location();
Console.Clear();
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
public interface IManticoreLocation { public int Location(); }
public class HumanChooses : IManticoreLocation
{
    public int Location()
    {
        Console.Write("Where do you want Manticore to be? ");
        return Convert.ToInt32(Console.ReadLine());
    }
}
public class ComputerChooses : IManticoreLocation
{
    public int Location() { return new Random().Next(0, 101); }
}