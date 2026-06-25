internal class Inventory
{
    public List<Weapon> Weapons { get; private set; } = new List<Weapon>();
    protected int InventorySize { get; }
    public Inventory(int inventorySize) => InventorySize = inventorySize;

    public bool TryAdd(Weapon weapon)
    {
        if (Full())
            return false;

        Weapons.Add(weapon);
        return true;
    }

    public bool TryAdd(Weapon weapon, int index)
    {
        if (Full())
            return false;
        
        Weapons.Insert(index, weapon);
        return true;
    }

    public bool Remove(int index) => Weapons.Remove(Weapons[index]);

    public Weapon Take(int index)
    {
        Weapon weapon = Weapons[index];
        Remove(index);
        return weapon;
    }

    public bool Full() => Weapons.Count == InventorySize; 
}