namespace FountainOfObjectsGame
{
    public class WorldEntity
    {
        protected string? Message { get; }
        protected RoomType RoomType { get; }
        public WorldEntity(RoomType roomType) { RoomType = roomType; }
        public WorldEntity(RoomType roomType, string message) { RoomType = roomType; Message = message; }
        public virtual bool Event(Player player, Map map)
        {
            if (map.RoomIs(RoomType, player.Row, player.Column))
            {
                Console.WriteLine(player.ToString());
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Message);
                Console.WriteLine("You lost...");
                Console.ForegroundColor = ConsoleColor.White;
                return true;
            }
            return false;
        }
    }
}