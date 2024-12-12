Pack pack = new Pack();
while (true)
{
    Console.WriteLine(pack.ToString());
    Console.WriteLine();
    Console.WriteLine("Choose an item to add.");
    Console.WriteLine("1 - Arrow, 2 - Bow, 3 - Rope, 4 - Water, 5 - Food, 6 - Sword");
    ConsoleKey choice = Console.ReadKey().Key;
    bool successAddingItem = choice switch
    {
        ConsoleKey.NumPad1 => pack.Add(new Arrow()),
        ConsoleKey.NumPad2 => pack.Add(new Bow()),
        ConsoleKey.NumPad3 => pack.Add(new Rope()),
        ConsoleKey.NumPad4 => pack.Add(new Water()),
        ConsoleKey.NumPad5 => pack.Add(new Food()),
        ConsoleKey.NumPad6 => pack.Add(new Sword()),
        _ => false
    };
    Console.WriteLine();
    if (!successAddingItem)
        Console.WriteLine("You can't add that!");
    Console.WriteLine("------------------------------------------------------------");
    Console.WriteLine();
    Console.WriteLine(pack.Items[0].ToString());
}
public class Pack
{
    public static readonly int ItemsMaximum = 12;
    public static readonly double WeightMaximum = 10;
    public static readonly double VolumeMaximum = 11;
    public InventoryItem[] Items { get; private set; } = new InventoryItem[ItemsMaximum];
    public int TotalItems { get; private set; } = 0;
    public double Weight { get; private set; } = 0;
    public double Volume { get; private set; } = 0;
    public bool Add(InventoryItem item)
    {
        if (item.Weight + Weight <= WeightMaximum && item.Volume + Volume <= VolumeMaximum && TotalItems + 1 <= ItemsMaximum)
        {
            Items[TotalItems] = item;
            Weight += item.Weight;
            Volume += item.Volume;
            TotalItems++;
            return true;
        }
        return false;
    }
    public void DisplayPackContents()
    {
        Console.WriteLine($"Total items in Pack: {TotalItems}, Pack Weight: {Weight:f2}, Pack Volume: {Volume:f2}");
        Console.WriteLine($"Max Items: {ItemsMaximum}, Max Weight: {WeightMaximum}, Max Volume: {VolumeMaximum}");
    }
    public override string ToString()
    {
        string response = "Pack containing";
        for (int i = 0; i < TotalItems; i++)
            response += " " + Items[i].ToString();
        if (response == "Pack containing")
            return "Pack is Empty";
        return response;
    }
}
public class InventoryItem
{
    public double Weight { get; }
    public double Volume { get; }
    public InventoryItem(double weight, double volume)
    {
        Weight = weight; Volume = volume;
    }
}
public class Arrow : InventoryItem
{
    public Arrow() : base(0.1, 0.05)
    {
    }
    public override string ToString()
    {
        return "Arrow";
    }
}
public class Bow : InventoryItem
{
    public Bow() : base(1, 4)
    {
    }
    public override string ToString()
    {
        return "Bow";
    }
}
public class Rope : InventoryItem
{
    public Rope() : base(1, 1.5)
    {
    }
    public override string ToString()
    {
        return "Rope";
    }
}
public class Water : InventoryItem
{
    public Water() : base(2, 3)
    {
    }
    public override string ToString()
    {
        return "Water";
    }
}
public class Food : InventoryItem
{
    public Food() : base(1, 0.5)
    {
    }
    public override string ToString()
    {
        return "Food";
    }
}
public class Sword : InventoryItem
{
    public Sword() : base(5, 3)
    {
    }
    public override string ToString()
    {
        return "Sword";
    }
}