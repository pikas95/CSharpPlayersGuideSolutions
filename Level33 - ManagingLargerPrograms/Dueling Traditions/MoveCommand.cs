namespace FountainOfObjectsGame
{
    public class MoveCommand : ICommand
    {
        private readonly int _row;
        private readonly int _column;
        public MoveCommand(int row, int column) { _row = row; _column = column; }
        public void Run(Player player, Map map)
        {
            if (map.MoveValidation(_row, _column))
                player.ChangeCoordinates(_row, _column);
        }
    }
}