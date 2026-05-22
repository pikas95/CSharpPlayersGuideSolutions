internal class GameProgram
{
    private const int _contractorStoreSize = 7;
    private const int _expeditionListSize = 5;
    private readonly GameRenderer _renderer = new GameRenderer();
    private readonly InputHandler _input = new InputHandler();
    private Contractor?[] _contractorStore;
    private readonly Player _player;
    private readonly Expedition?[] _expeditions;

    public GameProgram()
    {
        _contractorStore = ContractorStoreGenerator.Generate(_contractorStoreSize);
        _expeditions = ExpeditionsGenerator.Generate(_expeditionListSize);
        _player = CreatePlayer();
    }

    public void Run()
    {
        while (true) 
        {
            _renderer.DisplayPlayerAndGP(_player);
            _renderer.DisplayMainMenu();

            switch (_input.AskForIntInRange("What do you want to do? ", 0, 3))
            {
                case 1: ExpeditionMenu(); break;
                case 2: ContractorStore(); break;
                case 3: WaitADay(); break;
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

            int input = _input.AskForIntInRange("Which expedition you wish to conquer? ", 0, _expeditions.Length);
            
            if (input == 0)
                return;
            
            while (_expeditions[input - 1] == null) // displayed as index + 1 in console
            {
                _renderer.DisplayAllExpeditions(_expeditions);
                _renderer.DisplayReturnOption();
            
                input = _input.AskForIntInRange("No such option. Again: ", 0, _expeditions.Length);
            }
            
            
            if (_player.ContractorCount != 0)
            {
                _renderer.DisplayOwnedContractors(_player.Contractors);
                _renderer.DisplayMissingRolesReport(_player.MissingRolesReport());

                if (_input.AskConfirmExpeditionLaunch())
                {
                    RunExpedition(_expeditions[input - 1]!); // displayed as index + 1 in console

                    _expeditions[input - 1] = null;
                    SortExpeditions();
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
            }
        }

        _player.PayContractorsAfterExpedition();

        _renderer.DisplayExpeditionResult(expedition, _player);

        _input.WaitForEnter("Press enter to return to main menu");

        _player.RemoveAnyDeadContractor();
        _player.ResetContractors();
        _contractorStore = ContractorStoreGenerator.Generate(_contractorStoreSize);
    }

    private void AssaultEventHandler(AssaultEvent assaultEvent)
    {
        for (int i = 1; !_player.ContractorsAreWiped() && !assaultEvent.EventCompleted(); i++)
        {
            foreach (Contractor? contractor in _player.Contractors)
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

        if (requiredContractor != null)
        {
            while (roleTypeEvent.LeftTryCount > 0 && !roleTypeEvent.EventCompleted())
            {
                _renderer.DisplayEncounteredEvent(roleTypeEvent);
                _renderer.DisplayRoleTypeEventRequirements(roleTypeEvent);
                _renderer.DisplayDoesPlayerMeetRequirements(requiredContractor);
                _renderer.DisplayLeftTryCount(roleTypeEvent.LeftTryCount);

                if (roleTypeEvent.LeftTryCount != roleTypeEvent.MaxTries)
                    _renderer.DisplayRoleTypeEventTryOutcome(roleTypeEvent.EventCompleted());

                _input.WaitForEnter("Press enter to try");

                roleTypeEvent.Try(requiredContractor);
            }
        }

        _renderer.DisplayEncounteredEvent(roleTypeEvent);
        _renderer.DisplayRoleTypeEventRequirements(roleTypeEvent);
        _renderer.DisplayDoesPlayerMeetRequirements(requiredContractor);
        
        if (!roleTypeEvent.EventCompleted() && roleTypeEvent is TrapEvent trapEvent)
        {
            trapEvent.DealTrapDamage(_player.Contractors);
            _renderer.DisplayMessage("Trap activated and dealt damage to your team!", ConsoleColor.Red);
        }

        _renderer.DisplayRoleTypeEventTryOutcome(roleTypeEvent.EventCompleted());
        _input.WaitForEnter("Press enter to continue");
    }

    private void HealMenu()
    {
        if (_player.AnyHealerCanPerformHealing() && !_player.AllContractorsFullHP())
        {
            for (int i = 0; i < _player.Contractors.Length && !_player.AllContractorsFullHP(); i++)
            {
                if (_player.Contractors[i] != null && _player.Contractors[i] is IHealer healer && healer.HealCooldown == 0)
                {
                    _renderer.DisplayOwnedContractors(_player.Contractors);
                    _renderer.DisplayHealOptions(healer);
                    _renderer.DisplayReturnOption();

                    int healingType = _input.AskForIntInRange("What type of heal to apply? ", 0, 4);

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
                        case 4: 
                            continue;
                        default: 
                            return;
                    }

                    _renderer.DisplayMessage("Healing successful!\n", ConsoleColor.Green);
                }
            }
        }

        _renderer.DisplayOwnedContractors(_player.Contractors);

        if (_player.AllContractorsFullHP())
            _input.WaitForEnter("All contractors have full health. Press enter to continue to next event");
        else if (_player.AnyHealerCanPerformHealing())
            _input.WaitForEnter("Press enter to continue to next event");
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

            int input = _input.AskForIntInRange("Which contractor do you wish to hire? ", 1, _contractorStore.Length) - 1;
            // contractors are displayed index + 1 in console

            while (_contractorStore[input] == null)
            {
                _renderer.DisplayPlayerAndGP(_player);
                _renderer.DisplayRolesReport(_player.RolesReport());
                _renderer.DisplayHirableContractors(_contractorStore);
                
                input = _input.AskForIntInRange("No such option. Again: ", 1, _contractorStore.Length) - 1;
            }
            
            if (_player.HireContractor(_contractorStore[input]!))
            {
                _contractorStore[input] = null!;
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

            int input = _input.AskForIntInRange("Which contractor you wish to dismiss? ", 1, _player.ContractorCount) - 1;
            Contractor dismissed = _player.DismissContractor(input);
            // contractors are displayed index + 1 in console

            _renderer.DisplayMessage($"{dismissed.Name} was dismissed", ConsoleColor.Green);

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

    private void SortExpeditions()
    {
        for (int i = 0; i < _expeditions.Length - 1; i++)
        {
            if (_expeditions[i] == null)
            {
                _expeditions[i] = _expeditions[i + 1];
                _expeditions[i + 1] = null;
            }
        }
    }

    private void WaitADay()
    {
        _player.PayContractorsAfterExpedition();
        _contractorStore = ContractorStoreGenerator.Generate(_contractorStoreSize);
    }

    private Player CreatePlayer()
    {
        return new Player(_input.AskName(), 100);
    }
}