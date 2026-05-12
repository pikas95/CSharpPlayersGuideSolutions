internal class Room
{
    public string Name { get; }
    public string Description { get; private set; }
    public Enemy? Enemy { get; private set; } = null;
    public Weapon? Weapon { get; private set; } = null;

    public Room(Enemy enemy)
    {
        Enemy = enemy;
        Name = $"{enemy.Name} cave";
        Description = $"Filthy {Name}";
    }
    public Room(Weapon weapon)
    {
        Weapon = weapon;
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
        Weapon weapon = Weapon!;
        Weapon = null;
        Description += " (Claimed)";
        return weapon;
    }

    public void RoomUpdateEnemyDead()
    {
        if (Enemy?.Health <= 0)
            Description += " (Dead)";
    }
}