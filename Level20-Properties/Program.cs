Arrow arrow = new Arrow(GetArrowHead(), GetLength(), GetFletching());
Console.WriteLine($"That arrow cost: {arrow.Cost} gold.");
ArrowHead GetArrowHead()
{
    Console.Write("Pick arrowhead 1-3 (1 - steel, 2 - wood, 3 - obsidian): ");
    while (true)
    {
        // user could write names and code would check with if's for return option
        // but this approach makes it harder for user to misstype
        int input = Convert.ToInt32(Console.ReadLine());
        if (input >= 1 && input <= 3)
        {
            return input switch
            {
                1 => ArrowHead.Steel,
                2 => ArrowHead.Wood,
                3 => ArrowHead.Obsidian
            };
        }
        Console.WriteLine("There's no such arrowhead.");
    }
}

int GetLength()
{
    Console.Write("Choose shaft length (between 60 and 100): ");
    while (true)
    {
        int input = Convert.ToInt32(Console.ReadLine());
        if (input >= 60 && input <= 100) return input;
        Console.WriteLine("We don't make shafts that length.");
    }
}

Fletching GetFletching()
{
    Console.Write("Pick flecthing 1-3 (1 - plastic, 2 - turkey feathers, 3 - goose feathers): ");
    while (true)
    {
        int input = Convert.ToInt32(Console.ReadLine());
        if (input >= 1 && input <= 3)
        {
            return input switch
            {
                1 => Fletching.Plastic,
                2 => Fletching.TurkeyFeathers,
                3 => Fletching.GooseFeathers
            };
        }
        Console.WriteLine("There's no such arrowhead.");
    }
}

internal class Arrow
{
    public ArrowHead ArrowHead { get; }
    public int Length { get; }
    public Fletching Fletching { get; }
    public float Cost
    {
        get
        {
            float cost = ArrowHead switch
            {
                ArrowHead.Steel => 10,
                ArrowHead.Wood => 3,
                ArrowHead.Obsidian => 5
            };
            cost += Length * 0.05f;
            cost +=  Fletching switch
            {
                Fletching.Plastic => 10,
                Fletching.TurkeyFeathers => 5,
                Fletching.GooseFeathers => 3
            };
            return cost;
        }
    }

    public Arrow() : this(ArrowHead.Wood, 60, Fletching.GooseFeathers) // cheapest, if not picked by player
    { }

    public Arrow(ArrowHead arrowHead, int length, Fletching fletching)
    {
        ArrowHead = arrowHead;
        Length = length;
        Fletching = fletching;
    }
}

internal enum ArrowHead { Steel, Wood, Obsidian }
internal enum Fletching { Plastic, TurkeyFeathers, GooseFeathers }