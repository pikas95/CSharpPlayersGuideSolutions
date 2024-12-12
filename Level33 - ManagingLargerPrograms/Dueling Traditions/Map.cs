namespace FountainOfObjectsGame
{
    public class Map // knows rooms/grid; knows how to check did player enter specific room; or what room is in specific coordinates; and validates move commands
    {
        public RoomType[,] Room { get; private set; }
        public Map(int gameSize) // constructor that initializes rooms - entrance, fountain, pits, maelstroms, amarocks
        {
            Room = new RoomType[2 * gameSize, 2 * gameSize];
            Room[0, gameSize] = RoomType.FountainOfObjects;
            Room[0, 0] = RoomType.Entrance;

            for (int i = 2; i < 2 * gameSize; i += gameSize)
                for (int j = 2; j < 2 * gameSize; j += gameSize)
                    Room[i, j] = RoomType.Pit;

            if (gameSize < 4)
                Room[2 * gameSize - 1, 1] = RoomType.Maelstrom;
            else
            {
                Room[2 * gameSize - 1, 1] = RoomType.Maelstrom;
                Room[2 * gameSize - 5, gameSize] = RoomType.Maelstrom;
            }

            if (gameSize == 2)
                Room[gameSize, 0] = RoomType.Amarock;
            else if (gameSize == 3)
            {
                Room[gameSize, 0] = RoomType.Amarock;
                Room[gameSize, gameSize] = RoomType.Amarock;
            }
            else
            {
                Room[gameSize, 0] = RoomType.Amarock;
                Room[gameSize, gameSize] = RoomType.Amarock;
                Room[0, 2 * gameSize - 1] = RoomType.Amarock;
            }
        }
        public bool MoveValidation(int row, int column)
        {
            if (row < Room.GetLength(0) && column < Room.GetLength(1) && row >= 0 && column >= 0)
                return true;
            Console.ForegroundColor = ConsoleColor.Gray;
            if (row == -1 && column == 0)
                Console.WriteLine("You are not done yet!");
            else
                Console.WriteLine("You bumped into a wall...");
            Console.ForegroundColor = ConsoleColor.White;
            return false;
        }
        public bool RoomIs(RoomType room, int row, int column) => Room[row, column] == room;
        public void ClearRoom(int row, int column) => Room[row, column] = RoomType.Empty;
    }
}