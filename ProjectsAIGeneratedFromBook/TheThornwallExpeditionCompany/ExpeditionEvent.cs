internal abstract class ExpeditionEvent
{
    public string Name { get; }
    public EventType EventType { get; }
    public int EventRewardGP { get; }
    protected static Random Random { get; } = new Random();
    public ExpeditionEvent(string name, EventType eventType, int eventRewardGP)
    {
        Name = name;
        EventType = eventType;
        EventRewardGP = eventRewardGP;
    }
    public abstract bool EventCompleted();
    public override string ToString() => $"{Name} | Reward: {EventRewardGP}";
}