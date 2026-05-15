internal class Combatant : ICombatant
{
    public string Name { get; }
    public int Health { get; protected set; }
    protected int DamageMin { get; }
    protected int DamageMax { get; }
    protected static readonly Random Random = new Random();
    public Combatant(string name, int health, int damageMin, int damageMax)
    {
        Name = name;
        Health = health;
        DamageMin = damageMin;
        DamageMax = damageMax;
    }
    public virtual int Attack() => Random.Next(DamageMin, DamageMax);
    public void ReceiveDamage(int damage) => Health -= damage;
}