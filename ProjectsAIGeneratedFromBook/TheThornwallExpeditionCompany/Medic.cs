internal class Medic : Contractor, IHealer
{
    protected const int _defaultHealth = 50;
    protected const int _defaultDailyRate = 35;
    protected const int _defaultMinDamage = 1;
    protected const int _defaultMaxDamage = 8;
    protected int MinHeal { get; }
    protected int MaxHeal { get; }
    public int HealCooldown { get; protected set; }

    public Medic() : base("Medic", _defaultHealth, _defaultDailyRate, _defaultMinDamage, _defaultMaxDamage)
    {
        MinHeal = DeclareMinHeal();
        MaxHeal = DeclareMaxHeal();
        Roles = [RoleType.Healer];
    }

    public Medic(string name, int maxHealth, int dailyRate, RoleType[] roles)
        : base(name, maxHealth, dailyRate, _defaultMinDamage, _defaultMaxDamage)
    {
        MinHeal = DeclareMinHeal();
        MaxHeal = DeclareMaxHeal();
        Roles = roles;
    }
    
    public Medic(string name, int maxHealth, int dailyRate, int minDamage, int maxDamage, RoleType[] roles) 
        : base(name, maxHealth, dailyRate, minDamage, maxDamage)
    {
        MinHeal = DeclareMinHeal();
        MaxHeal = DeclareMaxHeal();
        Roles = roles;
    }

    public Medic(string name, int maxHealth, int dailyRate, int minDamage, int maxDamage, RoleType[] roles, int minHeal, int maxHeal)
        : base(name, maxHealth, dailyRate, minDamage, maxDamage)
    {
        MinHeal = minHeal;
        MaxHeal = maxHeal;
        Roles = roles;
    }

    protected int DeclareMinHeal() => MaxHealth / 4;

    protected int DeclareMaxHeal() => MaxHealth / 2;

    protected int Heal() => Random.Next(MinHeal, MaxHeal);

    public void HealTarget(Contractor contractor)
    {
        if (HealCooldown == 0)
        {
            contractor.ApplyHealing(Heal());
            HealCooldown = 2;
        }
    }

    public void HealAll(List<Contractor> contractors)
    {
        if (HealCooldown == 0)
        {
            int healing = Heal();

            for (int i = 0; i < contractors.Count; i++)
                contractors[i].ApplyHealing(healing / 2);

            HealCooldown = 3;
        }
    }

    public void HealSelf()
    {
        if (HealCooldown == 0)
        {
            Health = MaxHealth;
            HealCooldown = 1;
        }
    }

    public void DecrementCooldown()
    {
        if (HealCooldown > 0)
            HealCooldown--;
    }

    public override void Reset()
    {
        base.Reset();
        HealCooldown = 0;
    }

    public override string ToString() => $"{base.ToString()} | Heal Rate: {MinHeal}-{MaxHeal} | Heal Cooldown: {(HealCooldown > 0 ? HealCooldown + " Turns" : "None")}";
}