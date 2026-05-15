internal class Room
{
    public string Name { get; }
    public string Description { get; protected set; }
    public ConsoleColor DisplayColor { get; protected set; } = ConsoleColor.Gray;

    public Room(string name, string description)
    {
        Name = name;
        Description = description;
    }
}
internal class EnemyRoom : Room
{
    public Enemy Enemy { get; private set; }

    public EnemyRoom(Enemy enemy) : base ($"{enemy.Name} cave", $"Filthy {enemy.Name} cave")
    {
        Enemy = enemy;
        DisplayColor = ConsoleColor.Red;
    }
    public void MarkEnemyDead() 
    {
        Description += " (Dead)";
        DisplayColor = ConsoleColor.Gray;
    } 
}
internal class WeaponRoom : Room
{
    public Weapon? Weapon { get; private set; }
    public WeaponRoom(Weapon weapon) : base ($"{weapon.Name} tomb", $"Forgotten tomb of {weapon.Name} treasure")
    {
        Weapon = weapon;
        DisplayColor = ConsoleColor.Green;
    }
    public Weapon TakeWeapon()
    {
        Weapon weapon = Weapon!;
        Weapon = null;
        Description += " (Claimed)";
        DisplayColor = ConsoleColor.Gray;
        return weapon;
    }
}
internal class EmptyRoom : Room
{
    public EmptyRoom(string name, string description) : base(name, description) { DisplayColor = ConsoleColor.Gray; }
}