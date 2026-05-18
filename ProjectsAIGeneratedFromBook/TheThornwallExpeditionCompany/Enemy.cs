internal class Enemy : Combatant
{
    public Enemy(string name, int maxHealth, int minDamage, int maxDamage)
        : base(name, maxHealth, minDamage, maxDamage)
    { }

    public override string ToString()
    {
        return $"{Name} | {Health}/{MaxHealth} HP";
    }
}
internal class Goblin : Enemy
{
    public Goblin() : base("Goblin", 20, 2, 10) { }
}
internal class Wolf : Enemy
{
    public Wolf() : base("Wolf", 30, 5, 15) { }
}
internal class Thief : Enemy
{
    public Thief() : base("Thief", 35, 10, 25) { }
}
internal class Wizard : Enemy
{
    // make a whole group attack
    public Wizard() : base("Crazy Wizard", 70, 15, 35) { }
}