internal class TrapEvent : RoleTypeEvent
{
    protected int TrapDamage { get; }
    public TrapEvent(int trapDamage) : base("Trap", EventType.Trap, RoleType.Trapper, 35)
    {
        TrapDamage = trapDamage;
    }

    public override bool Try(Player player)
    {
        foreach (Contractor contractor in player.Contractors)
            if (contractor?.Roles.Contains(RoleRequired) == true && contractor.Health > 0 && LeftTryCount > 0)
            {
                TrySolving();
                break;
            }

        if (LeftTryCount == 0 && !IsSolved)
            DealTrapDamage(player.Contractors);

        if (LeftTryCount == 3)
            LeftTryCount = 0;

        return IsSolved;
    }

    public void DealTrapDamage(Contractor[] contractors)
    {
        for (int i = 0; i < contractors.Length; i++)
            if (contractors[i]?.Health > 0)
                contractors[i].ReceiveDamage(TrapDamage);
    }
}
