Console.Write("What game size you want to play: small - 4x4, medium - 6x6, large - 8x8? ");
string? input = Console.ReadLine();

while (input != "small" && input != "medium" && input != "large")
{
    Console.Write("There's no such option. ");
    input = Console.ReadLine();
}

Console.Clear();

int gameSize = input switch
{
    "small" => 4,
    "medium" => 6,
    _ => 8
};

new FountainOfObjectsGame(gameSize).Run();

internal class FountainOfObjectsGame
{
    public Player Player { get; }
    public Map Map { get; }
    public Monster[] Monsters { get; }

    public FountainOfObjectsGame(int gameSize)
    {
        Player = new Player(gameSize switch
        {
            4 => new Location(0, 0),
            6 => new Location(5, 0),
            _ => new Location(2, 7)
        });

        Map = new Map(gameSize, Player.Location);

        Monsters = gameSize switch
        {
            6 => new Monster[3] { new Maelstrom(new Location(3, 0)),
                                  new Amarok(new Location(2, 3)),
                                  new Amarok(new Location(5, 1)) },
            8 => new Monster[5] { new Maelstrom(new Location(3, 0)),
                                  new Maelstrom(new Location(1, 5)),
                                  new Amarok(new Location(2, 3)),
                                  new Amarok(new Location(5, 1)),
                                  new Amarok(new Location(7, 2)) },
            _ => new Monster[2] { new Maelstrom(new Location(3, 0)),
                                  new Amarok(new Location(2, 3)) },
        };
    }

    public void Run()
    {
        DisplayIntroMessage();

        DateTime gameStart = DateTime.Now;

        while (!(Map.Fountain.IsActivated && Player.Location == Map.Entrance.Location) && Player.IsAlive)
        {
            DisplayStatus();
            DisplaySenses();
            Console.Write("What do you want to do? ");

            ICommand? command = GetCommandFromInput(GetInput());

            while (command?.Run(this) != true)
            {
                Console.Write(GetCommandErrorText(command));
                command = GetCommandFromInput(GetInput());
            }

            Console.Clear();

            for (int i = 0; i < Monsters.Length; i++)
                if (Monsters[i].Location == Player.Location && Monsters[i].IsAlive)
                    Monsters[i].ActAgainstPlayer(Player, Map);

            if (Map.GetRoomAtLocation(Player.Location) == RoomType.Pit)
                Player.Kill("You fell into the pit and died...");
        }

        DateTime gameEnd = DateTime.Now;

        DisplayStatus();

        if (Player.IsAlive)
            Console.WriteLine("You Win!");
        else
            Console.WriteLine(Player.DeathCause + "\n" + "You lost...");

        TimeSpan gameTime = gameEnd - gameStart;

        Console.WriteLine("\n" + $"You've spent {gameTime.Minutes} minutes and {gameTime.Seconds} seconds playing.");
    }

    private static void DisplayIntroMessage()
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("You enter the Cavern of Objects, a maze of rooms filled with dangerous pits in search of the Fountain of Objects.\r\n" +
                          "Light is visible only in the entrance, and no other light is seen anywhere in the caverns.\r\n" +
                          "You must navigate the Caverns with your other senses.\r\n" +
                          "Find the Fountain of Objects, activate it, and return to the entrance.\n" +
                          "Look out for pits. You will feel a breeze if a pit is in an adjacent room. If you enter a room with a pit, you will die\n" +
                          "Maelstroms are violent forces of sentient wind.\n" +
                          "Entering a room with one could transport you to any other location in the caverns. \n" +
                          "You will be able to hear their growling and groaning in nearby rooms.\n" +
                          "Amaroks roam the caverns. Encountering one is certain death, but you can smell their rotten stench in nearby rooms\n" +
                          "You carry with you a bow and a quiver of arrows. You can use them to shoot monsters in the caverns but be warned: \n" +
                          "You have a limited supply. ");

        Console.Write("Press enter to start playing. ");
        Console.ReadLine();
        Console.Clear();
        Console.ResetColor();
    }

    private void DisplayStatus()
    {
        Console.WriteLine($"You are in the room at (Row={Player.Location.Row}, Column={Player.Location.Col}). Arrows - {Player.Arrows}");

        RoomType currentRoomType = Map.GetRoomAtLocation(Player.Location);

        if (currentRoomType != RoomType.Normal)
        {
            Console.ForegroundColor = currentRoomType switch
            {
                RoomType.Entrance => ConsoleColor.Yellow,
                RoomType.Fountain => ConsoleColor.Blue,
                _ => default
            };

            Console.Write(currentRoomType switch
            {
                RoomType.Entrance => Map.Entrance.ToString() + "\n",
                RoomType.Fountain => Map.Fountain.ToString() + "\n",
                _ => ""
            });

            Console.ResetColor();
        }
    }

    private void DisplaySenses()
    {
        for (int i = 0; i < Monsters.Length; i++)
        {
            if (Monsters[i].IsAlive &&
                Math.Abs(Monsters[i].Location.Row - Player.Location.Row) <= 1 &&
                Math.Abs(Monsters[i].Location.Col - Player.Location.Col) <= 1)
                Console.WriteLine(Monsters[i].SenseText);
        }

        if (Map.HasNeigbhorRoomType(Player.Location, RoomType.Pit))
            Console.WriteLine("You feel a draft. There is a pit in a nearby room.");
    }

    private static ICommand? GetCommandFromInput(string? input)
    {
        return input switch
        {
            "move north" => new MoveNorth(),
            "move south" => new MoveSouth(),
            "move west" => new MoveWest(),
            "move east" => new MoveEast(),
            "shoot north" => new ShootNorth(),
            "shoot south" => new ShootSouth(),
            "shoot west" => new ShootWest(),
            "shoot east" => new ShootEast(),
            "enable fountain" => new EnableFountain(),
            "help" => new HelpCommand(),
            _ => null
        };
    }

    private static string? GetInput()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        string? input = Console.ReadLine();
        Console.ResetColor();
        return input;
    }

    private static string GetCommandErrorText(ICommand? command)
    {
        return command switch
        {
            null => "There's no such command. ",
            MoveCommand => "You bumped into a wall. ",
            ShootCommand => "You don't have any more arrows. ",
            EnableFountain => "Fountain is already activated. ",
            _ => "You can't do that. "
        };
    }
}

internal record Location(int Row, int Col);

internal class Map
{
    private RoomType[,] Rooms { get; set; }
    public Fountain Fountain { get; }
    public CavernEntrance Entrance { get; }

    public Map(int gameSize, Location entranceLocation)
    {
        Rooms = new RoomType[gameSize, gameSize];

        Entrance = new CavernEntrance(entranceLocation);
        Rooms[Entrance.Location.Row, Entrance.Location.Col] = RoomType.Entrance;

        Fountain = new Fountain(gameSize switch
        {
            4 => new Location(0, 2),
            6 => new Location(3, 2),
            _ => new Location(5, 5)
        });
        Rooms[Fountain.Location.Row, Fountain.Location.Col] = RoomType.Fountain;

        Rooms[0, 1] = RoomType.Pit;
        if (gameSize > 4)
            Rooms[2, 5] = RoomType.Pit;
        if (gameSize > 6)
            Rooms[4, 7] = RoomType.Pit;
    }

    public RoomType GetRoomAtLocation(Location location) => ValidLocation(location) ? Rooms[location.Row, location.Col] : RoomType.OffMap;

    public bool HasNeigbhorRoomType(Location location, RoomType roomType)
    {
        if (GetRoomAtLocation(new Location(location.Row - 1, location.Col - 1)) == roomType) return true;
        if (GetRoomAtLocation(new Location(location.Row - 1, location.Col)) == roomType) return true;
        if (GetRoomAtLocation(new Location(location.Row - 1, location.Col + 1)) == roomType) return true;
        if (GetRoomAtLocation(new Location(location.Row, location.Col - 1)) == roomType) return true;
        if (GetRoomAtLocation(new Location(location.Row, location.Col)) == roomType) return true;
        if (GetRoomAtLocation(new Location(location.Row, location.Col + 1)) == roomType) return true;
        if (GetRoomAtLocation(new Location(location.Row + 1, location.Col - 1)) == roomType) return true;
        if (GetRoomAtLocation(new Location(location.Row + 1, location.Col)) == roomType) return true;
        if (GetRoomAtLocation(new Location(location.Row + 1, location.Col + 1)) == roomType) return true;
        return false;
    }

    public bool ValidLocation(Location location) =>
        location.Row >= 0 &&
        location.Row < Rooms.GetLength(0) &&
        location.Col >= 0 &&
        location.Col < Rooms.GetLength(1);

    public Location ClampInvalidLocation(Location location) => new Location(Math.Clamp(location.Row, 0, Rooms.GetLength(0)) - 1, Math.Clamp(location.Col, 0, Rooms.GetLength(1) - 1));
}

internal class WorldEntity
{
    public Location Location { get; protected set; }

    public WorldEntity(Location location) { Location = location; }
}

internal class Pawn : WorldEntity
{
    public bool IsAlive { get; protected set; } = true;
    public Pawn(Location location) : base(location) { }

    protected void Kill() => IsAlive = false;
}

internal class Player : Pawn
{
    public string DeathCause { get; private set; } = "";
    public int Arrows { get; private set; } = 5;

    public Player(Location location) : base(location) { }

    public void Move(int row, int col) => Location = new Location(Location.Row + row, Location.Col + col);

    public bool Shoot()
    {
        if (Arrows <= 0)
            return false;

        Arrows--;
        return true;
    }

    public void Kill(string reason)
    {
        base.Kill();
        DeathCause = reason;
    }
}

internal abstract class Monster : Pawn
{
    public string SenseText { get; }

    protected Monster(Location location, string senseText) : base(location) { SenseText = senseText; }

    public abstract void ActAgainstPlayer(Player player, Map map);

    public new void Kill() => base.Kill();
}

internal class Maelstrom : Monster
{
    public Maelstrom(Location location) : base(location, "You hear the growling and groaning of a maelstrom nearby.") { }

    public override void ActAgainstPlayer(Player player, Map map)
    {
        Location newPlayerLocation = Validate(new Location(player.Location.Row - 1, player.Location.Col + 2));
        player.Move(newPlayerLocation.Row - player.Location.Row, newPlayerLocation.Col - player.Location.Col);

        Location = Validate(new Location(Location.Row + 1, Location.Col - 2));

        Console.WriteLine("You were blown away by a maelstrom.");

        Location Validate(Location location)
        {
            if (!map.ValidLocation(location))
                location = map.ClampInvalidLocation(location);

            return location;
        }
    }
}

internal class Amarok : Monster
{
    public Amarok(Location location) : base(location, "You can smell the rotten stench of an amarok in a nearby room.") { }

    public override void ActAgainstPlayer(Player player, Map map) => player.Kill("Amarok crushed you to death..");
}

internal class CavernEntrance : WorldEntity
{
    public CavernEntrance(Location location) : base(location) { }

    public override string ToString() => "You see light in this room coming from outside the cavern. This is the entrance.";
}

internal class Fountain : WorldEntity
{
    public bool IsActivated { get; set; }

    public Fountain(Location location) : base(location) { }

    public override string ToString() => IsActivated ? "You hear the rushing waters from the Fountain of Objects. It has been reactivated!"
                                                     : "You hear water dripping in this room. The fountain of Objects is here!";
}

internal interface ICommand { public bool Run(FountainOfObjectsGame game); }

internal class MoveCommand : ICommand
{
    protected int Row { get; }
    protected int Col { get; }

    public MoveCommand(int row, int col)
    {
        Row = row;
        Col = col;
    }

    public bool Run(FountainOfObjectsGame game)
    {
        if (game.Map.ValidLocation(new Location(game.Player.Location.Row + Row, game.Player.Location.Col + Col)))
        {
            game.Player.Move(Row, Col);
            return true;
        }

        return false;
    }
}

internal class MoveNorth : MoveCommand { public MoveNorth() : base(-1, 0) { } }

internal class MoveSouth : MoveCommand { public MoveSouth() : base(1, 0) { } }

internal class MoveEast : MoveCommand { public MoveEast() : base(0, 1) { } }

internal class MoveWest : MoveCommand { public MoveWest() : base(0, -1) { } }

internal class ShootCommand : ICommand
{
    protected int Row { get; }
    protected int Col { get; }

    public ShootCommand(int row, int col)
    {
        Row = row;
        Col = col;
    }

    public bool Run(FountainOfObjectsGame game)
    {
        if (!game.Player.Shoot())
            return false;

        CheckForTargetInLocation(game.Monsters, new Location(game.Player.Location.Row + Row, game.Player.Location.Col + Col))?.Kill();
        return true;
    }

    protected static Monster? CheckForTargetInLocation(Monster[] monsters, Location location)
    {
        for (int i = 0; i < monsters.Length; i++)
            if (location == monsters[i].Location)
                return monsters[i];

        return null;
    }
}

internal class ShootNorth : ShootCommand { public ShootNorth() : base(-1, 0) { } }

internal class ShootSouth : ShootCommand { public ShootSouth() : base(1, 0) { } }

internal class ShootWest : ShootCommand { public ShootWest() : base(0, -1) { } }

internal class ShootEast : ShootCommand { public ShootEast() : base(0, 1) { } }

internal class HelpCommand : ICommand
{
    public bool Run(FountainOfObjectsGame game)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.White;

        Console.WriteLine("Commands:");
        Console.WriteLine("enable fountain");
        Console.WriteLine("    Turns on the Fountain of Objects if you are in the fountain room, or does nothing if you are not.");
        Console.WriteLine("move north");
        Console.WriteLine("    Moves to the room directly north of the current room, as long as there are no walls.");
        Console.WriteLine("move south");
        Console.WriteLine("    Moves to the room directly south of the current room, as long as there are no walls.");
        Console.WriteLine("move east");
        Console.WriteLine("    Moves to the room directly east of the current room, as long as there are no walls.");
        Console.WriteLine("move west");
        Console.WriteLine("    Moves to the room directly west of the current room, as long as there are no walls.");
        Console.WriteLine("shoot north");
        Console.WriteLine("    Fires an arrow into the room to the north, killing any monster in that room.");
        Console.WriteLine("shoot south");
        Console.WriteLine("    Fires an arrow into the room to the south, killing any monster in that room.");
        Console.WriteLine("shoot east");
        Console.WriteLine("    Fires an arrow into the room to the east, killing any monster in that room.");
        Console.WriteLine("shoot west");
        Console.WriteLine("    Fires an arrow into the room to the west, killing any monster in that room.");

        Console.Write("Press enter to continue. ");
        Console.ReadLine();
        Console.Clear();
        return true;
    }
}

internal class EnableFountain : ICommand
{
    public bool Run(FountainOfObjectsGame game)
    {
        if (game.Map.GetRoomAtLocation(game.Player.Location) != RoomType.Fountain || game.Map.Fountain.IsActivated)
            return false;

        game.Map.Fountain.IsActivated = true;
        return true;
    }
}

internal enum RoomType { Normal, Entrance, Fountain, Pit, OffMap }