Console.WriteLine("What is the x value?");
int x = Convert.ToInt32(Console.ReadLine());
Console.WriteLine("What is the y value?");
int y = Convert.ToInt32(Console.ReadLine());
if (x > 0)
{
    if (y > 0)
        Console.WriteLine("The enemy is to the Northeast!");
    else if (y < 0)
        Console.WriteLine("The enemy is to the Southeast!");
    else
        Console.WriteLine("The enemy is to the East!");
}
else if (x < 0)
{
    if (y > 0)
        Console.WriteLine("The enemy is to the Northwest!");
    else if (y < 0)
        Console.WriteLine("The enemy is to the Southwest!");
    else
        Console.WriteLine("The enemy is to the West!");
}
else
{
    if (y > 0)
        Console.WriteLine("The enemy is to the North!");
    else if (y < 0)
        Console.WriteLine("The enemy is to the South!");
    else
        Console.WriteLine("The enemy is here!");
}