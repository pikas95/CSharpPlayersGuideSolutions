internal class Contractor : Combatant
{
    public RoleType[] Roles { get; protected init; } = new RoleType[1]; // default RoleType.Commoner
    public int DailyRate { get; }

    public Contractor(string name, int maxHealth, int dailyRate, int minDamage, int maxDamage)
        : base(name, maxHealth, minDamage, maxDamage)
    {
        DailyRate = dailyRate;
    }

    public virtual void ApplyHealing(int healing)
    {
        if (Health + healing > MaxHealth)
            Health = MaxHealth;
        else
            Health += healing;
    }

    public override string ToString()
    {
        return $"{Name} | Daily Rate: {DailyRate} gp | Roles: {RoleList()} | {Health}/{MaxHealth} HP | Damage {MinDamage}-{MaxDamage}";
    }

    public string RoleList()
    {
        string list = null!;

        for (int i = 0; i < Roles.Length; i++)
        {
            list += Roles[i].ToString();
            if (i + 1 != Roles.Length)
                list += ", ";
        }

        return list;
    }
}