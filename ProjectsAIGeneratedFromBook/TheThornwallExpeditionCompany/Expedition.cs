internal class Expedition
{
    public string Name { get; }
    public string Destination { get; protected set; }
    public ExpeditionEvent[] Events { get; protected set; }
    public Difficulty Difficulty { get;}
    public int CompletedEvents { get; protected set; } = 0;
    public int RewardedGP { get; protected set; } = 0;
    
    public Expedition(string name, string destination, Difficulty difficulty, ExpeditionEvent[] events)
    {
        Name = name;
        Destination = destination;
        Difficulty = difficulty;
        Events = events;
    }

    public int GetCompletedEventsCount()
    {
        int completedEvents = 0;

        for (int i = 0; i < Events.Length; i++) 
            if (Events[i].EventCompleted())
                completedEvents++;

        return completedEvents;
    }

    public void UpdateAfterEvent(int eventIndex)
    {
        if (Events[eventIndex].EventCompleted())
        {
            CompletedEvents++;
            RewardedGP += Events[eventIndex].EventRewardGP;
        }
    }

    public override string ToString() => $"Expedition: {Name} | Destination: {Destination} | Difficulty: {Difficulty}";
}