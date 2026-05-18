internal class Expedition
{
    public string Name { get; }
    public string Destination { get; protected set; }
    public ExpeditionEvent[] Events { get; protected set; }
    public Difficulty Difficulty { get; protected set; }
    
    public Expedition(string name, string destination, ExpeditionEvent[] events)
    {
        Name = name;
        Destination = destination;
        Events = events;
    }

    public override string ToString() => $"Expedition: {Name} | Destination: {Destination} | Difficulty: {Difficulty}";
}