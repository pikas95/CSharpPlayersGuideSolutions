Sword basic = new(MaterialType.Iron, default, 80, 5);

Sword epic = basic with { Gemstone = GemstoneType.Diamond };
Sword legendary = basic with { Material = MaterialType.Binarium, Gemstone = GemstoneType.Bitstone };

Console.WriteLine(basic);
Console.WriteLine(epic);
Console.WriteLine(legendary);

internal record Sword(MaterialType Material, GemstoneType Gemstone, double Length, double CrossGuardWidth);
internal enum MaterialType { Wood, Bronze, Iron, Steel, Binarium }
internal enum GemstoneType { None, Emerald, Amber, Sapphire, Diamond, Bitstone }