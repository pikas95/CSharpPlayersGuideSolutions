void Countdown(int number)
{
    Console.WriteLine(number);
    if (number > 1)
        Countdown(number -  1);
}