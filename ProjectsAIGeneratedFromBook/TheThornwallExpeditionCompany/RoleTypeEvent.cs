internal class RoleTypeEvent : ExpeditionEvent
{
    protected RoleType RoleRequired { get; }
    protected bool IsSolved { get; set; } = false;
    public int MaxTries { get; } = 3;
    public int LeftTryCount { get; protected set; }
    public RoleTypeEvent(string name, EventType eventType, RoleType roleType, int eventRewardGP) 
        : base(name, eventType, eventRewardGP)
    {
        RoleRequired = roleType;
        LeftTryCount = MaxTries;
    }

    public virtual bool Try(Player player)
    {
        foreach (Contractor contractor in player.Contractors)
            if (contractor?.Roles.Contains(RoleRequired) == true && contractor.Health > 0 && LeftTryCount > 0)
            {
                TrySolving();
                break;
            }

        if (LeftTryCount == 3)
            LeftTryCount = 0;

        return IsSolved;
    }

    protected void TrySolving()
    {
        LeftTryCount--;

        if (Random.Next(0, 6) > 3)
            IsSolved = true;
    }

    public override bool EventCompleted() => IsSolved;
}