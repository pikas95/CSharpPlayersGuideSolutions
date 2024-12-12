namespace FountainOfObjectsGame
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("Do you want to play in small, medium or large game?");
            string input = Console.ReadLine()!;
            while (input != "small" && input != "medium" && input != "large")
            {
                Console.WriteLine("That's not a valid option! Try again");
                input = Console.ReadLine()!;
            }
            int gameSize;
            gameSize = input switch
            {
                "small" => 2,
                "medium" => 3,
                "large" => 4,
                _ => 2
            };
            Console.Clear();

            new Game(gameSize).Run();
        }
    }
}