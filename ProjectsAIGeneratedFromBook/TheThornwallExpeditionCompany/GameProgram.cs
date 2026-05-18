internal class GameProgram
{
    private GameRenderer _renderer = new GameRenderer();
    private InputHandler _input = new InputHandler();
    private Player _player;
    private Contractor[] _contractorStore;
    private Expedition[] _expeditions;

    public GameProgram()
    {
        _contractorStore = GenerateContractorStore();
        _expeditions = GenerateExpeditions();
        _player = CreatePlayer();
        _player.HireContractor(new Fighter());
    }

    public void Run()
    {
        while (true) // add gp logic, add heal logic
        {
            _renderer.DisplayPlayerAndGP(_player);
            _renderer.DisplayMainMenu();

            switch (_input.AskForIntInRange("What do you want to do? ", 0, 2))
            {
                case 1: ExpeditionMenu(); break;
                case 2: ContractorStore(); break;
            }
            // before starting the expedition make rolereport
            // and waitforenter before start


            // after finished expedition, displayed gained GP, current health
            // display contractor loses(and remove them)


            // show other available expeditions
            // create a class to store contractors, GP
        }
    }

    private void ExpeditionMenu()
    {
        while (true)
        {
            _renderer.DisplayAllExpeditions(_expeditions);
            _renderer.DisplayReturnOption();

            int choice = _input.AskForIntInRange("Which expedition you wish to conquer? ", 0, _expeditions.Length);

            if (choice == 0)
                return;
            else
            {
                _renderer.DisplayOwnedContractors(_player.Contractors);
                _renderer.DisplayMissingRolesReport(_player.MissingRolesReport());

                if (_input.AskConfirmExpeditionLaunch())
                {
                    RunExpedition(_expeditions[choice - 1]); // displayed as index + 1 in console
                    return;
                }
            }
        }

    }

    private void RunExpedition(Expedition expedition)
    {
        for (int i = 0; i < expedition.Events.Length && !_player.ContractorsAreWiped(); i++)
        {
            _renderer.DisplayExpeditionProgress(i, expedition.Events.Length);
            _renderer.DisplayExpedition(expedition);

            _renderer.DisplayPlayerAndGP(_player);
            _renderer.DisplayOwnedContractors(_player.Contractors);

            _renderer.DisplayEncounteredEvent(expedition.Events[i]);

            _input.WaitForEnter("Press enter to start event");

            if (expedition.Events[i] is AssaultEvent assaultEvent)
                AssaultEventHandler(assaultEvent);
            else if (expedition.Events[i] is RoleTypeEvent roleTypeEvent)
                RoleTypeEventHandler(roleTypeEvent);

            if (expedition.Events[i].EventCompleted())
                _player.ReceiveEventReward(expedition.Events[i].GrantEventReward());

            // display expedition Event: assault - begin combat,
            // trap - force to solve(if no trapper receive damage immediately),
            // puzzle/treasure ask to do it or go to other event

            // after event display status - contractors health; actions for healing
            // display what event awaits
            // wait for enter
        }

        // add logic for displaying expedition outcome
    }

    private void AssaultEventHandler(AssaultEvent assaultEvent)
    {
        for (int i = 1; !_player.ContractorsAreWiped() && !assaultEvent.EventCompleted(); i++)
        {
            foreach (Contractor contractor in _player.Contractors)
                if (contractor != null)
                {
                    _renderer.DisplayAssaultEvent(assaultEvent, i);
                    _renderer.DisplayBattleVersus(_player.Contractors, assaultEvent.Enemies);
                    
                    if (i > 1)
                        _renderer.DisplayEnemyAttackMessage();

                    _input.AskForTargetOfAttacker(assaultEvent.Enemies, contractor).ReceiveDamage(contractor.Attack());

                    if (assaultEvent.EventCompleted())
                        break;
                }

            if (!assaultEvent.EventCompleted())
            {
                assaultEvent.EnemyAttackTurn(_player);

                _renderer.DisplayAssaultEvent(assaultEvent, i);
                _renderer.DisplayBattleVersus(_player.Contractors, assaultEvent.Enemies);
            }
            else break;

            _input.WaitForEnter("Press enter to end round.");
        }

        _renderer.DisplayEvent(assaultEvent);
        _renderer.DisplayBattleVersus(_player.Contractors, assaultEvent.Enemies);
        _renderer.DisplayBattleOutcome(assaultEvent.EventCompleted());

        _input.WaitForEnter("Press enter to continue");
    }

    private void RoleTypeEventHandler(RoleTypeEvent roleTypeEvent)
    {
        while (roleTypeEvent.LeftTryCount > 0)
        {
            roleTypeEvent.Try(_player);
        }
    }

    private void ContractorStore()
    {
        while (true)
        {
            _renderer.DisplayRolesReport(_player.RolesReport());
            _renderer.DisplayHirableContractors(_contractorStore);
            _renderer.DisplayReturnOption();

            int input = _input.AskForIntInRange("Which contractor you wish to hire? ", 0, _contractorStore.Length);

            if (input == 0)
            {
                Console.Clear();
                return;
            }
            else
            {
                // contractors are displayed index + 1 in console
                _player.HireContractor(_contractorStore[input - 1]);
                _contractorStore[input - 1] = null!;
                SortContractorStore();
            }

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

    private Player CreatePlayer()
    {
        return new Player(_input.AskName(), 100);
    }

    private Contractor[] GenerateContractorStore()
    {
        return [new Fighter(), new Fighter(), new Medic(), new Scout()];
    }

    private Expedition[] GenerateExpeditions()
    {
        return [new Expedition("Testing Expedition", "Working Program", GenerateExpeditionEvents())];
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