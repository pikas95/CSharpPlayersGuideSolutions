namespace FountainOfObjectsGame;
public class Maelstrom : WorldEntity
{
    public Maelstrom() : base(RoomType.Maelstrom, "You were blown away by a Maelstrom.") { }
    public override bool Event(Player player, Map map)
    {
        if (map.Room[player.Row, player.Column] == RoomType)
        {
            map.Room[Math.Clamp(player.Row + 1, 0, map.Room.GetLength(0) - 1), Math.Clamp(player.Column - 2, 0, map.Room.GetLength(1) - 1)] = RoomType;
            map.Room[player.Row, player.Column] = RoomType.Empty;
            player.ChangeCoordinates(Math.Clamp(player.Row - 1, 0, map.Room.GetLength(0) - 1), Math.Clamp(player.Column + 2, 0, map.Room.GetLength(1) - 1));
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(Message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        return false;
    }
}
public class Pit : WorldEntity { public Pit() : base(RoomType.Pit, "You felt into a pit.") { } }
public class Amarock : WorldEntity { public Amarock() : base(RoomType.Amarock, "Amarock crushed you.") { } }