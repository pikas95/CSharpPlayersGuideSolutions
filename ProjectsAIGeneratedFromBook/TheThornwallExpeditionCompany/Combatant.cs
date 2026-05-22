internal class Combatant : ICombatant
{
    public string Name { get; }
    public int Health { get; protected set; }
    public int MaxHealth { get; }
    protected int MinDamage { get; }
    protected int MaxDamage { get; }
    protected static Random Random { get; } = new Random();
    public Combatant(string name, int maxHealth, int minDamage, int maxDamage)
    {
        Name = name;
        Health = maxHealth;
        MaxHealth = maxHealth;
        MinDamage = minDamage;
        MaxDamage = maxDamage;
    }
    public virtual int Attack() => Random.Next(MinDamage, MaxDamage);

    public virtual void ReceiveDamage(int damage)
    {
        if (Health - damage < 0)
            Health = 0;
        else
            Health -= damage;
    }

    public virtual void Reset()
    {
        if (Health != 0)
            Health = MaxHealth;
    }
}