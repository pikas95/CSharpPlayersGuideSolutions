namespace FountainOfObjectsGame
{
    public class ShootCommand : ICommand
    {
        private readonly int _row;
        private readonly int _column;
        public ShootCommand(int row, int column) { _row = row; _column = column; }
        public void Run(Player player, Map map)
        {
            if (player.Arrows == 0)
            {
                Console.WriteLine("You are out of arrows.");
                return;
            }
            else if (_row < map.Room.GetLength(0) && _column < map.Room.GetLength(1) && _row >= 0 && _column >= 0)
                ShootEnemy(map);
            else
                Console.WriteLine("Arrow hit the wall.");
            player.ArrowDecrement();
        }
        private void ShootEnemy(Map map)
        {
            if (map.RoomIs(RoomType.Amarock, _row, _column))
                EnemyDown("You shot down an amarock.");
            else if (map.RoomIs(RoomType.Maelstrom, _row, _column))
                EnemyDown("You shot down a maelstrom.");
            else
                Console.WriteLine("Arrow didn't hit anything.");
            void EnemyDown(string message)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                map.ClearRoom(_row, _column);
                Console.WriteLine(message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}