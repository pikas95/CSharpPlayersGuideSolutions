internal class AssaultEvent : ExpeditionEvent
{
    public Enemy[] Enemies { get; }
    public AssaultEvent(Enemy[] enemies) : base("Assault", EventType.Assault, 50) 
    { Enemies = enemies; }
    public AssaultEvent(string name, int eventRewardGP, Enemy[] enemies) : base(name, EventType.Assault, eventRewardGP)
    { Enemies = enemies; }

    public void EnemyAttackTurn(Player player)
    {
        foreach (Enemy enemy in Enemies)
            if (enemy.Health > 0)
                player.Contractors[Random.Next(0, player.Contractors.Count)]!.ReceiveDamage(enemy.Attack());
    }

    public override bool EventCompleted()
    {
        foreach (Enemy enemy in Enemies)
            if (enemy.Health > 0)
                return false;

        return true;
    }
}