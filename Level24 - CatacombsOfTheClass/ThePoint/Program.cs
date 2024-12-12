Point point1 = new Point(Convert.ToInt32(Console.ReadLine()), Convert.ToInt32(Console.ReadLine()));
Point point2 = new Point(Convert.ToInt32(Console.ReadLine()), Convert.ToInt32(Console.ReadLine()));
Console.Write($"Point1({point1.X}, {point1.Y}); Point2({point2.X}, {point2.Y})");
class Point
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
    public Point() // or - :this(0, 0) { }
    {
        X = 0;
        Y = 0;
    }
}