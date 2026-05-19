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
    }

    public void Run()
    {
        while (true) 
        {
            _renderer.DisplayPlayerAndGP(_player);
            _renderer.DisplayMainMenu();

            switch (_input.AskForIntInRange("What do you want to do? ", 0, 2))
            {
                case 1: ExpeditionMenu(); break;
                case 2: ContractorStore(); break;
                case 0: return;
            }
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
            else if (_player.ContractorCount != 0)
            {
                _renderer.DisplayOwnedContractors(_player.Contractors);
                _renderer.DisplayMissingRolesReport(_player.MissingRolesReport());

                if (_input.AskConfirmExpeditionLaunch())
                {
                    RunExpedition(_expeditions[choice - 1]); // displayed as index + 1 in console
                    return;
                }
            }
            else
            {
                _renderer.DisplayMessage("You can't start expedition without any contractors in your team", ConsoleColor.White);
                return;
            }
                
        }

    }

    private void RunExpedition(Expedition expedition)
    {
        for (int i = 0; i < expedition.Events.Length && !_player.ContractorsAreWiped(); i++)
        {
            // display before event
            _renderer.DisplayExpeditionProgress(i + 1, expedition.Events.Length);
            _renderer.DisplayExpedition(expedition);

            _renderer.DisplayPlayerAndGP(_player);
            _renderer.DisplayOwnedContractors(_player.Contractors);

            _renderer.DisplayEncounteredEvent(expedition.Events[i]);

            _input.WaitForEnter("Press enter to start event");

            if (expedition.Events[i] is AssaultEvent assaultEvent)
                AssaultEventHandler(assaultEvent);
            else if (expedition.Events[i] is RoleTypeEvent roleTypeEvent)
                RoleTypeEventHandler(roleTypeEvent);

            _player.TryReceiveEventReward(expedition.Events[i]);

            expedition.UpdateAfterEvent(i);
            _player.UpdateAfterEvent();

            // display after event
            _renderer.DisplayExpeditionProgress(i + 1, expedition.Events.Length);
            _renderer.DisplayExpedition(expedition);

            _renderer.DisplayPlayerAndGP(_player);
            _renderer.DisplayOwnedContractors(_player.Contractors);

            _renderer.DisplayAfterEventActions();
            int action = _input.AskForIntInRange("What do you want to do? ", 1, 2);

            switch (action)
            {
                case 1: HealMenu(); break;
                default: break;
            }
        }

        _player.UpdateGPAfterExpedition();

        _renderer.DisplayExpeditionResult(expedition, _player);

        _input.WaitForEnter("Press enter to return to main menu");

        _player.UpdateContractorsAfterExpedition();

        // display contractor loses(and remove them)

        // generate better
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

                    _input.AskForTargetOfAttacker(assaultEvent.Enemies, contractor).ReceiveDamage(contractor.Attack());

                    if (assaultEvent.EventCompleted())
                        break;
                }

            if (!assaultEvent.EventCompleted())
            {
                assaultEvent.EnemyAttackTurn(_player);

                _renderer.DisplayAssaultEvent(assaultEvent, i);
                _renderer.DisplayBattleVersus(_player.Contractors, assaultEvent.Enemies);

                _renderer.DisplayMessage("Enemies attacked your team!", ConsoleColor.Red);
            }
            else break;

            _input.WaitForEnter("Press enter to end round");
        }

        _renderer.DisplayEvent(assaultEvent);
        _renderer.DisplayBattleVersus(_player.Contractors, assaultEvent.Enemies);
        _renderer.DisplayBattleOutcome(assaultEvent.EventCompleted());

        _input.WaitForEnter("Press enter to continue");
    }

    private void RoleTypeEventHandler(RoleTypeEvent roleTypeEvent)
    {
        Contractor? requiredContractor = _player.GetRoleTypeContractor(roleTypeEvent.RoleRequired);

        _renderer.DisplayEncounteredEvent(roleTypeEvent);
        _renderer.DisplayRoleTypeEventRequirements(roleTypeEvent);
        _renderer.DisplayDoesPlayerMeetRequirements(requiredContractor);

        if (requiredContractor != null)
        {
            while (roleTypeEvent.LeftTryCount > 0 && !roleTypeEvent.EventCompleted())
            {
                _renderer.DisplayLeftTryCount(roleTypeEvent.LeftTryCount);

                if (roleTypeEvent.LeftTryCount != roleTypeEvent.MaxTries)
                    _renderer.DisplayRoleTypeEventTryOutcome(roleTypeEvent.EventCompleted());

                _input.WaitForEnter("Press enter to try");

                roleTypeEvent.Try(requiredContractor);
            }
        }

        if (!roleTypeEvent.EventCompleted() && roleTypeEvent is TrapEvent trapEvent)
        {
            trapEvent.DealTrapDamage(_player.Contractors);
            _renderer.DisplayMessage("Trap activated and dealt damage to your team!", ConsoleColor.Red);
        }

        _input.WaitForEnter("Press enter to continue");
    }

    private void HealMenu()
    {
        while (_player.HaveHealers() && _player.AnyHealerCanPerformHealing() && !_player.AllContractorsFullHP())
        {
            for (int i = 0; i < _player.Contractors.Length && !_player.AllContractorsFullHP(); i++)
                if (_player.Contractors[i] != null && _player.Contractors[i] is IHealer healer && healer.HealCooldown == 0)
                {
                    _renderer.DisplayOwnedContractors(_player.Contractors);
                    _renderer.DisplayHealOptions(healer);
                    _renderer.DisplayReturnOption();

                    int healingType = _input.AskForIntInRange("What type of heal to apply? ", 0, 3);

                    switch (healingType)
                    {
                        case 1:
                            healer.HealSelf(); 
                            break;
                        case 2:
                            _renderer.DisplayOwnedContractors(_player.Contractors);
                            Contractor target = _input.AskForTargetOfHealer(_player.Contractors);
                            if (target == healer)
                                healer.HealSelf();
                            else
                                healer.HealTarget(target);
                            break;
                        case 3:
                            healer.HealAll(_player.Contractors);
                            break;
                        default: 
                            return;
                    }

                    _renderer.DisplayMessage("Healing successfull!\n", ConsoleColor.Green);
                }
        }

        _renderer.DisplayOwnedContractors(_player.Contractors);

        if (_player.AllContractorsFullHP())
            _input.WaitForEnter("All contractors have full health. Press enter to continue to next event");
        else if (_player.HaveHealers())
            _input.WaitForEnter("Healers under cooldown. Press enter to continue to next event");
        else
            _input.WaitForEnter("You can't perform healing. Press enter to continue to next event");

    }

    private void ContractorStore()
    {
        while (true)
        {
            _renderer.DisplayPlayerAndGP(_player);
            _renderer.DisplayRolesReport(_player.RolesReport());
            _renderer.DisplayHirableContractors(_contractorStore);
            _renderer.DisplayContractorStoreActions(_player.ContractorCount != 0);
            _renderer.DisplayReturnOption();

            int action = _input.AskForIntInRange("What do you want to do? ", 0, _player.ContractorCount != 0 ? 2 : 1);

            switch (action)
            {
                case 0: return;
                case 1: HireContractor(); break;
                case 2: DismissContractor(); break;
            }
        }    
        
        void HireContractor()
        {
            _renderer.DisplayPlayerAndGP(_player);
            _renderer.DisplayRolesReport(_player.RolesReport());
            _renderer.DisplayHirableContractors(_contractorStore);

            int input = _input.AskForIntInRange("Which contractor do you wish to hire? ", 1, _contractorStore.Length);
            // contractors are displayed index + 1 in console

            if (_player.HireContractor(_contractorStore[input - 1]))
            {
                _contractorStore[input - 1] = null!;
                SortContractorStore();

                _renderer.DisplayMessage("Contractor was successfully hired", ConsoleColor.Green);
            }
            else
            {
                if (_player.ContractorCount == _player.Contractors.Length)
                    _renderer.DisplayMessage("Your team is full!", ConsoleColor.Red);
                else
                    _renderer.DisplayMessage("Not enough GP!", ConsoleColor.Red);
            }
        }

        void DismissContractor()
        {
            _renderer.DisplayPlayerAndGP(_player);
            _renderer.DisplayOwnedContractors(_player.Contractors);

            int input = _input.AskForIntInRange("Which contractor you wish to dismiss? ", 1, _player.ContractorCount);
            Contractor dismissed = _player.DismissContractor(input - 1);
            // contractors are displayed index + 1 in console

            _renderer.DisplayMessage($"{dismissed.Name} was dissmissed", ConsoleColor.Green);

            for (int i = 0; i < _contractorStore.Length; i++) 
                if (_contractorStore[i] == null)
                {
                    _contractorStore[i] = dismissed;
                    return;
                }
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