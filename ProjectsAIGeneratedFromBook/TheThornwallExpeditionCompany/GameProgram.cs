internal class GameProgram
{
    private Contractor[] _contractorPool;
    private Expedition _expedition;

    public GameProgram()
    {
        _contractorPool = GenerateContractorPool();
        _expedition = GenerateExpedition();
    }

    public void Run()
    {
        HiringContractors(); // make more intuitive and clean

        // before starting the expedition make rolereport
        // and waitforenter before start



        // display expedition Event: assault - begin combat,
        // trap - force to solve(if no trapper receive damage immediately),
        // puzzle/treasure ask to do it or go to other event

        // after event display status - contractors health; actions for healing
        // display what event awaits
        // wait for enter


        // after finished expedition, displayed gained GP, current health
        // display contractor loses(and remove them)


        // show other available expeditions
        // create a class to store contractors, GP
    }

    private void HiringContractors()
    {
        while (true)
        {
            Console.WriteLine(_expedition.RolesReport());
            Console.WriteLine();

            for (int i = 0; i < _contractorPool.Length; i++)
                if (_contractorPool[i] != null)
                    Console.WriteLine($"[{i + 1}] {_contractorPool[i]}");

            Console.Write("Which contractor you wish to hire ([0] to exit)? ");

            int input = Convert.ToInt32(Console.ReadLine());

            if (InputValid(input))
            {
                if (input == 0)
                    break;
                else
                {
                    _expedition.HireContractor(_contractorPool[input - 1]);
                    _contractorPool[input - 1] = null!;
                }
            }
            
            Console.Clear();
        }

        bool InputValid(int input)
        {
            if (input >= 0 && input <= _contractorPool.Length)
                return true;
            return false;
        }
        
    }

    /*private Contractor[] SortContractorPool()
    {
        for (int i =)
    }*/

    private Contractor[] GenerateContractorPool()
    {
        return [new Fighter(), new Fighter(), new Medic(), new Scout()];
    }

    private Expedition GenerateExpedition()
    {
        return new Expedition("Testing Expedition", "Working Program", GenerateExpeditions());
    }

    private ExpeditionEvent[] GenerateExpeditions()
    {
        return [new AssaultEvent(GenerateEnemies()), 
                new AssaultEvent(GenerateEnemies()), 
                new TrapEvent(10), 
                new PuzzleEvent(),
                new TreasureEvent()];
    }

    private Enemy[] GenerateEnemies()
    {
        return [new Goblin(), new Wolf()];
    }
}