double tBase, tSize, tHeight;
Console.WriteLine("What's the triangle's base, size and height?");
tBase = Convert.ToDouble(Console.ReadLine());
tSize = Convert.ToDouble(Console.ReadLine());
tHeight = Convert.ToDouble(Console.ReadLine());
double tArea = tBase * tHeight / 2;
Console.WriteLine("Triangle's area: " + tArea);