internal class Contractor : Combatant
{
    public RoleType[] Roles { get; protected set; } = new RoleType[1]; // default RoleType.Commoner
    public string Name { get; }
    public int DailyRate { get; }

    public Contractor(string name, int maxHealth, int dailyRate, int minDamage, int maxDamage)
        : base(maxHealth, minDamage, maxDamage)
    {
        Name = name;
        DailyRate = dailyRate;
    }

    public virtual void ApplyHealing(int healing) => Health += healing;

    public override string ToString()
    {
        return $"{Name} | {Health}/{MaxHealth} HP | Daily Rate: {DailyRate} gp | Damage {MinDamage}-{MaxDamage}";
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
