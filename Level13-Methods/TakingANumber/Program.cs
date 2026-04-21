int AskForNumber(string text)
{
    Console.WriteLine(text);
    int input = Convert.ToInt32(Console.ReadLine());
    return input;
}
int AskForNumberInRange(string text, int min, int max)
{
    while (true)
    {
        Console.WriteLine(text);
        int input = Convert.ToInt32(Console.ReadLine());
        if (input >= min && input <= max)
            return input;
        Console.WriteLine("Let's try that again.");
    }
}