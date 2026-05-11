internal class Weapon
{
    public string Name { get; }
    public int DamageMin { get; }
    public int DamageMax { get; }

    public Weapon(string name, int damageMin, int damageMax)
    {
        Name = name;
        DamageMin = damageMin;
        DamageMax = damageMax;
    }
}