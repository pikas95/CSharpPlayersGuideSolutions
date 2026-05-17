internal class AssaultEvent : ExpeditionEvent
{
    public Enemy[] Enemies { get; }
    public AssaultEvent(Enemy[] enemies) : base("Assault", EventType.Assault, 50) 
    { Enemies = enemies; }
    public AssaultEvent(string name, int eventRewardGP, Enemy[] enemies) : base(name, EventType.Assault, eventRewardGP)
    { Enemies = enemies; }

    public override void StartEvent(Contractor[] contractors)
    {
        Fighter[] fighters = GetFighters(contractors);

        while (AnyCombatantAlive(fighters) && !EventCompleted())
        {
            OneSideAttack(fighters, Enemies);

            if (!EventCompleted())
                OneSideAttack(Enemies, contractors);
        }
    }

    private Fighter[] GetFighters(Contractor[] contractors)
    {
        int fighterCount = 0;

        foreach (Contractor contractor in contractors)
            if (contractor is Fighter fighter)
                fighterCount++;

        Fighter[] fighters = new Fighter[fighterCount];
        fighterCount = 0;

        foreach (Contractor contractor in contractors)
            if (contractor is Fighter fighter)
                fighters[fighterCount++] = fighter;

        return fighters;
    }

    /*private Contractor[] GetContractorsWiithoutNull(Contractor[] contractorsWithNull)
    {
        int contractorCount = 0;

        foreach (Contractor contractor in contractorsWithNull)
            if (contractor != null)
                contractorCount++;

        Contractor[] contractors = new Contractor[contractorCount];
        fighterCount = 0;

        foreach (Contractor contractor in contractors)
            if (contractor is Fighter fighter)
                fighters[fighterCount++] = fighter;

        return fighters;
    }*/

    private void OneSideAttack(Combatant[] attacker, Combatant[] targets)
    {
        for (int i = 0; i < attacker.Length; i++)
            targets[Random.Next(0, targets.Length)]?.ReceiveDamage(attacker[i].Attack());
    }

    private bool AnyCombatantAlive(Combatant[] combatants)
    {
        foreach (Combatant combatant in combatants)
            if (combatant.Health > 0)
                return true;
        return false;
    }

    public override bool EventCompleted()
    {
        foreach (Enemy enemy in Enemies)
            if (enemy.Health > 0)
                return false;

        return true;
    }
}
