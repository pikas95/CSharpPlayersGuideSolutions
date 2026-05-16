internal class Expedition
{
    public string Name { get; }
    public string Destination { get; protected set; }
    public Contractor[] Contractors { get; protected set; } = new Contractor[5];
    public ExpeditionEvent[] Events { get; protected set; }
    protected int Count { get; set; }
    
    public Expedition(string name, string destination, ExpeditionEvent[] events)
    {
        Name = name;
        Destination = destination;
        Events = events;
    }

    public bool HireContractor(Contractor contractor)
    {
        if (Count < Contractors.Length)
        {
            Contractors[Count++] = contractor;
            return true;
        }
        return false;
    }

    public Contractor DismissContractor(int index)
    {
        Contractor dismiss = Contractors[index];
        Contractors[index] = null!;
        SortContractors();
        Count--;
        return dismiss;
    }

    protected void SortContractors()
    {
        for (int i = 0; i < Count - 1; i++)
        {
            for (int j = i + 1; j < Count; j++)
            {
                if (Contractors[i] == null)
                {
                    Contractors[i] = Contractors[j];
                    Contractors[j] = null!;
                }
            }
        }
    }

    public string RolesReport()
    {
        string report = "Covered Roles:\n";

        // adds covered roles into report
        foreach (Contractor contractor in Contractors)
            if (contractor != null)
                report += $"{contractor.Name}: {contractor.RoleList()}\n";

        report += "\n";

        // adds not covered roles into report
        report += MissingRolesReport();

        return report;
    }

    private string MissingRolesReport()
    {
        string report = null!;
        RoleType[] allRoles = Enum.GetValues<RoleType>();
        bool[] found = new bool[allRoles.Length];

        for (int i = 0; i < allRoles.Length; i++)
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
}