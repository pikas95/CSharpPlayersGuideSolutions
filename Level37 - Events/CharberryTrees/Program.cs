CharberryTree tree = new CharberryTree();
Notifier notifier = new Notifier(tree);
Harvester harvester = new Harvester(tree);
while (true)
    tree.MaybeGrow();
public class CharberryTree
{
    public event Action? Ripened;
    private Random _random = new Random();
    public bool Ripe { get; set; }
    public void MaybeGrow()
    {
        // Only a tiny chance of ripening each time, but we try a lot!
        if (_random.NextDouble() < 0.00000001 && !Ripe)
        {
            Ripe = true;
            Ripened?.Invoke();
        }
    }
}
public class Notifier
{
    public Notifier(CharberryTree tree) { tree.Ripened += OnRipened; }
    private void OnRipened() { Console.WriteLine("A charberry fruit has ripened!"); }
}
public class Harvester
{
    private int _harvestedCount;
    private CharberryTree _tree;
    public Harvester(CharberryTree tree) { _tree = tree; _tree.Ripened += OnRipened; }
    private void OnRipened() 
    { 
        _harvestedCount++;  
        _tree.Ripe = false;
        Console.WriteLine($"Tree was harvested {_harvestedCount} {(_harvestedCount != 1 ? "times" : "time")}.");
    }
}