internal class GameProgram
{
    private readonly Map _map = new Map();
    private Player _player;

    public void RunGame()
    {
        _player = CreatePlayer();

        while (_player.IsAlive)
        {
            Room currentRoom = _map.GetRoom(_player.Row, _player.Col);

            // displays room player is in
            Console.Write("Your surroundings: ");
            DisplayRoomInstance(currentRoom);
            Console.WriteLine();

            // displays player's inventory
            Console.Write("Your inventory: ");
            if (!DisplayInventory(_player.Inventory))
                Console.WriteLine("Inventory is empty");
            Console.WriteLine();
            
            // displays equiped weapon
            DisplayEquipedWeapon(_player.EquipedWeapon);
            Console.WriteLine();

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

        if (input == null || input == "")
            input = "Unknown";

        Console.Clear();
        return new Player(input, new Weapon("Rusty Sword", 2, 5));
    }

    private void DisplayRoomInstance(Room room) => Console.WriteLine(room.Description);

    private bool DisplayInventory(Inventory[] inventory)
    {
        if (inventory[0]?.Weapon == null)
            return false;

        Console.WriteLine();
        for (int i = 0; i < inventory.Length; i++)
            if (inventory[i] != null)
                Console.WriteLine($"[{i + 1}] {inventory[i].Weapon.Name}");

        return true;
    }

    private void DisplayEquipedWeapon(Weapon weapon) => Console.WriteLine($"Equiped weapon: {weapon.Name}");

    private void PlayerTurn(Room currentRoom)
    {
        DisplayActions();
        MakeTurn();
        

        void DisplayActions()
        {
            Console.WriteLine("[1] Move Nouth\n" +
                          "[2] Move East\n" +
                          "[3] Move South\n" +
                          "[4] Move West");

            if (_player.Inventory[0] != null)
                Console.WriteLine("[5] Change Equiped Weapon");

            if (currentRoom._weapon != null)
                Console.WriteLine("[6] Collect Item");

            Console.WriteLine();
        }

        void MakeTurn()
        {
            while (true)
            {
                Console.Write($"{_player.Name}, what do you want to do? ");
                int input = Convert.ToInt32(Console.ReadLine());

                if (input >= 1 &&
                    currentRoom._weapon != null ? input <= 6 :
                    _player.Inventory[0] != null ? input <= 5 : input <= 4)
                {
                    switch (input)
                    {
                        case 1: MovePlayer(0, -1); break;
                        case 2: MovePlayer(1, 0); break;
                        case 3: MovePlayer(0, 1); break;
                        case 4: MovePlayer(-1, 0); break;
                        case 5: _player.EquipWeapon(AskWeapon()); break;
                        case 6: _player.AddToInventory(currentRoom.TakeWeapon()); break;
                        default: break;
                    };

                    break;
                }  
                else Console.WriteLine("There's no such option.");
            }
        }

        void MovePlayer(int row, int col) => _player.Move(row, col);

        int AskWeapon()
        {
            Console.Write("Which weapon you wish to equip? ");
            while (true)
            {
                int input = Convert.ToInt32(Console.ReadLine()) - 1;//  because inventory items are displayed as index + 1

                if (input >= 0 || input < _player.Inventory.Length) 
                    return input;

                Console.WriteLine("There is no such weapon");
            }
        }
    }
}