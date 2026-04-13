Console.WriteLine("What are enemy coordinates?");
Console.Write("x: ");
int x = Convert.ToInt32(Console.ReadLine());
Console.Write("y: ");
int y = Convert.ToInt32(Console.ReadLine());
if (x == 0)
{
    if (y > 0) Console.WriteLine("The enemy is to the North");
    else if (y == 0) Console.WriteLine("The enemy is here!");
    else Console.WriteLine("The enemy is to the South");
}
else if (x > 0)
{
    if (y > 0) Console.WriteLine("The enemy is to the Northeast");
    else if (y == 0) Console.WriteLine("The enemy is to the East");
    else Console.WriteLine("The enemy is to the Southeast");
}
else
{
    if (y > 0) Console.WriteLine("The enemy is to the Northwest");
    else if (y == 0) Console.WriteLine("The enemy is to the West");
    else Console.WriteLine("The enemy is to the Southwest");
}
// instead of going through 8 ifs to get to last option, here it gets to it in 4 ifs

/* clever alternative:
if (x == 0)
{
    if (y == 0) Console.WriteLine("The enemy is here!");
    else Console.WriteLine(y > 0 ? "The enemy is to the North" : "The enemy is to the South");
}
else if (x > 0)
{
    if (y == 0) Console.WriteLine("The enemy is to the East");
    else Console.WriteLine(y > 0 ? "The enemy is to the Northeast" : "The enemy is to the Southeast");

}
else
{
    if (y == 0) Console.WriteLine("The enemy is to the West");
    else Console.WriteLine(y > 0 ? "The enemy is to the Northwest" : "The enemy is to the Southwest");
}*/