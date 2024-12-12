Sword original = new(Material.Iron, Gemstone.None, 7.5f, 1.25f);
Sword sword1 = original with { material = Material.Binarium, gemstone = Gemstone.Diamond, length = 5, crossguardWidth = 1 };
Sword sword2 = sword1 with { gemstone = Gemstone.Bitstone, length = 11.4f, crossguardWidth = 2 };
Console.WriteLine(original);
Console.WriteLine(sword1);
Console.WriteLine(sword2);
public record Sword(Material material, Gemstone gemstone, float length, float crossguardWidth);
public enum Material { Wood, Bronze, Iron, Steel, Binarium }
public enum Gemstone { None, Emerald, Amber, Sapphire, Diamond, Bitstone }