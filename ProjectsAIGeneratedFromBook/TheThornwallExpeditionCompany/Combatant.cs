internal class Combatant : ICombatant
{
    public int Health { get; protected set; }
    public int MaxHealth { get; }
    protected int MinDamage { get; set; }
    protected int MaxDamage { get; set; }
    protected static Random Random { get; } = new Random();
    public Combatant(int maxHealth, int minDamage, int maxDamage)
    {
        Health = maxHealth;
        MaxHealth = maxHealth;
        MinDamage = minDamage;
        MaxDamage = maxDamage;
    }
    public virtual int Attack() => Random.Next(MinDamage, MaxDamage);

    public virtual void ReceiveDamage(int damage) => Health -= damage;
}