string name = Console.ReadLine()!;
int score = 0;
if (File.Exists($"{name}.txt"))
    score = Convert.ToInt32(File.ReadAllText($"{name}.txt"));
while (Console.ReadKey().Key != ConsoleKey.Enter)
{
    score++;
    Console.SetCursorPosition(0, 0);
    Console.WriteLine($"{name} score: {score}");
}
File.WriteAllText($"{name}.txt", score.ToString());