Arrow arrow = new Arrow(GetArrowHead(), GetShaft(), GetFletching());
Console.WriteLine($"That arrow cost: {arrow._cost} gold.");
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

int GetShaft()
{
    Console.Write("Choose shaft length (between 60 and 100): ");
    while (true)
    {
        int input = Convert.ToInt32(Console.ReadLine());
        if (input >= 60 && input <= 100) return input;
        Console.WriteLine("We don't make shafts that size.");
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

class Arrow
{
    public ArrowHead _arrowHead;
    public int _shaft;
    public Fletching _fletching;
    public float _cost;

    public Arrow() :this(ArrowHead.Wood, 60, Fletching.GooseFeathers) // cheapest, if not picked by player
    { }

    public Arrow(ArrowHead arrowHead, int shaft, Fletching fletching)
    {
        _arrowHead = arrowHead;
        _shaft = shaft;
        _fletching = fletching;
        _cost = GetCost();
    }

    public float GetCost()
    {
        float cost = _arrowHead switch
        {
            ArrowHead.Steel => 10,
            ArrowHead.Wood => 3,
            ArrowHead.Obsidian => 5
        };

        cost += _shaft * 0.05f;

        cost += _fletching switch
        {
            Fletching.Plastic => 10,
            Fletching.TurkeyFeathers => 5,
            Fletching.GooseFeathers => 3
        };
        return cost;
    }
}

enum ArrowHead { Steel, Wood, Obsidian }
enum Fletching { Plastic, TurkeyFeathers, GooseFeathers }