int oatmealCookie = new Random().Next(10);
List<int> picked = new List<int>();
bool currentPlayer = false; // false - player 1, true - player 2
try
{
    while (true)
    {
        Console.Write($"Player {(currentPlayer ? "2" : "1")}, pick a cookie(0-9): ");
        int input = Convert.ToInt32(Console.ReadLine());
        while (input < 0 || input > 9)
        {
            Console.Write("There are only cookies between number 0-9! Pick a cookie: ");
            input = Convert.ToInt32(Console.ReadLine());
        }
        if (input == oatmealCookie) throw new Exception();
        if (!picked.Contains(input)) picked.Add(input);
        else
        {
            Console.WriteLine("This cookie was already eaten.");
            continue;
        }
        currentPlayer = currentPlayer ? false : true;
    }
}
catch (Exception) { Console.WriteLine($"Player {(currentPlayer ? "2" : "1")} Lost."); }