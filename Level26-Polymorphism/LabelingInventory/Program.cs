Pack pack = new Pack(5, 5, 5);
while (true)
{
    DisplayPack();

    DisplayAvailableItems();

    if (!pack.Add(AskForItem()))
        Console.WriteLine("\nItem can not be added");

    Console.Write("To continue press enter");
    Console.ReadLine();
    Console.Clear();
}

void DisplayPack()
{
    Console.WriteLine($"{pack.ToString()}");
    Console.WriteLine($"Items Stashed: {pack.ItemCount} | Items Max: {pack.ItemsMax}");
    Console.WriteLine($"Weight: {pack.Weight:0.00} | Weight Max: {pack.WeightMax}");
    Console.WriteLine($"Volume: {pack.Volume:0.00} | Volume Max: {pack.VolumeMax}");
    Console.WriteLine("-------------------------------------------------------------");
}

void DisplayAvailableItems()
{
    Console.WriteLine("Available items to add:\n" +
                      "[1] Arrow\n" +
                      "[2] Bow\n" +
                      "[3] Rope\n" +
                      "[4] Water\n" +
                      "[5] Food\n" +
                      "[6] Sword");
}

InventoryItem AskForItem()
{
    Console.Write("What would you like to add? ");

    return Console.ReadLine() switch
    {
        "1" => new Arrow(),
        "2" => new Bow(),
        "3" => new Rope(),
        "4" => new Water(),
        "5" => new Food(),
        "6" => new Sword(),
    };
}
internal class Pack
{
    public InventoryItem[] Inventory { get; }
    public int ItemCount { get; protected set; }
    public float Weight { get; protected set; }
    public float Volume { get; protected set; }
    public int ItemsMax { get; }
    public float WeightMax { get; }
    public float VolumeMax { get; }
    public Pack(int itemsMax, float weightMax, float volumeMax)
    {
        Inventory = new InventoryItem[itemsMax];
        ItemsMax = itemsMax;
        WeightMax = weightMax;
        VolumeMax = volumeMax;
    }
    public bool Add(InventoryItem item)
    {
        if (Weight + item.Weight > WeightMax || Volume + item.Volume > VolumeMax || ItemCount == Inventory.Length)
            return false;

        Inventory[ItemCount] = item;
        Weight += item.Weight;
        Volume += item.Volume;
        ItemCount++;
        return true;
    }
    public override string ToString()
    {
        string text = "Pack containing";
        foreach (InventoryItem item in Inventory)
        {
            if (item != null)
                text += $" {item.ToString()}";
        }

        if (text == "Pack containing")
            return "Pack is empty";
        return text;
    }
}
internal class InventoryItem
{
    public float Weight { get; }
    public float Volume { get; }
    public InventoryItem(float weight, float volume)
    {
        Weight = weight;
        Volume = volume;
    }
}
internal class Arrow : InventoryItem 
{
    public Arrow() : base(0.1f, 0.05f) { }
    public override string ToString() => "Arrow";
}
internal class Bow : InventoryItem 
{
    public Bow() : base(1, 4) { }
    public override string? ToString() => "Bow";
}
internal class Rope : InventoryItem 
{
    public Rope() : base(1, 1.5f) { }
    public override string? ToString() => "Rope";
}
internal class Water : InventoryItem 
{
    public Water() : base(2, 3) { }
    public override string? ToString() => "Water";
}
internal class Food : InventoryItem 
{
    public Food() : base(1, 0.5f) { }
    public override string? ToString() => "Food";
}
internal class Sword : InventoryItem 
{ 
    public Sword() : base(5, 3) { }
    public override string? ToString() => "Sword";
}