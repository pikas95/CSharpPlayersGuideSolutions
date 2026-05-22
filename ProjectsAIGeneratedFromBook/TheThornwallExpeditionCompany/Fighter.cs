internal class Fighter : Contractor
{
    protected const int _defaultHealth = 120;
    protected const int _defaultDailyRate = 25;
    protected const int _defaultMinDamage = 10;
    protected const int _defaultMaxDamage = 30;

    public Fighter() : base("Fighter", _defaultHealth, _defaultDailyRate, _defaultMinDamage, _defaultMaxDamage)
    {
        Roles = [RoleType.Fighter];
    }

    public Fighter(string name, int maxHealth, int dailyRate, RoleType[] roles)
        : base(name, maxHealth, dailyRate, _defaultMinDamage, _defaultMaxDamage)
    {
        Roles = roles;
    }

    public Fighter(string name, int maxHealth, int dailyRate, int minDamage, int maxDamage, RoleType[] roles) 
        : base(name, maxHealth, dailyRate, minDamage, maxDamage)
    {
        Roles = roles;
    }
}