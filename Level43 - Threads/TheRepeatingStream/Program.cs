RecentNumbers numbers = new RecentNumbers();
Thread thread = new Thread(RandomNumber);
thread.Start(numbers);

while (Console.ReadKey(true).Key != ConsoleKey.None)
{
    lock (numbers)
    {
        if (numbers.LastNumber == numbers.BeforeLastNumber)
            Console.WriteLine("They are a match!");
        else Console.WriteLine("They are different.");
    }
}

void RandomNumber(object? obj)
{
    if (obj == null || obj is not RecentNumbers) return;
    RecentNumbers numbers = (RecentNumbers)obj;
    Random random = new Random();
    while (true)
    {
        lock (numbers)
        {
            numbers.BeforeLastNumber = numbers.LastNumber;
            numbers.LastNumber = random.Next(10);
            Console.WriteLine(numbers.LastNumber);
        }
        Thread.Sleep(1000);
    }
}

public class RecentNumbers
{
    public int LastNumber { get; set; }
    public int BeforeLastNumber { get; set; }
}