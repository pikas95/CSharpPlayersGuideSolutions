internal class TrapEvent : RoleTypeEvent
{
    protected int TrapDamage { get; }
    public TrapEvent(int trapDamage) : base("Trap", EventType.Trap, RoleType.Trapper, 35)
    {
        TrapDamage = trapDamage;
    }

    public void DealTrapDamage(Contractor?[] contractors)
    {
        for (int i = 0; i < contractors.Length; i++)
            if (contractors[i]?.Health > 0)
                contractors[i]!.ReceiveDamage(TrapDamage);
    }
}