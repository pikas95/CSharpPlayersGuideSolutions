internal class Player : Combatant
{
    public int Col { get; private set; } = 0;
    public int Row { get; private set; } = 0;
    public Inventory Inventory { get; private set; } 
    public Weapon EquippedWeapon { get; private set; }

    public Player(string name, Weapon weapon) : base (name, 20, 1, 4) // Health = 20; DamageMin = 1, DamageMax = 4 (base player damage)
    {
        EquippedWeapon = weapon;
        Inventory = new Inventory(5);
    }

    public bool AddToInventory(Weapon weapon) => Inventory.TryAdd(weapon);

    public bool RemoveFromInventory(int index) => Inventory.Remove(index);

    public bool EquipWeapon(int weaponIndex)
    {
        if (Inventory.Weapons[weaponIndex] == null)
            return false;
        else
        {
            Weapon temp = EquippedWeapon;
            EquippedWeapon = Inventory.Take(weaponIndex);
            Inventory.TryAdd(temp);
        }

        return true;
    }

    public void Move(int col, int row)
    {
        Col += col;
        Row += row;
    }

    public override int Attack()
    {
        int damageAddon = ((DamageMax + EquippedWeapon.DamageMax) - (DamageMin + EquippedWeapon.DamageMin)) / 3;

        return Random.Next(DamageMin + EquippedWeapon.DamageMin + damageAddon, 
                            DamageMax + EquippedWeapon.DamageMax - damageAddon);
    }

    public int PowerAttack()
    {
        int damageAddon = 0;

        if (DamageMax + EquippedWeapon.DamageMax < 20)
            damageAddon = ((DamageMax + EquippedWeapon.DamageMax) - (DamageMin + EquippedWeapon.DamageMin)) / 3;

        return Random.Next(0, DamageMax + EquippedWeapon.DamageMax + damageAddon);
    }
}