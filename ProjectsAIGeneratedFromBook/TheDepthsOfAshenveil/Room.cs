internal class Room
{
    public string Name { get; }
    public string Description { get; private set; }
    public Enemy? _enemy = null;
    public Weapon? _weapon = null;

    public Room(Enemy enemy)
    {
        _enemy = enemy;
        Name = $"{enemy.Name} cave";
        Description = $"Filthy {Name}";
    }
    public Room(Weapon weapon)
    {
        _weapon = weapon;
        Name = $"{weapon.Name} tomb";
        Description = $"Forgotten tomb of {weapon.Name} treasure";
    }
    public Room(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public Weapon TakeWeapon()
    {
        Weapon weapon = _weapon!;
        _weapon = null;
        Description += " (Cleared)";
        return weapon;
    }
}