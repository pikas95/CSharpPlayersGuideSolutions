internal class GameProgram
{
    private readonly Map _map = new Map();
    private Player _player;

    public GameProgram() => _player = CreatePlayer();

    public void RunGame()
    {
        while (_player.Health > 0) 
        {
            Room currentRoom = _map.GetRoom(_player.Row, _player.Col);

            if (currentRoom.Enemy?.Health > 0)
            {
                if (!FightEnemy(currentRoom, _player))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"You died from {currentRoom.Enemy.Name}...");
                    Console.ForegroundColor = ConsoleColor.Black;
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
                Console.ForegroundColor = ConsoleColor.Black;
                break;
            }

            Console.Clear();
        }
    }

    private Player CreatePlayer()
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
        Console.WriteLine($"Health: {_player.Health} | Weapon: {_player.EquipedWeapon.Name}");
        Console.ForegroundColor = ConsoleColor.White;
    }

    private void DisplayCurrentRoom(Room room)
    {
        if (room.Enemy != null)
            Console.ForegroundColor = ConsoleColor.Red;
        else if (room.Weapon != null)
            Console.ForegroundColor = ConsoleColor.Green;
        else 
            Console.ForegroundColor = ConsoleColor.Gray;

        Console.WriteLine($"Your surroundings: {room.Description}");
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.White;
    }

    private bool DisplayInventory(Inventory[] inventory)
    {
        if (inventory[0]?.Weapon == null)
        {
            Console.WriteLine("Inventory is empty");
            Console.WriteLine();
            return false;
        }

        Console.WriteLine("Your inventory: ");
        for (int i = 0; i < inventory.Length; i++)
            if (inventory[i] != null)
                Console.WriteLine($"[{i + 1}] {inventory[i].Weapon.Name}");

        Console.WriteLine();
        return true;
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

            Console.ForegroundColor = ConsoleColor.DarkMagenta;

            Console.WriteLine($"[1] Move Up\n" +
                          $"[2] Move Right\n" +
                          $"[3] Move Down\n" +
                          $"[4] Move Left");

            if (_player.Inventory[0] != null)
            {
                Console.WriteLine($"[5] Change Equiped Weapon");
                Console.WriteLine($"[6] Remove Weapon From Inventory");
            }

            if (currentRoom.Weapon != null)
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
                Console.Write($"{_player.Name}, what do you want to do? ");
                int input = Convert.ToInt32(Console.ReadLine());

                if (input >= 0 &&
                    _player.Inventory[0] != null ? 
                    (currentRoom.Weapon != null ? input <= 7 : input <= 6) :
                    (currentRoom.Weapon != null ? input <= 4 || input == 7 : input <= 4))
                {
                    switch (input)
                    {
                        case 1: 
                            {
                                if (!MovePlayer(0, -1))
                                    continue;
                                break;
                            }
                        case 2:
                            {
                                if (!MovePlayer(1, 0))
                                    continue;
                                break;
                            }
                        case 3: 
                            {
                                if (!MovePlayer(0, 1))
                                    continue;
                                break;
                            }
                        case 4: 
                            {
                                if (!MovePlayer(-1, 0))
                                    continue;
                                break;
                            }
                        case 5: 
                            _player.EquipWeapon(AskWeapon("Which weapon you wish to equip? "));
                            break;
                        case 6:
                            _player.RemoveFromInventory(AskWeapon("Which weapon you wish to remove from inventory? "));
                            break;
                        case 7:
                            _player.AddToInventory(currentRoom.TakeWeapon());
                            break;
                        case 0:
                            return true;
                        default: 
                            break;
                    };

                    break;
                }  
                else Console.WriteLine("There's no such option.");
            }

            return false;
        }

        bool MovePlayer(int row, int col)
        {
            if (_player.Row + row < 0 || 
                _player.Col + col < 0 ||
                _player.Row + row >= _map.EdgeNumber() || 
                _player.Col + col >= _map.EdgeNumber())
            {
                Console.WriteLine("You bumped into a wall..");
                return false;
            }

            _player.Move(row, col);
            return true;
        }

        int AskWeapon(string description)
        {
            Console.Write(description);

            while (true)
            {
                int input = Convert.ToInt32(Console.ReadLine()) - 1; //  because weapons are displayed as index + 1

                if (_player.Inventory[input]?.Weapon != null) 
                    return input;

                Console.Write("There is no such weapon. Again: ");
            }
        }
    }

    private bool FightEnemy(Room room, Player player)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"{player.Name}, you have entered {room.Name}.");
        Console.WriteLine($"{room.Enemy!.Name} has noticed you and began running towards you!");
        Console.ForegroundColor = ConsoleColor.White;

        Console.Write("Press enter to begin battle");
        Console.ReadLine();

        while (player.Health > 0 && room.Enemy.Health > 0)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Your health: {player.Health} | {room.Enemy.Name} health: {room.Enemy.Health}");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("[1] Stab Attack " +
                              "[2] Power Attack");

            int input = Convert.ToInt32(Console.ReadLine());

            while (input != 1 && input != 2)
            {
                Console.Write("There is no such attack. Again: ");
                input = Convert.ToInt32(Console.ReadLine());
            }

            int playerDamage = input switch
            {
                1 => _player.StabAttack(),
                2 => _player.PowerAttack(),
                _ => _player.StabAttack(),
            };

            room.Enemy.ReceiveDamage(playerDamage);

            player.ReceiveDamage(room.Enemy.Attack());
        }

        Console.Clear();

        if (player.Health <= 0)
            return false;

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"You have killed {room.Enemy.Name}!");
        Console.ForegroundColor = ConsoleColor.White;

        room.RoomUpdateEnemyDead();
        Console.Write("Press enter to continue");
        Console.ReadLine();
        Console.Clear();

        return true;
    }
}