internal class GameRenderer
{
    public void DisplayMainMenu()
    {
        Console.WriteLine("");
    }
    public void DisplayOwnedContractorRoles(Expedition expedition)
    {
        Console.WriteLine(expedition.RolesReport());
        Console.WriteLine();
    }

    public void DisplayOwnedContractors(Contractor[] contractors)
    {
        string contractorsReport = null!;
        foreach (Contractor contractor in contractors) 
            if (contractor != null)
                contractorsReport += $"{contractor}\n";

        Console.WriteLine(contractorsReport == null ? "No contractors hired" : "Owned Contractors:\n" + contractorsReport);
    }

    public void DisplayHirableContractors(Contractor[] pool)
    {
        string poolText = null!;

        for (int i = 0; i < pool.Length; i++)
            if (pool[i] != null)
                poolText += $"[{i + 1}] {pool[i]}\n";

        Console.WriteLine((poolText == null ? "Contractor pool is empty\n" : poolText) + "[0] to exit");
        Console.WriteLine();
    }
}