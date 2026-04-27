Arrow arrow = GetArrow();
Console.WriteLine($"That arrow cost: {arrow.Cost} gold.");

Arrow GetArrow()
{
    Console.WriteLine("Choose which arrow to make:");
    Console.WriteLine($"[1] {Arrow.DisplayEliteArrow()}");
    Console.WriteLine($"[2] {Arrow.DisplayBeginnerArrow()}");
    Console.WriteLine($"[3] {Arrow.DisplayMarksmanArrow()}");
    Console.WriteLine("[4] Custom");

    string input = Console.ReadLine();
    return input switch
    {
        "1" => Arrow.CreateEliteArrow(),
        "2" => Arrow.CreateBeginnerArrow(),
        "3" => Arrow.CreateMarksmanArrow(),
        _ => new Arrow(GetArrowHead(), GetLength(), GetFletching())
    };
}
ArrowHead GetArrowHead()
{
    Console.Clear();
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
            cost += Fletching switch
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

    public static Arrow CreateEliteArrow() => new Arrow(ArrowHead.Steel, 95, Fletching.Plastic);
    public static string DisplayEliteArrow() => $"Arrowhead: {ArrowHead.Steel}, length: 95, fletching: {Fletching.Plastic}";

    public static Arrow CreateBeginnerArrow() => new Arrow(ArrowHead.Wood, 75, Fletching.GooseFeathers);
    public static string DisplayBeginnerArrow() => $"Arrowhead: {ArrowHead.Wood}, length: 75, fletching: {Fletching.GooseFeathers}";

    public static Arrow CreateMarksmanArrow() => new Arrow(ArrowHead.Steel, 65, Fletching.GooseFeathers);
    public static string DisplayMarksmanArrow() => $"Arrowhead: {ArrowHead.Steel}, length: 65, fletching: {Fletching.GooseFeathers}";
}

internal enum ArrowHead { Steel, Wood, Obsidian }
internal enum Fletching { Plastic, TurkeyFeathers, GooseFeathers }