Point p1 = new Point(2, 3);
Point p2 = new(-4, 0);
Console.WriteLine($"({p1.X}, {p1.Y})");
Console.WriteLine($"({p2.X}, {p2.Y})");
internal class Point
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public Point() { X = 0; Y = 0; }
    public Point(int x, int y) { X = x; Y = y; }
}