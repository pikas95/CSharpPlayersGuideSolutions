internal class RoleTypeEvent : ExpeditionEvent
{
    public RoleType RoleRequired { get; }
    protected bool IsSolved { get; set; }
    public int MaxTries { get; } = 3;
    public int LeftTryCount { get; protected set; }
    public RoleTypeEvent(string name, EventType eventType, RoleType roleType, int eventRewardGP) 
        : base(name, eventType, eventRewardGP)
    {
        RoleRequired = roleType;
        LeftTryCount = MaxTries;
    }

    public virtual bool Try(Contractor contractor)
    {
        if (contractor?.Roles.Contains(RoleRequired) == true && contractor.Health > 0 && LeftTryCount > 0)
            TrySolving();

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