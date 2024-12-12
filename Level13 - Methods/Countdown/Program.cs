Recursion(10);
void Recursion(int number)
{
    if (number > 0)
    {
        Console.WriteLine(number);
        number--;
        Recursion(number);
    }
}