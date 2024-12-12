ArrowClass arrow = DescribeArrow();
Console.WriteLine("This will cost " + arrow.GetCost() + " gold.");
ArrowClass DescribeArrow()
{
    ArrowClass LocalArrow = new ArrowClass();

    Console.Write("Pick Arrow Head (Steel, Wood, Obsidian): ");
    string text = Console.ReadLine();
    LocalArrow.arrowHead = text switch
    {
        "Steel" => ArrowHead.Steel,
        "Wood" => ArrowHead.Wood,
        "Obsidian" => ArrowHead.Obsidian
    };

    Console.Write("Enter the length of the shaft (60 - 100 cm): ");
    byte length = Convert.ToByte(Console.ReadLine());
    LocalArrow.length = length;
    Console.Write("Pick a Fletching (Plastic, Turkey Feathers, Goose Feathers): ");
    text = Console.ReadLine();
    LocalArrow.flecthing = text switch
    {
        "Plastic" => Fletching.Plastic,
        "Turkey Feathers" => Fletching.TurkeyFeathers,
        "Goose Feathers" => Fletching.GooseFeathers
    };

    return LocalArrow;
}
class ArrowClass
{
    public ArrowHead arrowHead;
    public float length;
    public Fletching flecthing;

    public float GetCost()
    {
        float gold = 0;
        if (arrowHead == ArrowHead.Steel)
            gold += 10;
        else if (arrowHead == ArrowHead.Wood)
            gold += 3;
        else
            gold += 5;
        if (flecthing == Fletching.Plastic)
            gold += 10;
        else if (flecthing == Fletching.TurkeyFeathers)
            gold += 5;
        else
            gold += 3;
        gold += 0.05f * length;
        return gold;
    }
}
enum ArrowHead { Steel, Wood, Obsidian }
enum Fletching { Plastic, TurkeyFeathers, GooseFeathers }

/* SECOND ATTEMPT
Arrow order = new();
GetOrder(order);
float orderCost = order.GetCost();
Arrow GetOrder(Arrow order)
{
    Console.Write("Select Arrowhead (Steel, Wood, Obsidian): ");
    string textInput = Console.ReadLine();
    order._arrowHead = textInput switch
    {
        "Wood" => Arrowhead.Wood,
        "Steel" => Arrowhead.Steel,
        "Obsidian" => Arrowhead.Obsidian
    };
    Console.Write("Select Fletching (Plastic, Turkey Feathers, Goose Feathers): ");
    textInput = Console.ReadLine();
    order._fletching = textInput switch
    {
        "Plastic" => Fletching.Plastic,
        "Turkey Feathers" => Fletching.TurkeyFeathers,
        "Goose Feathers" => Fletching.GooseFeathers
    };
    Console.Write("Type in Length between 60 and 100 cm: ");
    order._shaft = Convert.ToSingle(Console.ReadLine());
    return order;
}

Console.WriteLine($"Arrow Cost: {orderCost}");
class Arrow
{
    public Arrowhead _arrowHead;
    public float _shaft;
    public Fletching _fletching;

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
enum Arrowhead { Wood, Steel, Obsidian }
enum Fletching { Plastic, TurkeyFeathers, GooseFeathers }*/