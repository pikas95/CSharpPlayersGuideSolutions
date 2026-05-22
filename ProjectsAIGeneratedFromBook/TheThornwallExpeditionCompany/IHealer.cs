internal interface IHealer
{
    public string Name { get; }
    public int HealCooldown { get; }
    void HealTarget(Contractor contractor);
    void HealAll(Contractor?[] contractors);
    void HealSelf();
    void DecrementCooldown();
    void Reset();
}