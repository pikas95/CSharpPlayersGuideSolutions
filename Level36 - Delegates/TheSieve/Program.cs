Console.WriteLine("Type in operation(number): 1 - IsEven, 2 - IsPositive, 3 - MultipleOf10");
string input = Console.ReadLine();
Sieve sieve = input switch
{
    "1" => new Sieve(IsEven),
    "2" => new Sieve(IsPositive),
    "3" => new Sieve(MultipleOf10)
};
while (true)
{
    Console.Write("User, enter a number: ");
    Console.WriteLine(sieve.IsGood(Convert.ToInt32(Console.ReadLine())));
}
bool IsEven(int number) => number % 2 == 0;
bool IsPositive(int number) => number >= 0;
bool MultipleOf10(int number) => number == number % 10;
public class Sieve
{
    public Func<int, bool> operation;
    public Sieve(Func<int, bool> type) { operation = type; }
    public bool IsGood(int number) => operation(number);
}