internal class TreasureEvent : ExpeditionEvent
{
    protected bool IsTreasureClaimed { get; set; } = false;
    public int MaxClaimTries { get; } = 3;
    public int LeftTryCount { get; protected set; }
    public TreasureEvent() : base("Treasure", EventType.Treasure, 150)
    {
        LeftTryCount = MaxClaimTries;
    }

    public override void StartEvent(Contractor[] contractors)
    {
        foreach (Contractor contractor in contractors)
            if (contractor?.Roles.Contains(RoleType.Researcher) == true && contractor.Health > 0 && LeftTryCount > 0)
            {
                TryStealing();

                if (IsTreasureClaimed)
                    return;
            }
    }

    private void TryStealing()
    {
        LeftTryCount--;

        if (Random.Next(0, 6) > 3)
            IsTreasureClaimed = true;
    }

    public override bool EventCompleted() => IsTreasureClaimed;
}