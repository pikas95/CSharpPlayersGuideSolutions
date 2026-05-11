internal class Player
{
    public string Name { get; } = "Unknown";
    public int Health { get; private set; } = 20;
    public int Row { get; private set; } = 0;
    public int Col { get; private set; } = 0;
    public bool IsAlive { get; private set; } = true;
    public Inventory[] Inventory { get; private set; } = new Inventory[5];
    public Weapon EquipedWeapon { get; private set; }

    public Player(string name, Weapon weapon)
    {
        Name = name;
        EquipedWeapon = weapon;
    }

    public Inventory[] GetInventory() => Inventory[0..^1];

    public bool AddToInventory(Weapon weapon)
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
}