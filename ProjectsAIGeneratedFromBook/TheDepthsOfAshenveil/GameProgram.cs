internal class GameProgram
{
    private readonly Map _map = new Map();
    private readonly Player _player;

    public GameProgram() => _player = CreatePlayer();

    public void RunGame()
    {
        
        while (_player.Health > 0) 
        {
            Room currentRoom = _map.GetRoom(_player.Col, _player.Row);

            if (currentRoom is EnemyRoom room && room.Enemy.Health > 0)
            {
                if (!FightEnemy(room))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"You died from {room.Enemy.Name}...");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                }
            }

            // displays player health and weapon
            DisplayPlayerStats();

            // displays room player is in
            DisplayCurrentRoom(currentRoom);

            // displays player's inventory
            DisplayInventory(_player.Inventory);    

            if (PlayerTurn(currentRoom))
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Congratulations {_player.Name}, you came out the dungeon alive!");
                Console.ForegroundColor = ConsoleColor.White;
                break;
            }

            Console.Clear();
        }
    }

    private static Player CreatePlayer()
    {
        Console.Write("What's your name? ");
        string input = Console.ReadLine()!;

        while (input == null || input == "")
        {
            Console.Write("Name can not be empty. ");
            input = Console.ReadLine()!;
        }

        Console.Clear();
        return new Player(input, new Weapon("Rusty Sword", 1, 5));
    }

    private void DisplayPlayerStats()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"{_player.Name} | Health: {_player.Health} | Weapon: {_player.EquippedWeapon.Name}");
        Console.ForegroundColor = ConsoleColor.White;
    }

    private static void DisplayCurrentRoom(Room room)
    {
        Console.ForegroundColor = room.DisplayColor();

        Console.WriteLine($"Your surroundings: {room.Description}");
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.White;
    }

    private static void DisplayInventory(Inventory inventory)
    {
        if (inventory.Weapons[0] == null)
            Console.WriteLine("Inventory is empty");
        else
        {
            Console.WriteLine("Your inventory: ");
            for (int i = 0; i < inventory.Weapons.Length; i++)
                if (inventory.Weapons[i] != null)
                    Console.WriteLine($"[{i + 1}] {inventory.Weapons[i]!.Name}");
        }

        Console.WriteLine();
    }

    private bool PlayerTurn(Room currentRoom)
    {
        DisplayActions();

        if (MakeTurn())
            return true;
        return false;
        
        void DisplayActions()
        {
            Console.WriteLine("You can do:");

            Console.ForegroundColor = ConsoleColor.Magenta;

            Console.WriteLine($"[1] Move Up\n" +
                          $"[2] Move Right\n" +
                          $"[3] Move Down\n" +
                          $"[4] Move Left");

            if (_player.Inventory.Weapons[0] != null)
            {
                Console.WriteLine($"[5] Change Equiped Weapon");
                Console.WriteLine($"[6] Remove Weapon From Inventory");
            }

            if (currentRoom is WeaponRoom room && room.Weapon != null)
                Console.WriteLine($"[7] Collect Item");

            if (_player.Row == 0 && _player.Col == 0)
                Console.WriteLine("[0] Exit Dungeon");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }

        bool MakeTurn()
        {
            while (true)
            {
                int input = GetValidInput();
            
                switch (input)
                {
                    case 1:
                        if (!MovePlayer(-1, 0))
                            continue;
                        break;
                    case 2:
                        if (!MovePlayer(0, 1))
                            continue;
                        break;
                    case 3:
                        if (!MovePlayer(1, 0))
                            continue;
                        break;
                    case 4:
                        if (!MovePlayer(0, -1))
                            continue;
                        break;
                    case 5:
                        _player.EquipWeapon(AskWeapon("Which weapon you wish to equip? "));
                        break;
                    case 6:
                        _player.RemoveFromInventory(AskWeapon("Which weapon you wish to remove from inventory? "));
                        break;
                    case 7:
                        // this case will only compute if input = 7 validates through IsValidAction() 
                        // so casting will always compile
                        _player.AddToInventory(((WeaponRoom)currentRoom).TakeWeapon());
                        break;
                    case 0:
                        return true;
                };

                break;
            }
            
            return false;
        }

        int GetValidInput()
        {
            Console.Write($"{_player.Name}, what do you want to do? ");
            int input = Convert.ToInt32(Console.ReadLine()); // TODO: implement TryParse

            while (!IsValidAction(input))
            {
                Console.Write("There's no such option. Again: ");
                input = Convert.ToInt32(Console.ReadLine()); // TODO: implement TryParse
            }

            return input;
        }

        bool IsValidAction(int input)
        {
            if (_player.Row == 0 && _player.Col == 0 && input >= 0 && EndRangeIsValid())
                return true;
            else if (input >= 1 && EndRangeIsValid())
                return true;

            return false;

            bool EndRangeIsValid()
            {
                if (input <= 4)
                    return true;
                else if (_player.Inventory.Weapons[0] != null)
                {
                    if (currentRoom is WeaponRoom room && room.Weapon != null && input <= 7)
                        return true;
                    else if (input <= 6)
                        return true;
                }
                else if (currentRoom is WeaponRoom room && room.Weapon != null && input == 7)
                    return true;

                return false;
            }
        }

        bool MovePlayer(int col, int row)
        {
            if (_player.Row + row < 0 || 
                _player.Col + col < 0 ||
                _player.Row + row >= _map.ArraySize || 
                _player.Col + col >= _map.ArraySize)
            {
                Console.WriteLine("You bumped into a wall..");
                return false;
            }

            _player.Move(col, row);
            return true;
        }

        int AskWeapon(string description)
        {
            Console.Write(description);

            while (true)
            { // TODO: implement TryParse
                int input = Convert.ToInt32(Console.ReadLine()) - 1; //  because weapons are displayed as index + 1

                if (input >= 0 && input < _player.Inventory.Weapons.Length && _player.Inventory.Weapons[input] != null) 
                    return input;

                Console.Write("There is no such weapon. Again: ");
            }
        }
    }

    private bool FightEnemy(EnemyRoom room)
    {
        Console.Clear();
        Console.ForegroundColor = room.DisplayColor();
        Console.WriteLine($"{_player.Name}, you have entered {room.Name}.");
        Console.WriteLine($"{room.Enemy.Name} has noticed you and began running towards you!");
        Console.ForegroundColor = ConsoleColor.White;

        Console.Write("Press enter to begin battle");
        Console.ReadLine();

        while (_player.Health > 0 && room.Enemy.Health > 0)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Your health: {_player.Health} | {room.Enemy.Name} health: {room.Enemy.Health}");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("[1] Stab Attack " +
                              "[2] Power Attack");

            int input = Convert.ToInt32(Console.ReadLine()); // TODO: implement TryParse

            while (input != 1 && input != 2)
            {
                Console.Write("There is no such attack. Again: ");
                input = Convert.ToInt32(Console.ReadLine()); // TODO: implement TryParse
            }

            int playerDamage = input switch
            {
                1 => _player.StabAttack(),
                2 => _player.PowerAttack(),
                _ => _player.StabAttack(),
            };

            room.Enemy.ReceiveDamage(playerDamage);
            
            if (room.Enemy.Health > 0)
                _player.ReceiveDamage(room.Enemy.Attack());
        }

        Console.Clear();

        if (_player.Health <= 0)
            return false;

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"You have killed {room.Enemy.Name}!");
        Console.ForegroundColor = ConsoleColor.White;

        room.MarkEnemyDead();
        Console.Write("Press enter to continue");
        Console.ReadLine();
        Console.Clear();

        return true;
    }
}