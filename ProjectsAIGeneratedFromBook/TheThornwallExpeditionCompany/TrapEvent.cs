internal class TrapEvent : ExpeditionEvent
{
    protected bool IsTrapActive { get; set; } = true;
    public int MaxDisarmTries { get; } = 3;
    public int LeftTryCount { get; protected set; }
    protected int TrapDamage { get; }
    public TrapEvent(int trapDamage) : base("Trap", EventType.Trap, 25) 
    { 
        TrapDamage = trapDamage;
        LeftTryCount = MaxDisarmTries;
    }

    public override void StartEvent(Contractor[] contractors)
    {
        foreach (Contractor contractor in contractors)
            if (contractor.Roles.Contains(RoleType.Trapper) && contractor.Health > 0 && LeftTryCount > 0)
            {
                TryDisarming();

                if (!IsTrapActive)
                    return;
            }
    }

    private void TryDisarming()
    {
        LeftTryCount--;

        if (Random.Next(0, 6) > 3)
            IsTrapActive = false;
    }

    public override bool EventCompleted() => !IsTrapActive;
}
