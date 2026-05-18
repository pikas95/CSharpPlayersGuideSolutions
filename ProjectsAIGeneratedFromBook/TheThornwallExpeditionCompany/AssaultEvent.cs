internal class AssaultEvent : ExpeditionEvent
{
    public Enemy[] Enemies { get; }
    public AssaultEvent(Enemy[] enemies) : base("Assault", EventType.Assault, 50) 
    { Enemies = enemies; }
    public AssaultEvent(string name, int eventRewardGP, Enemy[] enemies) : base(name, EventType.Assault, eventRewardGP)
    { Enemies = enemies; }

    /*public virtual bool StartEvent(Expedition expedition)
    {
        Fighter[] fighters = expedition.GetFighters();

        while (AnyCombatantAlive(fighters) && !EventCompleted())
        {
            OneSideAttack(fighters, Enemies);

            if (!EventCompleted())
                OneSideAttack(Enemies, expedition.Contractors);
        }

        return EventCompleted();
    }

    private static void OneSideAttack(Combatant[] attacker, Combatant[] targets)
    {
        for (int i = 0; i < attacker.Length; i++)
            targets[Random.Next(0, targets.Length)]?.ReceiveDamage(attacker[i].Attack());
    }

    private static bool AnyCombatantAlive(Combatant[] combatants)
    {
        foreach (Combatant combatant in combatants)
            if (combatant.Health > 0)
                return true;
        return false;
    }*/

    public void EnemyAttackTurn(Player player)
    {
        for (int i = 0; i < Enemies.Length; i++)
            if (Enemies[i].Health > 0)
                player.Contractors[Random.Next(0, player.ContractorCount)].ReceiveDamage(Enemies[i].Attack());
    }

    public override bool EventCompleted()
    {
        foreach (Enemy enemy in Enemies)
            if (enemy.Health > 0)
                return false;

        return true;
    }
}
