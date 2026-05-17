internal class GameProgram
{
    private GameRenderer _renderer = new GameRenderer();
    private InputHandler _input = new InputHandler();
    private Contractor[] _contractorStore;
    private Expedition _expedition;

    public GameProgram()
    {
        _contractorStore = GenerateContractorStore();
        _expedition = GenerateExpedition();
    }

    public void Run()
    {
        ContractorStore(); // make more intuitive and clean

        _renderer.DisplayOwnedContractors(_expedition.Contractors);
        _input.WaitForEnter("Press enter to begin expedition");
        Console.Clear();
        // before starting the expedition make rolereport
        // and waitforenter before start


        for (int i = 0; i < _expedition.Events.Length; i++ )
        {
            Console.WriteLine($"Expedition: {_expedition.Name} | Destination: {_expedition.Destination}");
            _renderer.DisplayOwnedContractors(_expedition.Contractors);

            Console.WriteLine(_expedition.Events[i]);
            Console.WriteLine();

            _input.WaitForEnter("Press enter to simulate event");

            while (!_expedition.Events[i].EventCompleted())
            {
                _expedition.Events[i].StartEvent(_expedition.Contractors);

                Console.WriteLine("Status:");
                _renderer.DisplayOwnedContractors(_expedition.Contractors);

                _input.WaitForEnter("Press enter to attempt event again");
            }
            

            Console.Clear();
        }
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

    private void ContractorStore()
    {
        while (true)
        {
            _renderer.DisplayOwnedContractorRoles(_expedition);
            _renderer.DisplayHirableContractors(_contractorStore);
            int input = _input.AskContractorToHire(_contractorStore);

            if (input == 0)
            {
                Console.Clear();
                break;
            }
            else
            {
                // contractors are displayed index + 1 in _renderer
                _expedition.HireContractor(_contractorStore[input - 1]);
                _contractorStore[input - 1] = null!;
            }

            SortContractorStore();

            Console.Clear();
        }        
    }

    private void SortContractorStore()
    {
        for (int i = 0; i < _contractorStore.Length - 1; i++)
            for (int j = i + 1; j < _contractorStore.Length; j++)
                if (_contractorStore[i] == null && _contractorStore[j] != null)
                {
                    _contractorStore[i] = _contractorStore[j];
                    _contractorStore[j] = null!;
                }
    }

    private Contractor[] GenerateContractorStore()
    {
        return [new Fighter(), new Fighter(), new Medic(), new Scout()];
    }

    private Expedition GenerateExpedition()
    {
        return new Expedition("Testing Expedition", "Working Program", GenerateExpeditionEvents());
    }

    private ExpeditionEvent[] GenerateExpeditionEvents()
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