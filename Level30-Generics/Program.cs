ColoredItem<Sword> blueSword = new(new Sword(), ConsoleColor.Blue);
ColoredItem<Bow> redBow = new(new Bow(), ConsoleColor.Red);
ColoredItem<Axe> greenAxe = new(new Axe(), ConsoleColor.Green);

blueSword.Display();
redBow.Display();
greenAxe.Display();

internal class Sword { }
internal class Bow { }
internal class Axe { }
internal class ColoredItem<T> where T : notnull
{
    public T Item { get; }
    public ConsoleColor Color { get; }

    public ColoredItem(T item, ConsoleColor color)
    {
        Item = item;
        Color = color;
    }

    public void Display()
    {
        Console.ForegroundColor = Color;
        Console.WriteLine(Item.ToString());
        Console.ResetColor();
    }
}