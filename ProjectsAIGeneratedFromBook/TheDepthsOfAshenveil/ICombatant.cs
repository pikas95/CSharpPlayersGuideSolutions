internal interface ICombatant
{
    string Name { get; }
    int Health { get; }

    int Attack();
    void ReceiveDamage(int damage);
}