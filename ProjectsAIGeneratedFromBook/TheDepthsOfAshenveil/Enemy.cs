internal class Enemy
{
    public string Name { get; }
    public int Health { get; private set; }
    private int _damageMin;
    private int _damageMax;
    private static readonly Random _random = new Random();

    public Enemy(string name, int health, int damageMin, int damageMax)
    {
        Name = name;
        Health = health;
        _damageMin = damageMin;
        _damageMax = damageMax;
    }

    public int Attack() => _random.Next(_damageMin, _damageMax);

    public void ReceiveDamage(int damage) => Health -= damage;
}