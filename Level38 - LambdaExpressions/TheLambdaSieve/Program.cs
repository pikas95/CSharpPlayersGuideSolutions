Console.WriteLine("Type in operation(number): 1 - IsEven, 2 - IsPositive, 3 - MultipleOf10");
string input = Console.ReadLine();
Sieve sieve = input switch
{
    "1" => new Sieve(n => n % 2 == 0),
    "2" => new Sieve(n => n >= 0),
    "3" => new Sieve(n => n % 10 == n)
};
while (true)
{
    Console.Write("User, enter a number: ");
    Console.WriteLine(sieve.IsGood(Convert.ToInt32(Console.ReadLine())));
}
public class Sieve
{
    public Func<int, bool> operation;
    public Sieve(Func<int, bool> type) { operation = type; }
    public bool IsGood(int number) => operation(number);
}