internal interface IMedic
{
    bool HealTarget(Contractor contractor);
    bool HealAll(Contractor[] contractors);
    bool HealSelf();
    void DecrementCooldown();
}