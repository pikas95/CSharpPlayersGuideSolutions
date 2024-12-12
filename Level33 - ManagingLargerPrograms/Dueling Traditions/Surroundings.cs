namespace FountainOfObjectsGame
{
    public static class Surroundings // describes to player what are the surroundings, knows how to check adjecent rooms for danger
    {
        public static void Print(Map map, Player player, Fountain fountain)
        {
            if (map.RoomIs(RoomType.FountainOfObjects, player.Row, player.Column))
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                if (fountain.IsActivated)
                    Console.WriteLine("You hear the rushing waters from the Fountain of Objects. It has been reactivated!");
                else
                    Console.WriteLine("You hear water dripping in this room. The Fountain of Objects is here!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            if (AdjacentRoomsCheck(RoomType.Pit, map, player.Row, player.Column))
                Console.WriteLine("You feel a draft. There is a pit in a nearby room.");
            if (AdjacentRoomsCheck(RoomType.Amarock, map, player.Row, player.Column))
                Console.WriteLine("You can smell the rotten stench of an amarok in a nearby room.");
            if (AdjacentRoomsCheck(RoomType.Maelstrom, map, player.Row, player.Column))
                Console.WriteLine("You hear the growling and groaning of a maelstrom nearby.");
            if (player.Row == 0 && player.Column == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("You see light coming from the cavern entrance.");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        private static bool AdjacentRoomsCheck(RoomType room, Map map, int row, int column)
        {
            if (row < map.Room.GetLength(0) - 1 && map.Room[row + 1, column] == room) return true;
            if (row > 0 && map.Room[row - 1, column] == room) return true;

            if (column < map.Room.GetLength(1) - 1 && map.Room[row, column + 1] == room) return true;
            if (column > 0 && map.Room[row, column - 1] == room) return true;

            if (row > 0 && column > 0 && map.Room[row - 1, column - 1] == room) return true;
            if (row < map.Room.GetLength(0) - 1 && column < map.Room.GetLength(1) - 1 && map.Room[row + 1, column + 1] == room) return true;

            if (column < map.Room.GetLength(1) - 1 && row > 0 && map.Room[row - 1, column + 1] == room) return true;
            if (column > 0 && row < map.Room.GetLength(0) - 1 && map.Room[row + 1, column - 1] == room) return true;
            return false;
        }
    }
}