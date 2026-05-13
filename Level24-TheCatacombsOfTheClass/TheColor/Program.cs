Color random = new Color(189, 92, 22);
Color blue = Color.Blue;
Console.WriteLine($"{random.R}, {random.G}, {random.B}");
Console.WriteLine($"{blue.R}, {blue.G}, {blue.B}");
internal class Color
{
    public byte R { get; }
    public byte G { get; }
    public byte B { get; }
    public static Color White { get; } = new Color(255, 255, 255);
    public static Color Black { get; } = new Color(0, 0, 0);
    public static Color Red { get; } = new Color(255, 0, 0);
    public static Color Orange { get; } = new Color(255, 165, 0);
    public static Color Yellow { get; } = new Color(255, 255, 0);
    public static Color Green { get; } = new Color(0, 128, 0);
    public static Color Blue { get; } = new Color(0, 0, 255);
    public static Color Purple { get; } = new Color(128, 0, 128);
    public Color() { R = 0; G = 0; B = 0; }
    public Color (byte r, byte g, byte b) { R = r; G = g; B = b; }
}