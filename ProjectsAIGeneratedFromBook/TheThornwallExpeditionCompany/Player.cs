internal class Player
{
    public string Name { get; }
    public Contractor[] Contractors { get; protected set; } = new Contractor[5];
    public int ContractorCount { get; protected set; }
    public int GP { get; protected set; }
    public int DailyContractorsGPRate { get; protected set; }

    public Player(string name, int startingGP)
    {
        Name = name;
        GP = startingGP;
    }
    public bool HireContractor(Contractor contractor)
    {
        if (ContractorCount < Contractors.Length && contractor.DailyRate + DailyContractorsGPRate < GP) 
        {
            Contractors[ContractorCount++] = contractor;
            DailyContractorsGPRate += contractor.DailyRate;
            return true;
        }
        return false;
    }

    public Contractor DismissContractor(int index) 
    {
        DailyContractorsGPRate -= Contractors[index].DailyRate;
        Contractor dismiss = Contractors[index];
        Contractors[index] = null!;
        SortContractors();
        ContractorCount--;
        return dismiss;
    }

    protected void SortContractors()
    {
        for (int i = 0; i < ContractorCount - 1; i++)
        {
            for (int j = i + 1; j < ContractorCount; j++)
            {
                if (Contractors[i] == null)
                {
                    Contractors[i] = Contractors[j];
                    Contractors[j] = null!;
                }
            }
        }
    }

    public Contractor? GetRoleTypeContractor(RoleType roleType)
    {
        foreach (Contractor contractor in Contractors)
            if (contractor?.Roles.Contains(roleType) == true && contractor.Health > 0)
                return contractor;

        return null;
    }

    public string RolesReport()
    {
        string report = "Covered Roles:\n";

        // adds covered roles into report
        foreach (Contractor contractor in Contractors)
            if (contractor != null)
                report += $"{contractor.Name}: {contractor.RoleList()}\n";

        if (report == "Covered Roles:\n")
            report = "No contractor hired";

        report += "\n";

        // adds not covered roles into report
        report += MissingRolesReport();

        return report;
    }

    public string MissingRolesReport()
    {
        string report = null!;
        RoleType[] allRoles = Enum.GetValues<RoleType>();
        bool[] found = new bool[allRoles.Length];

        for (int i = 1; i < allRoles.Length; i++) // checks all except commoner(default enum)
        {
            for (int j = 0; j < Contractors.Length; j++)
            {
                if (Contractors[j] != null && Contractors[j].Roles.Contains(allRoles[i]))
                {
                    found[i] = true;
                    break;
                }
            }

            if (!found[i])
            {
                if (report != null) report += ", ";

                report += allRoles[i].ToString();
            }
        }

        return report == null ? "All roles are covered!" : "Missing roles:\n" + report;
    }

    public void TryReceiveEventReward(ExpeditionEvent expeditionEvent)
    {
        if (expeditionEvent.EventCompleted())
            GP += expeditionEvent.EventRewardGP;
    }

    public bool AllContractorsFullHP()
    {
        foreach (Contractor contractor in Contractors)
            if (contractor?.Health < contractor?.MaxHealth)
                return false;

        return true;
    }

    public bool HaveHealers()
    {
        foreach (Contractor contractor in Contractors)
            if (contractor is IHealer)
                return true;

        return false;
    }

    public bool AnyHealerCanPerformHealing()
    {
        foreach (Contractor contractor in Contractors)
            if (contractor is IHealer healer && healer.HealCooldown == 0)
                return true;

        return false;
    }

    public bool ContractorsAreWiped()
    {
        foreach (Contractor contractor in Contractors)
            if (contractor?.Health > 0)
                return false;

        return true;
    }

    public void UpdateGPAfterExpedition()
    {
        for (int i = 0; i < Contractors.Length; i++)
            if (Contractors[i]?.Health == 0)
                DailyContractorsGPRate -= Contractors[i].DailyRate;

        GP -= DailyContractorsGPRate;
    }

    public void UpdateContractorsAfterExpedition()
    {
        foreach (Contractor contractor in Contractors)
            if (contractor?.Health > 0)
                contractor.Reset();
    }

    public void UpdateAfterEvent()
    {
        foreach (Contractor contractor in Contractors)
            if (contractor is IHealer healer)
                healer.DecrementCooldown();
    }
}