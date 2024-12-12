Arrow arrow;
Console.Write("What kind of arrow you want to buy (Elite, Beginner, Marksman, Custom): ");
string input = Console.ReadLine();
if (input == "Custom")
    arrow = GetArrow();
else
    arrow = Arrow.CreateEliteArrow(input);

Console.WriteLine($"Arrow Cost for {arrow._fletching} {arrow._arrowHead} arrow that is {arrow._shaft} cm length: {arrow.GetCost()}");
Arrow GetArrow()
{
    Arrowhead arrowhead = GetArrowhead();
    Fletching fletching = GetFletching();
    float length = GetLength();
    return new Arrow(arrowhead, fletching, length);
}
Arrowhead GetArrowhead()
{
    Console.Write("Select Arrowhead (Steel, Wood, Obsidian): ");
    string input = Console.ReadLine();
    return input switch
    {
        "Steel" => Arrowhead.Steel,
        "Wood" => Arrowhead.Wood,
        "Obsidian" => Arrowhead.Obsidian
    };
}
Fletching GetFletching()
{
    Console.Write("Select Fletching (Plastic, Turkey Feathers, Goose Feathers): ");
    string input = Console.ReadLine();
    return input switch
    {
        "Plastic" => Fletching.Plastic,
        "Turkey Feathers" => Fletching.TurkeyFeathers,
        "Goose Feathers" => Fletching.GooseFeathers
    };
}
float GetLength()
{
    Console.Write("Type in Length between 60 and 100 cm: ");
    return Convert.ToSingle(Console.ReadLine());
}
class Arrow
{
    public Arrowhead _arrowHead { get; init; }
    public float _shaft { get; init; }
    public Fletching _fletching { get; init; }

    public Arrow(Arrowhead arrowhead, Fletching fletching, float shaft)
    {
        _arrowHead = arrowhead;
        _shaft = shaft;
        _fletching = fletching;
    }
    public static Arrow CreateEliteArrow(string arrowType)
    {
        return arrowType switch
        {
            "Elite" => new Arrow(Arrowhead.Steel, Fletching.Plastic, 95),
            "Beginner" => new Arrow(Arrowhead.Wood, Fletching.GooseFeathers, 75),
            "Marksman" => new Arrow(Arrowhead.Steel, Fletching.GooseFeathers, 65)
        };
    }
    public float GetCost()
    {
        float cost = 0;
        cost += _arrowHead switch
        {
            Arrowhead.Steel => 10,
            Arrowhead.Obsidian => 5,
            Arrowhead.Wood => 3
        };
        cost += _fletching switch
        {
            Fletching.Plastic => 10,
            Fletching.TurkeyFeathers => 5,
            Fletching.GooseFeathers => 3
        };
        cost += _shaft * 0.05f;
        return cost;
    }
}
public enum Arrowhead { Wood, Steel, Obsidian }
public enum Fletching { Plastic, TurkeyFeathers, GooseFeathers }

/* UPDATED AND BETTER
Console.WriteLine("What arrow do you want?");
Console.WriteLine("1 - Elite Arrow");
Console.WriteLine("2 - Beginner Arrow");
Console.WriteLine("3 - Marksman Arrow");
Console.WriteLine("4 - Custom Arrow");
string choice = Console.ReadLine();
Arrow arrow = choice switch
{
    "1" => Arrow.CreateEliteArrow(),
    "2" => Arrow.CreateBeginnerArrow(),
    "3" => Arrow.CreateMarksmanArrow(),
    _ => GetArrow(),
};
Console.WriteLine($"Arrow Cost for {arrow._fletching} {arrow._arrowHead} arrow that is {arrow._shaft} cm length: {arrow.GetCost()}");
Arrow GetArrow()
{
    Arrowhead arrowhead = GetArrowhead();
    Fletching fletching = GetFletching();
    float length = GetLength();
    return new Arrow(arrowhead, fletching, length);
}
Arrowhead GetArrowhead()
{
    Console.Write("Select Arrowhead (Steel, Wood, Obsidian): ");
    string input = Console.ReadLine();
    return input switch
    {
        "Steel" => Arrowhead.Steel,
        "Wood" => Arrowhead.Wood,
        "Obsidian" => Arrowhead.Obsidian
    };
}
Fletching GetFletching()
{
    Console.Write("Select Fletching (Plastic, Turkey Feathers, Goose Feathers): ");
    string input = Console.ReadLine();
    return input switch
    {
        "Plastic" => Fletching.Plastic,
        "Turkey Feathers" => Fletching.TurkeyFeathers,
        "Goose Feathers" => Fletching.GooseFeathers
    };
}
float GetLength()
{
    Console.Write("Type in Length between 60 and 100 cm: ");
    return Convert.ToSingle(Console.ReadLine());
}
class Arrow
{
    public Arrowhead _arrowHead { get; }
    public float _shaft { get; }
    public Fletching _fletching { get; }

    public Arrow(Arrowhead arrowhead, Fletching fletching, float shaft)
    {
        _arrowHead = arrowhead;
        _shaft = shaft;
        _fletching = fletching;
    }
    public float GetCost()
    {
        float cost = 0;
        cost += _arrowHead switch
        {
            Arrowhead.Steel => 10,
            Arrowhead.Obsidian => 5,
            Arrowhead.Wood => 3
        };
        cost += _fletching switch
        {
            Fletching.Plastic => 10,
            Fletching.TurkeyFeathers => 5,
            Fletching.GooseFeathers => 3
        };
        cost += _shaft * 0.05f;
        return cost;
    }
    public static Arrow CreateEliteArrow() => new Arrow(Arrowhead.Steel, Fletching.Plastic, 95);
    public static Arrow CreateBeginnerArrow() => new Arrow(Arrowhead.Wood, Fletching.GooseFeathers, 75);
    public static Arrow CreateMarksmanArrow() => new Arrow(Arrowhead.Steel, Fletching.GooseFeathers, 65);
}
public enum Arrowhead { Wood, Steel, Obsidian }
public enum Fletching { Plastic, TurkeyFeathers, GooseFeathers }*/