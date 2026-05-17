internal class PuzzleEvent : ExpeditionEvent
{
    protected bool IsPuzzleSolved { get; set; } = false;
    public int MaxSolveTries { get; } = 3;
    public int LeftTryCount { get; protected set; }
    public PuzzleEvent() : base("Puzzle", EventType.Puzzle, 75)
    {
        LeftTryCount = MaxSolveTries;
    }

    public override void StartEvent(Contractor[] contractors)
    {
        foreach (Contractor contractor in contractors)
            if (contractor?.Roles.Contains(RoleType.Researcher) == true && contractor.Health > 0 && LeftTryCount > 0)
            {
                TrySolving();

                if (IsPuzzleSolved)
                    return;
            }
    }

    private void TrySolving()
    {
        LeftTryCount--;

        if (Random.Next(0, 6) > 3)
            IsPuzzleSolved = true;
    }

    public override bool EventCompleted() => IsPuzzleSolved;
}
