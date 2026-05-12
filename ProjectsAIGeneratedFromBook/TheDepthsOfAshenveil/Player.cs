internal class Player
{
    public string Name { get; } = "Unknown";
    public int Health { get; private set; } = 20;
    public int Row { get; private set; } = 0;
    public int Col { get; private set; } = 0;
    public Inventory[] Inventory { get; private set; } = new Inventory[5];
    public Weapon EquipedWeapon { get; private set; }

    public Player(string name, Weapon weapon)
    {
        Name = name;
        EquipedWeapon = weapon;
    }

    public Inventory[] GetInventory() => Inventory[0..^1];

    public bool AddToInventory(Weapon weapon) // TODO: add remove from inventory method (refactor other objects that checks inventory)
    {
        for (int i = 0; i < Inventory.Length; i++)
        {
            if (Inventory[i]?.Weapon == null)
            {
                Inventory[i] = new Inventory(weapon);
                return true;
            }
        }

        return false;
    }

    public bool RemoveFromInventory(int index)
    {
        if (Inventory[index]?.Weapon == null)
            return false;

        Inventory[index] = null!;
        SortInventory();
        return true;
    }

    private void SortInventory()
    {
        for (int i = 0; i < Inventory.Length - 1; i++)
        {
            if (Inventory[i]?.Weapon == null && Inventory[i + 1]?.Weapon != null)
            {
                Inventory[i] = Inventory[i + 1];
                Inventory[i + 1] = null!;
            }
        }
    }

    public bool EquipWeapon(int invIndex)
    {
        if (Inventory[invIndex] == null)
            return false;
        else
        {
            Weapon temp = EquipedWeapon;
            EquipedWeapon = Inventory[invIndex].Take();
            Inventory[invIndex].TryAdd(temp);
        }

        return true;
    }

    public void Move(int row, int col)
    {
        Row += row;
        Col += col;
    }

    public int StabAttack()
    {
        return new Random().Next(EquipedWeapon.DamageMin + 2, EquipedWeapon.DamageMax - 2);
    }

    public int PowerAttack()
    {
        return new Random().Next(0, EquipedWeapon.DamageMax + 3);
    }

    public void ReceiveDamage(int damage) => Health -= damage;
}