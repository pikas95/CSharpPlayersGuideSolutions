internal class Player
{
    public string Name { get; }
    public int Health { get; private set; } = 20;
    public int Col { get; private set; } = 0;
    public int Row { get; private set; } = 0;
    public Inventory Inventory { get; private set; } 
    public Weapon EquippedWeapon { get; private set; }
    private static readonly Random _random = new Random();

    public Player(string name, Weapon weapon)
    {
        Name = name;
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

    public int StabAttack()
    {
        int damageAddon = (EquippedWeapon.DamageMax - EquippedWeapon.DamageMin) / 3;

        return _random.Next(EquippedWeapon.DamageMin + damageAddon, EquippedWeapon.DamageMax - damageAddon);
    }

    public int PowerAttack()
    {
        int damageAddon = 0;

        if (EquippedWeapon.DamageMax < 20)
            damageAddon = (EquippedWeapon.DamageMax - EquippedWeapon.DamageMin) / 3;

        return _random.Next(0, EquippedWeapon.DamageMax + damageAddon);
    }

    public void ReceiveDamage(int damage) => Health -= damage;
}