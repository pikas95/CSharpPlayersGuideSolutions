internal static class ContractorStoreGenerator
{
    private static readonly int FighterMaxHealth = 180;
    private static readonly int FighterMaxDamage = 40;
    private static readonly string[] FighterNames =
    {
        "Bron the Bulwark", "Garruk Ironhide", "Hilda Stonearm", "Drogan Vael",
        "Marn the Unbroken", "Thessa Blackshield", "Korvash", "Ulric Greaves",
        "Vada Steeljaw", "Brunn the Wall"
    };
    
    private static readonly int MedicMaxHealth = 90;
    private static readonly int MedicMaxDamage = 12;
    private static readonly string[] MedicNames =
    {
        "Sister Elowen", "Caleb Mossheart", "Liraen Soft-hands", "Old Pell",
        "Wynn the Tender", "Asha Quill", "Father Demet", "Niamh Greenwillow",
        "Tobias Sage", "Mirel the Mild"
    };
    
    private static readonly int ScoutMaxHealth = 140;
    private static readonly int ScoutMaxDamage = 30;
    private static readonly string[] ScoutNames =
    {
        "Fenn Quickstep", "Ravi Nightshade", "Sable Vance", "Kestrel",
        "Dax Whisperwind", "Nyx Halloway", "Pike Swift", "Esra Lightfoot",
        "Corwin Gray", "Vesper Lark"
    };

    private static Random Random { get; } = new Random();

    public static List<Contractor> Generate(int size)
    {
        List<Contractor> Contractors = new List<Contractor>();
        
        for (int i = 0; i < size; i++)
        {
            int type = Random.Next(0, 7);
            Contractors.Add(type switch
            {
                1 => GenerateMedic(),
                2 => GenerateScout(),
                _ => GenerateFighter()
            });
        }
        
        return Contractors;
    }

    private static Contractor GenerateFighter()
    {
        int maxHealth = Random.Next(FighterMaxHealth / 2, FighterMaxHealth);
        int maxDamage = Random.Next(FighterMaxDamage / 2, FighterMaxDamage);
        int dailyRate = maxHealth / 10 + maxDamage / 2;
        
        int rolesCount = Random.Next(1, 3);
        RoleType[] roles = new RoleType[rolesCount];
        roles[0] = RoleType.Fighter;
        
        if (rolesCount > 1)
            roles = GenerateRoles(roles);
        
        return new Fighter(FighterNames[Random.Next(0, FighterNames.Length)],
                           maxHealth, 
                           dailyRate, 
                           maxDamage / 2, 
                           maxDamage,
                           roles
        );

        RoleType[] GenerateRoles(RoleType[] roles)
        {
            RoleType[] allRoles = Enum.GetValues<RoleType>();

            for (int i = 1; i < roles.Length; i++)
            {
                RoleType newRole = allRoles[Random.Next(0, allRoles.Length)];
                
                while (roles.Contains(newRole) || newRole == RoleType.Healer)
                    newRole = allRoles[Random.Next(0, allRoles.Length)];
                
                roles[i] = newRole;
            }

            return roles;
        }
    }

    private static Contractor GenerateMedic()
    {
        int maxHealth = Random.Next(MedicMaxHealth / 2, MedicMaxHealth);
        int maxDamage = Random.Next(MedicMaxDamage / 2, MedicMaxDamage);
        int dailyRate = maxHealth / 10 + maxDamage / 2 + 15;
        
        int rolesCount = Random.Next(2, 4);
        RoleType[] roles = new RoleType[rolesCount];
        roles[0] = RoleType.Healer;
        roles[1] = RoleType.Fighter;
        
        if (rolesCount > 2)
            roles = GenerateRoles(roles);
        
        return new Medic(MedicNames[Random.Next(0, FighterNames.Length)],
                         maxHealth, 
                         dailyRate, 
                         maxDamage / 2, 
                         maxDamage,
                         roles
        );

        RoleType[] GenerateRoles(RoleType[] roles)
        {
            RoleType[] allRoles = Enum.GetValues<RoleType>();

            for (int i = 2; i < roles.Length; i++)
            {
                RoleType newRole = allRoles[Random.Next(0, allRoles.Length)];
                
                while (roles.Contains(newRole))
                    newRole = allRoles[Random.Next(0, allRoles.Length)];
                
                roles[i] = newRole;
            }

            return roles;
        }
    }

    private static Contractor GenerateScout()
    {
        int maxHealth = Random.Next(ScoutMaxHealth / 2, ScoutMaxHealth);
        int maxDamage = Random.Next(ScoutMaxDamage / 2, ScoutMaxDamage);
        int dailyRate = maxHealth / 10 + maxDamage / 2;
        
        int rolesCount = Random.Next(2, 4);
        RoleType[] roles = new RoleType[rolesCount];
        roles[0] = RoleType.Fighter;
        roles[1] = RoleType.Healer;
        
        if (rolesCount > 2)
            roles = GenerateRoles(roles);
        
        return new Scout(ScoutNames[Random.Next(0, FighterNames.Length)],
                           maxHealth, 
                           dailyRate, 
                           maxDamage / 2, 
                           maxDamage,
                           roles
        );

        RoleType[] GenerateRoles(RoleType[] roles)
        {
            RoleType[] allRoles = Enum.GetValues<RoleType>();

            for (int i = 2; i < roles.Length; i++)
            {
                RoleType newRole = allRoles[Random.Next(0, allRoles.Length)];
                
                while (roles.Contains(newRole))
                    newRole = allRoles[Random.Next(0, allRoles.Length)];
                
                roles[i] = newRole;
            }

            return roles;
        }
    }
}