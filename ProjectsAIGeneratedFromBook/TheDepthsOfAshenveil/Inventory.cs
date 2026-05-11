internal class Inventory
{
    public Weapon Weapon { get; private set; } = null!;

    public Inventory() { }
    public Inventory(Weapon weapon) => Weapon = weapon;

    public void TryAdd(Weapon weapon)
    {
        if (Weapon == null) Weapon = weapon;

    }
    public void Remove() => Weapon = null!;
    public Weapon Take()
    {
        Weapon weapon = Weapon!;
        Remove();
        return weapon;
    }
}