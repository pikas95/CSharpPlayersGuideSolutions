Color random = new Color(Convert.ToByte(Console.ReadLine()), Convert.ToByte(Console.ReadLine()), Convert.ToByte(Console.ReadLine()));
Color fixedColor = Color.Purple;
Console.WriteLine($"Random - R: {random.R}, G: {random.G}, B: {random.B}");
Console.WriteLine($"Fixed - R: {fixedColor.R}, G: {fixedColor.G}, B: {fixedColor.B}");
class Color
{
    public byte R { get; }
    public byte G { get; }
    public byte B { get; }
    public Color(byte Red, byte Green, byte Blue)
    {
        R = Red;
        G = Green;
        B = Blue;
    }
    public static Color White { get; } = new Color(255, 255, 255);
    public static Color Black { get; } = new Color(0, 0, 0);
    public static Color Red { get; } = new Color(255, 0, 0);
    public static Color Orange { get; } = new Color(255, 165, 0);
    public static Color Yellow { get; } = new Color(255, 255, 0);
    public static Color Green { get; } = new Color(0, 128, 0);
    public static Color Blue { get; } = new Color(0, 0, 255);
    public static Color Purple { get; } = new Color(128, 0, 128);
}