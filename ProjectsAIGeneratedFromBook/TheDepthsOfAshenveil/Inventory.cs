internal class Inventory
{
    public Weapon?[] Weapons { get; private set; }
    public Inventory(int inventorySize) => Weapons = new Weapon[inventorySize];

    public bool TryAdd(Weapon weapon)
    {
        for (int i = 0; i < Weapons.Length; i++)
        {
            if (Weapons[i] == null)
            {
                Weapons[i] = weapon;
                return true;
            }
        }
        return false;
    }
    public bool Remove(int index)
    {
        if (Weapons[index] == null)
            return false;

        Weapons[index] = null;
        SortInventory();
        return true;
    }
    public Weapon Take(int index)
    {
        Weapon weapon = Weapons[index]!;
        Remove(index);
        return weapon;
    }

    private void SortInventory()
    {
        for (int i = 0; i < Weapons.Length; i++)
        {
            if (Weapons[i] == null)
            {
                for (int j = i + 1; j < Weapons.Length; j++)
                {
                    if (Weapons[j] != null)
                    {
                        Weapons[i] = Weapons[j];
                        Weapons[j] = null;
                        break;
                    }
                }
            }
        }
    }

    public bool Full() => Weapons[^1] != null; // SortInventory() runs anytime a weapon is removed from Weapons[]
}