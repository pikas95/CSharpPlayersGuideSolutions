int number = AskForNumber("User, enter a number, please");
int AskForNumber(string text)
{
    Console.WriteLine(text);
    return Convert.ToInt32(Console.ReadLine());
}
Console.WriteLine(number);
number = AskForNumberInRange("User, enter a number between these numbers", 1, 10);
int AskForNumberInRange(string text, int min, int max)
{
    int input;
    while (true)
    {
        Console.WriteLine(text + " " + min + " " + max);
        input = Convert.ToInt32(Console.ReadLine());
        if (input >= min && input <= max)
            break;
        Console.WriteLine("That won't work...");
    }
    return input;
}
Console.WriteLine(number);