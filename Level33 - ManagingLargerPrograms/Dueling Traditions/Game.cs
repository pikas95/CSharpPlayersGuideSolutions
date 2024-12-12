namespace FountainOfObjectsGame
{
    public class Game
    {
        Player player = new Player();
        Map map;
        WorldEntity[] entities = [new Maelstrom(), new Pit(), new Amarock()];
        Fountain fountain = new Fountain();
        public Game(int gameSize) { map = new Map(gameSize); }
        public void Run()
        {
            DisplayIntro();
            while (!(fountain.IsActivated && player.Row == 0 && player.Column == 0))
            {
                Console.WriteLine(player.ToString());
                Surroundings.Print(map, player, fountain);
                Console.WriteLine("What do you want to do?");

                Console.ForegroundColor = ConsoleColor.Cyan;
                string? input = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;

                CommandExecution(input!);
                Console.WriteLine("--------------------------------------------------------------------------------------------");

                foreach (WorldEntity entity in entities)
                    if (entity.Event(player, map))
                        return;
            }
            PlayerWon();
        }
        private void CommandExecution(string input)
        {
            ICommand? moveCommand = input switch
            {
                "move north" => new MoveCommand(player.Row - 1, player.Column),
                "move south" => new MoveCommand(player.Row + 1, player.Column),
                "move west" => new MoveCommand(player.Row, player.Column - 1),
                "move east" => new MoveCommand(player.Row, player.Column + 1),
                "shoot north" => new ShootCommand(player.Row - 1, player.Column),
                "shoot south" => new ShootCommand(player.Row + 1, player.Column),
                "shoot west" => new ShootCommand(player.Row, player.Column - 1),
                "shoot east" => new ShootCommand(player.Row + 1, player.Column),
                _ => null
            };
            if (moveCommand != null)
                moveCommand.Run(player, map);
            else if (input == "enable fountain")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                if (fountain.IsActivated)
                    Console.WriteLine("You already activated the fountain!");
                else if (!fountain.Event(player, map))
                    Console.WriteLine("You are not in the room where the fountain is!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (input == "help")
                DisplayHelp();
            else
                Console.WriteLine("There is no such command.");
        }
        private static void DisplayIntro()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("You enter the Cavern of Objects, a maze of rooms filled with dangerous pits in search of the Fountain of Objects.");
            Console.WriteLine("Light is visible only in the entrance, and no other light is seen anywhere in the caverns.");
            Console.WriteLine("You must navigate the Caverns with your other senses.");
            Console.WriteLine("Find the Fountain of Objects, activate it, and return to the entrance.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("--------------------------------------------------------------------------------------------");
            Console.WriteLine("Look out for pits. You will feel a breeze if a pit is in an adjacent room.\nIf you enter a room with a pit, you will die.\n");
            Console.WriteLine("Maelstroms are violent forces of sentient wind.\n" +
                              "Entering a room with one could transport you to any other location in the caverns.\n" +
                              "You will be able to hear their growling and groaning in nearby rooms.\n");
            Console.WriteLine("Amaroks roam the caverns. Encountering one is certain death, but you can smell their rotten stench in nearby rooms.\n");
            Console.WriteLine("You carry with you a bow and a quiver of arrows.\nYou can use them to shoot monsters in the caverns but be warned: you have a limited supply.\n");
            Console.WriteLine("You can type in \"help\" to get a list of commands. Good luck!");
            Console.WriteLine("--------------------------------------------------------------------------------------------");
            Console.WriteLine();
        }
        private static void DisplayHelp()
        {
            Console.WriteLine("\nmove north, move south, move west, move east - moves the player");
            Console.WriteLine("shoot north, shoot south, shoot west, shoot east - shoots an arrow");
            Console.WriteLine("enable fountain - attempts to activate fountain");
        }
        private void PlayerWon()
        {
            Surroundings.Print(map, player, fountain);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("The Fountain of Objects has been reactivated, and you have escaped with your life!");
            Console.WriteLine("You Win!");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}