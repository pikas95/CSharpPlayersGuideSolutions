internal interface IHealer
{
    public string Name { get; }
    public int HealCooldown { get; }
    bool HealTarget(Contractor contractor);
    bool HealAll(Contractor[] contractors);
    bool HealSelf();
    void DecrementCooldown();
    void Reset();
}