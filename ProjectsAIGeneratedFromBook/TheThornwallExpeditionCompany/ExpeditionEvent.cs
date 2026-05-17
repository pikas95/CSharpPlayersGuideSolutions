internal abstract class ExpeditionEvent
{
    public string Name { get; }
    public EventType EventType { get; }
    protected int EventRewardGP { get; }
    protected static Random Random { get; } = new Random();
    public ExpeditionEvent(string name, EventType eventType, int eventRewardGP)
    {
        Name = name;
        EventType = eventType;
        EventRewardGP = eventRewardGP;
    }
    public abstract void StartEvent(Contractor[] contractors);

    public virtual int GrantEventReward() => EventCompleted() ? EventRewardGP : 0;
    public abstract bool EventCompleted();

    public override string ToString() => $"Name: {Name} | Type: {EventType} | Reward: {EventRewardGP}";
}