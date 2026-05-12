internal class GameProgram
{
    private readonly Map _map = new Map();
    private Player _player;

    public GameProgram() => _player = CreatePlayer();

    public void RunGame()
    {
        while (_player.IsAlive) 
        {
            Room currentRoom = _map.GetRoom(_player.Row, _player.Col);

            // displays room player is in
            DisplayCurrentRoom(currentRoom);

            // displays equiped weapon
            DisplayEquipedWeapon(_player.EquipedWeapon);

            // displays player's inventory
            DisplayInventory(_player.Inventory);

            //if (currentRoom._enemy != null)
            //    if (FightEnemy())
            //        PlayerDied();

            PlayerTurn(currentRoom);

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
        return new Player(input, new Weapon("Rusty Sword", 2, 5));
    }

    private void DisplayCurrentRoom(Room room)
    {
        Console.WriteLine($"Your surroundings: {room.Description}");
        Console.WriteLine();
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

    private void DisplayEquipedWeapon(Weapon weapon)
    {
        Console.WriteLine($"Equiped weapon: {weapon.Name}");
    }

    private void PlayerTurn(Room currentRoom)
    {
        DisplayActions();
        MakeTurn();
        
        void DisplayActions()
        {
            Console.WriteLine("You can do:");

            int menuOption = 1;
            Console.WriteLine($"[{menuOption++}] Move Up\n" +
                          $"[{menuOption++}] Move Right\n" +
                          $"[{menuOption++}] Move Down\n" +
                          $"[{menuOption++}] Move Left");

            if (currentRoom.Weapon != null)
                Console.WriteLine($"[{menuOption++}] Collect Item");

            if (_player.Inventory[0] != null)
            {
                Console.WriteLine($"[{menuOption++}] Change Equiped Weapon");
                Console.WriteLine("[0] Remove Weapon From Inventory");
            }

            Console.WriteLine();
        }

        void MakeTurn()
        {
            while (true)
            {
                Console.Write($"{_player.Name}, what do you want to do? ");
                int input = Convert.ToInt32(Console.ReadLine());

                if ((_player.Inventory[0] != null ? input >= 0 : input >= 1) &&
                    (currentRoom.Weapon != null ? 
                    (_player.Inventory[0] != null ? input <= 6 : input <= 5) :
                    (_player.Inventory[0] != null ? input <= 5 : input <= 4)))
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
                            {
                                if (currentRoom.Weapon != null)
                                    _player.AddToInventory(currentRoom.TakeWeapon());
                                else
                                    _player.EquipWeapon(AskWeapon("Which weapon you wish to equip? "));

                                break;
                            }
                        case 6:
                            _player.EquipWeapon(AskWeapon("Which weapon you wish to equip? "));
                            break;
                        case 0:
                            {
                                if (!_player.RemoveFromInventory(AskWeapon("Which weapon you wish to remove from inventory? ")))
                                {
                                    Console.WriteLine("There's no such item");
                                    continue;
                                }
                                break;
                            }
                        default: 
                            break;
                    };

                    break;
                }  
                else Console.WriteLine("There's no such option.");
            }
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
}