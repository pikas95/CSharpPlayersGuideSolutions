internal class Player
{
    public string Name { get; } = "Unknown";
    public int Health { get; private set; } = 20;
    public int Col { get; private set; } = 0;
    public int Row { get; private set; } = 0;
    public Inventory Inventory { get; private set; } 
    public Weapon EquipedWeapon { get; private set; }
    private static readonly Random _random = new Random();

    public Player(string name, Weapon weapon)
    {
        Name = name;
        EquipedWeapon = weapon;
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
            Weapon temp = EquipedWeapon;
            EquipedWeapon = Inventory.Take(weaponIndex);
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
        int damageAddon = (EquipedWeapon.DamageMax - EquipedWeapon.DamageMin) / 3;

        return _random.Next(EquipedWeapon.DamageMin + damageAddon, EquipedWeapon.DamageMax - damageAddon);
    }

    public int PowerAttack()
    {
        int damageAddon = 0;

        if (EquipedWeapon.DamageMax < 20)
            damageAddon = (EquipedWeapon.DamageMax - EquipedWeapon.DamageMin) / 3;

        return _random.Next(0, EquipedWeapon.DamageMax + damageAddon);
    }

    public void ReceiveDamage(int damage) => Health -= damage;
}