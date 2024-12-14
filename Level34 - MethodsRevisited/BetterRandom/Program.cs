Random random = new Random();
Console.WriteLine(random.NextDouble(25));
Console.WriteLine(random.NextString("up", "down", "left", "right"));
Console.WriteLine(random.CoinFlip(0.0000001) == true ? "heads" : "tails");
public static class RandomExtentions
{
    public static double NextDouble(this Random value, int upperClamp)
    {
        return value.NextDouble() * upperClamp;
    }
    public static string NextString(this Random value, params string[] choices)
    {
        Random length = new Random();
        return choices[length.Next(choices.GetLength(0))];
    }
    public static bool CoinFlip(this Random value, double heads = 0.5)
    {
        return value.NextDouble() < heads ? true : false;
    }
}