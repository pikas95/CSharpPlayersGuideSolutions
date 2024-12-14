Console.Write("Enter an integer-type number: ");
string inputInt = Console.ReadLine()!;
while (true)
{
    if (!int.TryParse(inputInt, out int result))
    {
        Console.Write("That's not an integer-type number. Please enter an integer-type number: ");
        inputInt = Console.ReadLine()!;
    }
    else
    {
        Console.WriteLine(result);
        break;
    }
}
Console.Write("Enter a double-type number: ");
string inputDouble = Console.ReadLine()!;
while (true)
{
    if (!double.TryParse(inputDouble, out double result))
    {
        Console.Write("That's not a double-type number. Please enter an double-type number: ");
        inputDouble = Console.ReadLine()!;
    }
    else
    {
        Console.WriteLine(result);
        break;
    }
}
Console.Write("Enter a boolean value: ");
string inputBool = Console.ReadLine()!;
while (true)
{
    if (!bool.TryParse(inputDouble, out bool result))
    {
        Console.Write("That's not a bool value. Please enter an bool value: ");
        inputDouble = Console.ReadLine()!;
    }
    else
    {
        Console.WriteLine(result);
        break;
    }
}