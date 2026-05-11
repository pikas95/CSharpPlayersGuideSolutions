internal class Enemy
{
    public string Name { get; }
    public int Health { get; private set; }
    private int _damageMin;
    private int _damageMax;
    public bool IsAlive { get; private set; } = true;

    public Enemy(string name, int health, int damageMin, int damageMax)
    {
        Name = name;
        Health = health;
        _damageMin = damageMin;
        _damageMax = damageMax;
    }
}