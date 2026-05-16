internal class Scout : Contractor, IMedic
{
    protected const int _defaultHealth = 85;
    protected const int _defaultDailyRate = 50;
    protected const int _defaultMinDamage = 7;
    protected const int _defaultMaxDamage = 22;
    protected int MinHeal { get; set; }
    protected int MaxHeal { get; set; }
    public int HealCooldown { get; protected set; }

    public Scout() : base("Scout", _defaultHealth, _defaultDailyRate, _defaultMinDamage, _defaultMaxDamage)
    {
        MinHeal = DeclareMinHeal();
        MaxHeal = DeclareMaxHeal();
        Roles = [RoleType.Fighter, RoleType.Healer];
    }

    public Scout(string name, int maxHealth, int dailyRate, RoleType[] roles)
        : base(name, maxHealth, dailyRate, _defaultMinDamage, _defaultMaxDamage)
    {
        MinHeal = DeclareMinHeal();
        MaxHeal = DeclareMaxHeal();
        Roles = roles;
    }

    public Scout(string name, int maxHealth, int dailyRate, int minHeal, int maxHeal, RoleType[] roles)
        : base(name, maxHealth, dailyRate, _defaultMinDamage, _defaultMaxDamage)
    {
        MinHeal = minHeal;
        MaxHeal = maxHeal;
        Roles = roles;
    }

    public Scout(string name, int maxHealth, int dailyRate, int minHeal, int maxHeal, int minDamage, int maxDamage, RoleType[] roles)
        : base(name, maxHealth, dailyRate, minDamage, maxDamage)
    {
        MinHeal = minHeal;
        MaxHeal = maxHeal;
        Roles = roles;
    }

    protected int DeclareMinHeal() => MaxHealth / 8;

    protected int DeclareMaxHeal() => MaxHealth / 4;

    protected int Heal() => Random.Next(MinHeal, MaxHeal);

    public bool HealTarget(Contractor contractor)
    {
        if (HealCooldown == 0)
        {
            contractor.ApplyHealing(Heal());
            HealCooldown = 2;
            return true;
        }
        return false;
    }

    public bool HealAll(Contractor[] contractors)
    {
        if (HealCooldown == 0)
        {
            int healing = Heal();

            for (int i = 0; i < contractors.Length; i++)
                contractors[i].ApplyHealing(healing / 2);

            HealCooldown = 3;

            return true;
        }
        return false;
    }

    public bool HealSelf()
    {
        if (HealCooldown == 0)
        {
            Health = MaxHealth;
            HealCooldown = 1;
            return true;
        }
        return false;
    }

    public void DecrementCooldown() => HealCooldown--;

    public override string ToString() => $"{base.ToString()} | Heal Rate: {MinHeal}-{MaxHeal}";
}