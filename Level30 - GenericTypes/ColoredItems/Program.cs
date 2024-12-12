ColoredItem<Sword> blueSword = new(new Sword(), ConsoleColor.Blue);
ColoredItem<Bow> redBow = new(new Bow(), ConsoleColor.Red);
ColoredItem<Axe> greenAxe = new(new Axe(), ConsoleColor.Green);
blueSword.Display();
redBow.Display();
greenAxe.Display();
public class Sword { }
public class Bow { }
public class Axe { }
public class ColoredItem<T>
{
    private T Item { get; }
    private ConsoleColor Color { get; }
    public ColoredItem(T item, ConsoleColor color)
    {
        Item = item;
        Color = color;
    }
    public void Display()
    {
        Console.BackgroundColor = Color;
        Console.WriteLine(Item.ToString());
        Console.BackgroundColor = ConsoleColor.Black;
    }
}